using Newtonsoft.Json;
using Xunit;

namespace MR.Json.Tests
{
	public class GracefulExpandoObjectConverterTest
	{
		[Fact]
		public void Works()
		{
			var value = "{}";

			dynamic geo = JsonConvert.DeserializeObject<GracefulExpandoObject>(
				value,
				new GracefulExpandoObjectConverter());
		}

		[Fact]
		public void KeyDoesNotExist_ReturnsNull()
		{
			var value = "{}";
			dynamic geo = JsonConvert.DeserializeObject<GracefulExpandoObject>(
				value,
				new GracefulExpandoObjectConverter());

			var result = geo.foo;

			Assert.Null(result);
		}

		[Fact]
		public void KeyExists_ReturnsValue()
		{
			var value = "{ \"foo\": \"bar\" }";
			dynamic geo = JsonConvert.DeserializeObject<GracefulExpandoObject>(
				value,
				new GracefulExpandoObjectConverter());

			var result = geo.foo;

			Assert.Equal("bar", result);
		}

		[Fact]
		public void NestedObjectsAreAlsoGracefulExpandoObjects()
		{
			var value = "{ \"foo\": \"bar\", \"nested\": { \"baz\": \"some\" } }";
			dynamic geo = JsonConvert.DeserializeObject<GracefulExpandoObject>(
				value,
				new GracefulExpandoObjectConverter());

			var result = geo.nested;

			Assert.IsType<GracefulExpandoObject>(result);
		}
	}
}
