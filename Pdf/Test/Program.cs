/*
 * Создано в SharpDevelop.
 * Пользователь: vital_000
 * Дата: 15.09.2015
 * Время: 23:33
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing;

namespace Test
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			var doc = new Document();
PdfWriter.GetInstance(doc, new System.IO.FileStream( @".\Document.pdf", System.IO.FileMode.Create));
doc.Open();
iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance( @"./images.jpg");
jpg.Alignment = Element.ALIGN_CENTER;
doc.Add(jpg);
PdfPTable table = new PdfPTable(3);
PdfPCell cell = new PdfPCell(new Phrase("Simple table",
  new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16,
  iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
cell.BackgroundColor = new BaseColor(Color.Wheat);
cell.Padding = 5;
cell.Colspan = 3;
cell.HorizontalAlignment = Element.ALIGN_CENTER;
table.AddCell(cell);
table.AddCell("Col 1 Row 1");
table.AddCell("Col 2 Row 1");
table.AddCell("Col 3 Row 1");
table.AddCell("Col 1 Row 2");
table.AddCell("Col 2 Row 2");
table.AddCell("Col 3 Row 2");
jpg = iTextSharp.text.Image.GetInstance(/*Application.StartupPath +*/ @"./left.jpg");
cell = new PdfPCell(jpg);
cell.Padding = 5;
cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
table.AddCell(cell);
cell = new PdfPCell(new Phrase("Col 2 Row 3"));
cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
table.AddCell(cell);
jpg = iTextSharp.text.Image.GetInstance(/*Application.StartupPath*/  @"./right.jpg");
cell = new PdfPCell(jpg);
cell.Padding = 5;
cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
table.AddCell(cell);
doc.Add(table);
doc.Close();

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}