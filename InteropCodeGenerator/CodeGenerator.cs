namespace InteropCodeGenerator
{
	public static class CodeGenerator
	{
		public static string Generate(string content, string name, string path)
		{
			return $"namespace Include {{ public static class {name} {{ public static void TestMethod() => Console.WriteLine(\"HelloWorld\"); }} }}";
		}
	}
}
