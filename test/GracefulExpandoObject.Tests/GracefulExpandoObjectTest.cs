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
	}
}
