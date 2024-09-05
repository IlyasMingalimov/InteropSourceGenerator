using InteropCodeGenerator;
using Microsoft.CodeAnalysis;
using System.IO;

namespace InteropSourceGenerator
{
	[Generator(LanguageNames.CSharp)]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext initContext)
        {
            IncrementalValuesProvider<AdditionalText> textFiles = initContext.AdditionalTextsProvider.Where(file => file.Path.EndsWith(".h"));

            IncrementalValuesProvider<(string name, string content, string path)> namesAndContentAndPaths = textFiles.Select((text, cancellationToken) => (name: Path.GetFileNameWithoutExtension(text.Path), content: text.GetText(cancellationToken).ToString(), path: text.Path ));

            initContext.RegisterSourceOutput(namesAndContentAndPaths, (spc, nameAndContentAndPath) =>
            {
                spc.AddSource($"Interop.{nameAndContentAndPath.name}", CodeGenerator.Generate(nameAndContentAndPath.content, nameAndContentAndPath.name, nameAndContentAndPath.path));
            });
        }
    }
}