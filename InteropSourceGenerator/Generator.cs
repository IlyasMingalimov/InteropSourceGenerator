using Microsoft.CodeAnalysis;

namespace InteropSourceGenerator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var code = @"
            namespace Test
            {
                public static class Welcome 
                {
                    public static void Print() => Console.WriteLine($""Hello World"");
                }
            }";
            context.AddSource("Test.welcome.generated.cs", code);
        }
        public void Initialize(GeneratorInitializationContext context)
        {
            
        }
    }
}