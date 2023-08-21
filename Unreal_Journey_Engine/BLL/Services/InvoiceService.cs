using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelectPdf;




namespace BLL.Services
{
    public class InvoiceService
    {
        public static bool Create_and_Send_Invoice(string Email_To, string invoiceHtml)
        {

            // Create an instance of the HtmlToPdf class
            var converter = new HtmlToPdf();

            // Set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfCompressionLevel = PdfCompressionLevel.Normal;

            // Convert HTML content to PDF
            PdfDocument doc = converter.ConvertHtmlString(invoiceHtml);

            // Use the generated PDF document (doc) as needed

            //MemoryStream pdfStream = new MemoryStream();
            //doc.Save(pdfStream);
            //pdfStream.Position = 0;
            // Convert the PdfDocument to a byte array
            byte[] pdfBytes;
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);
                pdfBytes = pdfStream.ToArray();
            }


            var decision = EmailService.SendEmail_With_Invoice(Email_To, "Invoice", "", pdfBytes, "Tour Invoice.pdf");
            

            return decision? true:false;
        }
    }
}
