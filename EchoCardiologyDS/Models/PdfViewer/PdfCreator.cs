using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Image = iTextSharp.text.Image;

namespace EchoCardiologyDS.Models.PdfViewer
{
    public class PdfCreator : IView
    {
        private Patient _patient;
        private string _patientName = string.Empty;

        public PdfCreator(Patient patient)
        {
            _patient = patient;
            _patientName = string.IsNullOrWhiteSpace(_patient.FIO) ? DateTime.Today.ToShortDateString() : $"{_patient.FIO}_{DateTime.Today.ToShortDateString()}";
        }
        private readonly string _savePdfDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
        public void CreateDocument()
        {
            
            var doc = new Document();
            iTextSharp.text.Font f2 = FontFactory.GetFont(@"D:\CardeologyDS\EchoCardiologyDS\EchoCardiologyDS\Resources\ARIALUNI.TTF", BaseFont.IDENTITY_H, true,10);
            PdfWriter.GetInstance(doc, new FileStream(_savePdfDocumentsPath+Path.Combine($"{_patientName}.pdf"), FileMode.Create));
            doc.Open();
            PdfPTable table = new PdfPTable(5);
            Paragraph name = new Paragraph(new Phrase("Название параметра",f2));
            Paragraph p = new Paragraph(new Phrase("Эхокардиография",
              f2));
            p.IndentationRight = 20;
            doc.Add(p);
            PdfPCell cell = new PdfPCell(new Phrase("Эхокардиография",
              f2));
            cell.BackgroundColor = new BaseColor(Color.Wheat);
            cell.Padding = 5;
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Название",f2));
            table.AddCell(cell);
            
            table.AddCell("Название");          
            doc.Add(table);
            doc.Close();
        }
    }
}
