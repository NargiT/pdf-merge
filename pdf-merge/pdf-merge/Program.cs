using iText.Forms;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;


namespace pdf_merge
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!new Input(args).IsValid())
            {
                Console.WriteLine("Usage : pdf-merge FILES FILE");
                Console.WriteLine("");
                Console.WriteLine("\t FILES\t list of file separated by comma ','");
                Console.WriteLine("\t FILE\t the ouput file");
                return;
            }

            String outputPath = args.Last();
            List<String> toMerge = new List<string>(args.Reverse().Skip(1).Reverse());
            new PDFFiles(toMerge).Merge(outputPath);
        }
    }

    internal class PDFFiles
    {
        private readonly List<PdfDocument> _pdfFiles;

        public PDFFiles(List<String> filesPath)
        {
            _pdfFiles = new List<PdfDocument>();

            foreach (var filePath in filesPath)
            {
                _pdfFiles.Add(new PdfDocument(new PdfReader(filePath)));
            }
        }


        public void Merge(String path)
        {
            PdfWriter pdfWriter = new PdfWriter(path);
            pdfWriter.SetSmartMode(true);
            PdfDocument finalPdf = new PdfDocument(pdfWriter);
            finalPdf.InitializeOutlines();

            foreach (var pdfDocuement in _pdfFiles)
            {
                pdfDocuement.CopyPagesTo(1, pdfDocuement.GetNumberOfPages(), finalPdf, new PdfPageFormCopier());
            }

            finalPdf.Close();
        }
    }

    internal class Input
    {
        private readonly List<String> _arguments;

        public Input(string[] args)
        {
            _arguments = new List<string>(args);
        }

        public bool IsValid()
        {
            return _arguments.Count > 2;
        }
    }
}