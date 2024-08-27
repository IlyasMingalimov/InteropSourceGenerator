using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Text;

namespace InteropSourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class Generator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext initContext)
        {
            IncrementalValuesProvider<AdditionalText> textFiles = initContext.AdditionalTextsProvider.Where(file => file.Path.EndsWith(".h"));

            IncrementalValuesProvider<(string name, string content)> namesAndContents = textFiles.Select((text, cancellationToken) => (name: Path.GetFileNameWithoutExtension(text.Path), content: text.GetText(cancellationToken).ToString()));

            initContext.RegisterSourceOutput(namesAndContents, (spc, nameAndContent) =>
            {
                spc.AddSource($"Interop.{nameAndContent.name}", GenerateInteropClass(nameAndContent.content, nameAndContent.name));
            });
        }

        private static string GenerateInteropClass(string content, string name) 
        {
            string className = $"public static partial class Interop";

            string DllAttribute = $"[DllImport(\"{name}.h\", CallingConvention = CallingConvention.Cdecl)]";
            string staticExtern = "public static extern ";
            string Namespace = "using System.Runtime.InteropServices;";

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(Namespace);
            stringBuilder.AppendLine(className);
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(DllAttribute);
            stringBuilder.Append(staticExtern);
            stringBuilder.Append(content);
            stringBuilder.AppendLine("}");
            var res = stringBuilder.ToString();
            Console.WriteLine(res);
            return res;
        }
    }
}