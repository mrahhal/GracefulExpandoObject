using System;
using System.Collections.Generic;
using Xunit;

namespace MR.Tests
{
	public class GracefulExpandoObjectTest
	{
		[Fact]
		public void MemberAccess_KeyDoesNotExist_ReturnsNull()
		{
			dynamic geo = new GracefulExpandoObject();

			var result = geo.Foo;

			Assert.Null(result);
		}

		[Fact]
		public void MemberAccess_KeyExists_ReturnsValue()
		{
			dynamic geo = new GracefulExpandoObject();
			geo.Foo = "bar";

			var result = geo.Foo;

			Assert.Equal("bar", result);
		}

		[Fact]
		public void FromObject_ArgumentNullCheck()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				GracefulExpandoObject.FromObject(null);
			});
		}

		[Fact]
		public void FromObject()
		{
			var obj = new
			{
				Foo = "foo",
				Bar = 2,
				Baz = true,
				Too = default(object)
			};

			dynamic geo = GracefulExpandoObject.FromObject(obj);
			var map = geo as IDictionary<string, object>;

			Assert.Equal("foo", geo.Foo);
			Assert.Equal(2, geo.Bar);
			Assert.True(geo.Baz);
			Assert.Null(geo.Too);
			Assert.True(map.ContainsKey("Too"));
		}

		[Fact]
		public void FromObject_DoesNotIgnoreNullValuesForComplexPropertiesByDefault()
		{
			var obj = new
			{
				Foo = "foo",
				Bar = default(object)
			};

			dynamic geo = GracefulExpandoObject.FromObject(obj);
			var map = geo as IDictionary<string, object>;

			Assert.True(map.ContainsKey("Bar"));
			Assert.Null(geo.Bar);
		}

		[Fact]
		public void FromObject_IgnoreNullValues_IgnoresNullValuesForComplexProperties()
		{
			var obj = new
			{
				Foo = "foo",
				Bar = default(object)
			};

			dynamic geo = GracefulExpandoObject.FromObject(obj, deep: false, ignoreNullValues: true);
			var map = geo as IDictionary<string, object>;

			Assert.False(map.ContainsKey("Bar"));
		}

		[Fact]
		public void FromObject_Deep_DoesNotIgnoreNullValuesForComplexPropertiesByDefault()
		{
			var obj = new
			{
				Foo = "foo",
				Bar = default(object),
				Baz = new
				{
					Some = "some"
				}
			};

			dynamic geo = GracefulExpandoObject.FromObject(obj, true);
			var map = geo as IDictionary<string, object>;

			Assert.True(map.ContainsKey("Bar"));
			Assert.Null(geo.Bar);
		}

		[Fact]
		public void FromObject_Deep()
		{
			var obj = new
			{
				Foo = "foo",
				Bar = new
				{
					Some = "some",
					Too = default(object)
				},
				Created = new DateTime(42)
			};

			dynamic geo = GracefulExpandoObject.FromObject(obj, true);
			var map = geo as IDictionary<string, object>;

			Assert.Equal("foo", geo.Foo);
			Assert.NotNull(geo.Bar);
			Assert.IsType<GracefulExpandoObject>(geo.Bar);
			Assert.Equal("some", geo.Bar.Some);
			Assert.Null(geo.Bar.Too);
			Assert.True(((IDictionary<string, object>)geo.Bar).ContainsKey("Too"));
			Assert.Equal(new DateTime(42), geo.Created);
		}
	}
}
