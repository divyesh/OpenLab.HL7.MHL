using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace OpenLab.HL7.Test
{
    public class HL7NewReportHeaderFooter : PdfPageEventHelper
    {
	//Git
        private string _logo;
        private BaseFont _helv;
        private PdfPTable _table;
        protected PdfTemplate _total;

        public HL7NewReportHeaderFooter()
        {
            _logo = @"Content\logo.tif";
        }
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            _total = writer.DirectContent.CreateTemplate(100, 100);
            _total.BoundingBox = new Rectangle(-20, -20, 100, 100);
            try
            {
                _helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            }
            catch
            {
                throw new DocumentException();
            }
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            Image logo = Image.GetInstance(_logo);
            logo.SetAbsolutePosition(document.Left, document.Top + 4);
            logo.ScalePercent(15f);
            document.Add(logo);
            const string head = "MEDICAL LABORATORIES LTD.";
            const string address = "1234 Wonderful Ave. West, Toronto M6A 1B2";
            const string tel = "Tel: (416) 567-7273 Fax:(416) 567-7657";
            cb.SaveState();
            cb.SetFontAndSize(_helv, 12);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(head, PDFFonts.TimesRoman(18, Font.BOLD)), (document.Left + 45), document.Top + 20, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(address, PDFFonts.TimesRoman(10, Font.NORMAL)), (document.Left + 45), document.Top + 10, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(tel, PDFFonts.TimesRoman(10, Font.NORMAL)), (document.Left + 45), document.Top, 0);

            var table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.SpacingAfter = 103f;
            table.DefaultCell.BorderColor = BaseColor.WHITE;
            table.SetTotalWidth(new float[] { 0 });
            table.AddCell("");
            document.Add(table);

            cb.RestoreState();
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            _table = (PdfPTable) Properties.Settings.Default["HL7NewReportHeader"];
            _table.WriteSelectedRows(0, -1, document.Left, document.Top - 10, writer.DirectContent);


            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();

            String text = "Page " + writer.PageNumber + " of";
            float textBase = document.Bottom - 20;
            float textSize = _helv.GetWidthPoint(text, 12);
            cb.BeginText();
            cb.SetFontAndSize(_helv, 10);

            float adjust = _helv.GetWidthPoint("0", 12);
            cb.SetTextMatrix((document.Right - 10) - textSize - adjust, textBase);
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(_total, (document.Right - 12) - adjust, textBase);
            var note = new Paragraph(new Chunk("IF GENDER IS NOT PROVIDED, NEITHER NORMAL RANGES WILL BE INDICATED NOR RESULT WILL FLAG", PDFFonts.Courier(8, Font.NORMAL)));
            var lab = new Paragraph(new Chunk("Confidentiality Notice: The contents of this document are confidential and intended only for the use of", PDFFonts.Courier(8, Font.BOLD)));
            var lab2 = new Paragraph(new Chunk("doctor(s) named above. If you have received this information in error, please notify immiditely by ", PDFFonts.Courier(8, Font.BOLD)));
            var lab3 = new Paragraph(new Chunk("telephone at the above number.", PDFFonts.Courier(8, Font.BOLD)));
            var printed = new Paragraph(new Chunk("PRINTED DATE: " + (DateTime.Now), PDFFonts.Courier(8, Font.NORMAL)));
            ColumnText.ShowTextAligned(cb, Element.ALIGN_MIDDLE, lab, document.Left, document.Bottom - 5, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_MIDDLE, lab2, document.Left, document.Bottom - 10, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_MIDDLE, lab3, document.Left, document.Bottom - 15, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_MIDDLE, note, document.Left, document.Bottom - 25, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_RIGHT, printed, document.Right, document.Top, 0);
            cb.RestoreState();
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            _total.BeginText();
            _total.SetFontAndSize(_helv, 10);
            _total.SetTextMatrix(0, 0);
            _total.ShowText((writer.PageNumber - 1).ToString());
            _total.EndText();
        }
    }
}
