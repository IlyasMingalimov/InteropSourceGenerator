namespace InteropSourceGenerator.Tests;

public class MatchTest
{
	[Fact]
	public void Round()
	{
		Include.math.TestMethod();
		var result = PInvokeInclude.GlibsMatch.round(1.5);
		Assert.Equal(2.0, result);
	}
}