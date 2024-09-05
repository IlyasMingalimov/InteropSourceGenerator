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

            IncrementalValuesProvider<(string name, string content)> namesAndContents = textFiles.Select((text, cancellationToken) => (name: Path.GetFileNameWithoutExtension(text.Path), content: text.GetText(cancellationToken).ToString()));

            initContext.RegisterSourceOutput(namesAndContents, (spc, nameAndContent) =>
            {
                spc.AddSource($"Interop.{nameAndContent.name}", CodeGenerator.Generate(nameAndContent.content, nameAndContent.name));
            });
        }
    }
}