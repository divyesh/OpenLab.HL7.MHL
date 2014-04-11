using System;

namespace OpenLab.HL7.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var hl7 = new HL7ToPDF(@"Content\hl7.txt","hl7.pdf");
            hl7.CreatePDF();
            Console.WriteLine("PDF file is created.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
