using EchoCardiologyDS.Models;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EchoCardiologyDS.Models.PatientAction;
using System.Threading.Tasks;

namespace EchoCardiologyDS.PfdImplementation
{
	public class PdfCreator : IView
	{
        IPatientRepository repository;
        DBConn dB =null;
		private PatientMV _patient;
		private string _patientName;
		private readonly string _savePdfDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +@"\Отчеты";
		private readonly string _pathToFont = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+ "\\Resources\\ARIALUNI.TTF";
		//private readonly string _pathToFont = Path.Combine( AppDomain.CurrentDomain.BaseDirectory , "ARIALUNI.TTF");
		public PdfCreator(PatientMV patient,DBConn conn)
		{
            if (conn == null) return;
           
            dB = conn;
            _patient = patient;
            //repository = new PatientRepository(dB);
            //Task task = new Task(()=>
            //{
            //    repository.Initialize(patient);
            //});
            //task.Start();

            string date = _patient.Patient.ResearchDateTime.ToString().Replace(" ", "_").Replace(":", "_");
            _patientName = string.IsNullOrWhiteSpace(_patient.Patient.FIO) ? date : $"{_patient.Patient.FIO}_{date}";
			CreateFolder();
        }

		public void CreateDocument()
		{
			var doc = new Document();
			Font f2 = FontFactory.GetFont(_pathToFont, BaseFont.IDENTITY_H, true, 10);
			PdfWriter.GetInstance(doc, new FileStream(_savePdfDocumentsPath + $@"\{_patientName}.pdf", FileMode.Create));
			string w = _savePdfDocumentsPath + $@"\{_patientName}.pdf";
			doc.Open();
			PdfPTable table = new PdfPTable(4);
			table.DefaultCell.Border = Rectangle.NO_BORDER;
			float[] width = new float[] { 400f, 1150f, 400f, 1150f };

			table.SetWidths(width);

			Paragraph p1 = new Paragraph(new Phrase("Ф.И.О пациента", f2));
			Paragraph p2 = new Paragraph(new Phrase(_patient.Patient.FIO+"  "+_patient.Patient.CardNumber, f2));
			Paragraph p3 = new Paragraph(new Phrase("Ф.И.О доктора", f2));
			Paragraph p4 = new Paragraph(new Phrase(_patient.Patient.DoctorName, f2));
			PdfPCell cell1 = new PdfPCell(p1);
			PdfPCell cell2 = new PdfPCell(p2);
			PdfPCell cell3 = new PdfPCell(p3);
			PdfPCell cell4 = new PdfPCell(p4);
			cell1.SetLeading(0.9f, 1.2f);
			cell2.SetLeading(0.9f, 1.2f);
			cell3.SetLeading(0.9f, 1.2f);
			cell4.SetLeading(0.9f, 1.2f);
			cell1.BorderWidth = 0;
			cell2.BorderWidth = 0;
			cell3.BorderWidth = 0;
			cell4.BorderWidth = 0;
            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);
            p1 = new Paragraph(new Phrase("Дата рождения", f2));         
            p2 = new Paragraph(new Phrase(_patient.Patient.BirthDay.Value.ToShortDateString(), f2));
            p3 = new Paragraph(new Phrase("Дата проведения", f2));
            p4 = new Paragraph(new Phrase(_patient.Patient.ResearchDateTime.Value.ToShortDateString(), f2));
            cell1 = new PdfPCell(p1);
            cell2 = new PdfPCell(p2);
            cell3 = new PdfPCell(p3);
            cell4 = new PdfPCell(p4);

            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);

            doc.Add(table);
			doc.Close();
		}

		private void CreateFolder()
		{
			_savePdfDocumentsPath.CreateFolder();
		}
	}


	public static class PdfExtension
	{
		public static void CreateFolder(this string str)
		{
			if (Directory.Exists(str))
				return;
			Directory.CreateDirectory(str);
		}
	}
}
