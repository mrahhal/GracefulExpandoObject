using Xunit;

namespace GracefulExpandoObject
{
	public class GracefulDictionaryTest
	{
		[Fact]
		public void Indexer_KeyDoesNotExist_ReturnsNull()
		{
			var gd = new GracefulDictionary();

			var value = gd["foo"];

			Assert.Null(value);
		}

		[Fact]
		public void TryGetValue_KeyDoesNotExist_ReturnsFalse_WithNullValue()
		{
			var gd = new GracefulDictionary();
			var value = default(object);

			var result = gd.TryGetValue("foo", out value);

			Assert.False(result);
			Assert.Null(value);
		}

		[Fact]
		public void Indexer_KeyExists_ReturnsValue()
		{
			var gd = new GracefulDictionary();
			gd["foo"] = "bar";

			var value = gd["foo"];

			Assert.Equal("bar", value);
		}

		[Fact]
		public void TryGetValue_KeyExists_ReturnsTrue_WithValue()
		{
			var gd = new GracefulDictionary();
			gd["foo"] = "bar";
			var value = default(object);

			var result = gd.TryGetValue("foo", out value);

			Assert.True(result);
			Assert.Equal("bar", value);
		}
	}
}
