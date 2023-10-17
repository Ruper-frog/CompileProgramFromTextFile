using System;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;
using System.Reflection;
using System.Text;


namespace CompileProgramFromTextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter images location or simply drag the image in here");
            string fileCodePath = Console.ReadLine();
            if (fileCodePath[0].Equals('\"'))
                fileCodePath = fileCodePath.Trim('\"');

            string code = File.ReadAllText(fileCodePath);

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateExecutable = true;

            string Output = GetFilePath(fileCodePath) + @"\Out.exe";

            parameters.OutputAssembly = Output;
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, code);
            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Compilation successful.");
                System.Diagnostics.Process.Start(Output);
            }

            //Console.WriteLine("Press any key to continue . . .");
            //Console.ReadKey();
        }
        static string GetFilePath(string File)
        {
            return File.Substring(0, File.LastIndexOf('\\'));
        }
    }
}
