using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.util;
using OpenLab.HL7.MHL;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace OpenLab.HL7.Test
{
    public class HL7ToPDF
    {
        private readonly string _pdfFilePath;
        private readonly Report _pr;
        public HL7ToPDF(string hl7FilePath,string pdfFilePath)
        {
            _pr = new Report().ReadHL7(hl7FilePath);
            _pdfFilePath = pdfFilePath;
        }
        public void CreatePDF()
        {
            var doc = new Document(PageSize.LETTER);
            var writer = PdfWriter.GetInstance(doc, System.IO.File.Create(_pdfFilePath));
            writer.CloseStream = false;

            Properties.Settings.Default["HL7NewReportHeader"] = WriteHeader();
            
            writer.PageCount = 0;
            var e = new HL7NewReportHeaderFooter();
            writer.PageEvent = e;
            doc.Open();

            WriteTests(doc);
            WriteComment(doc);
            if (doc.IsOpen())
            {
                doc.Close();
            }
        }
        private void WriteTests(Document d)
        {
            var tstGrp = from obxs in _pr.ObservationResultXs
                         group obxs by obxs.TestGroup into grp
                         select new { grp.Key, Other = grp };

            foreach (var obr in tstGrp)
            {
                var t = new PdfPTable(4);
                t.DefaultCell.Padding = 5;
                float[] headerwidths = { 27, 25, 15, 25 };
                t.SetWidths(headerwidths);
                t.WidthPercentage = 100;
                var ch = new PdfPCell(new Phrase(obr.Key, PDFFonts.TimesRoman(12, Font.BOLD | Font.UNDERLINE)));
                ch.Colspan = 4;
                ch.BorderColor = BaseColor.WHITE;


                if (obr.Key.Contains("Comment"))
                {
                    var cmt = obr.Other.FirstOrDefault();
                    if (cmt != null)
                    {
                        d.Add(new Paragraph(new Phrase("COMMENT", PDFFonts.TimesRoman(12, Font.BOLD | Font.UNDERLINE))));
                        var par = new Paragraph(new Chunk(cmt.ObservationValue));
                        d.Add(par);
                    }

                }
                else
                {
                    t.AddCell(ch);

                    var subIdgrp = (from ot in obr.Other
                                    group ot by ot.TestName into sg
                                    select new { sg.Key, Other = sg }).ToArray();
                    foreach (var s in subIdgrp)
                    {
                        foreach (var ob in s.Other)
                        {
                            if (ob.ObservationSubId == null || ob.ObservationSubId == 1)
                            {

                                var result = ob;
                                var flag = string.IsNullOrEmpty(result.AbnormalFlags)
                                               ? ""
                                               : result.AbnormalFlags.Equals("H")
                                                     ? "HIGH"
                                                     : result.AbnormalFlags.Equals("L") ? "LOW" : "";
                                var c = new PdfPCell(new Phrase(result.TestName, PDFFonts.Times(11, Font.NORMAL)));
                                c.Padding = 2;
                                c.BorderColor = BaseColor.WHITE;
                                t.AddCell(c);
                                c = new PdfPCell(new Phrase(result.ObservationValue, PDFFonts.Times(11, Font.NORMAL)));
                                c.BorderColor = BaseColor.WHITE;
                                t.AddCell(c);
                                c = new PdfPCell(new Phrase(flag, PDFFonts.Times(11, Font.BOLD | Font.UNDERLINE)));
                                c.BorderColor = BaseColor.WHITE;
                                t.AddCell(c);
                                c =
                                    new PdfPCell(new Phrase(result.ReferenceRange + " " + result.Units,
                                                            PDFFonts.Times(11, Font.NORMAL)));
                                c.BorderColor = BaseColor.WHITE;
                                t.AddCell(c);
                            }
                            else
                            {
                                var c =
                                    new PdfPCell(new Phrase(ob.ObservationValue, PDFFonts.TimesRoman(12, Font.NORMAL)));
                                c.BorderColor = BaseColor.WHITE;

                                c.HorizontalAlignment = Element.ALIGN_CENTER;
                                c.Colspan = 4;
                                t.AddCell(c);
                            }
                        }
                    }
                }
                d.Add(t);
            }
        }
        private void WriteComment(Document d)
        {
            if (_pr.NTEs != null && _pr.NTEs.Count>0)
            {
                foreach (var ntE in _pr.NTEs)
                {
                    var par = new Paragraph(new Chunk(ntE.Comment));
                    d.Add(par);    
                }
                
            }
        }
        private PdfPTable WriteHeader()
        {
            var tb = new PdfPTable(4);
            tb.WidthPercentage = 100;
            tb.SpacingBefore = 1000f;
            tb.DefaultCell.BorderWidth = 1;
            tb.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tb.SetTotalWidth(new float[] { 120, 100, 190, 130 });
            tb.AddCell(WritePatient());
            tb.AddCell(WriteLabInfo());
            tb.AddCell(WriteServiceInfo());
            tb.AddCell(WritePhysicians());
            var th = new PdfPTable(4);
            th.DefaultCell.PaddingBottom = 5;
            th.DefaultCell.BackgroundColor = new BaseColor(102, 204, 255);
            float[] headerwidths = { 25, 25, 15, 25 };
            th.SetWidths(headerwidths);
            th.WidthPercentage = 100;
            th.AddCell(new Phrase("TEST NAME", PDFFonts.TimesRoman(12, Font.BOLD)));
            th.AddCell(new Phrase("RESULT", PDFFonts.TimesRoman(12, Font.BOLD)));
            th.AddCell(new Phrase("FLAGS", PDFFonts.TimesRoman(12, Font.BOLD)));
            th.AddCell(new Phrase("REFERENCE RANGE", PDFFonts.TimesRoman(12, Font.BOLD)));
            th.DefaultCell.BackgroundColor = BaseColor.WHITE;
            var c = new PdfPCell(th);
            c.Colspan = 4;
            tb.AddCell(c);
            return tb;
        }
        private Phrase WritePatient()
        {
            PatientIdentification pid = _pr.PatientIdentification;
            var ch = new Chunk("PATIENT:\n", PDFFonts.Courier(10, Font.BOLDITALIC));
            var pi = new Phrase();
            pi.Add(ch);
            if (pid != null)
            {
                pi.Add(new Chunk(pid.LastName + " " + pid.FirstName, PDFFonts.Courier(10, Font.BOLD)));
                pi.Add(new Chunk("\n"));
                if (!string.IsNullOrEmpty(pid.Address1))
                {
                    pi.Add(new Chunk(pid.Address1, PDFFonts.Courier(9, Font.NORMAL)));
                }
                if (!string.IsNullOrEmpty(pid.Address2))
                {
                    pi.Add(new Chunk("\n" + pid.Address2, PDFFonts.Courier(9, Font.NORMAL)));
                }
                if (!string.IsNullOrEmpty(pid.City))
                {
                    pi.Add(new Chunk("\n" + pid.City, PDFFonts.Courier(9, Font.NORMAL)));
                }
                if (!string.IsNullOrEmpty(pid.Province))
                {
                    pi.Add(new Chunk("," + pid.Province, PDFFonts.Courier(9, Font.NORMAL)));
                }
                if (!string.IsNullOrEmpty(pid.PostalCode))
                {
                    pi.Add(new Chunk("," + pid.PostalCode, PDFFonts.Courier(9, Font.NORMAL)));
                }
                if (!string.IsNullOrEmpty(pid.HomePhone))
                {
                    pi.Add(new Chunk("\n(H)" + pid.HomePhone, PDFFonts.Courier(9, Font.NORMAL)));
                }
                if (!string.IsNullOrEmpty(pid.BusinessPhone))
                {
                    pi.Add(new Chunk("\n(B)" + pid.BusinessPhone, PDFFonts.Courier(9, Font.NORMAL)));
                }
                return pi;
            }
            return null;
        }
        private Phrase WriteLabInfo()
        {
            PatientIdentification pid = _pr.PatientIdentification;
            ObservationRequest obr = _pr.ObservationRequest.FirstOrDefault();
            var li = new Phrase();
            var ch = new Chunk("ACCESSION#: ", PDFFonts.Courier(10, Font.BOLDITALIC));
            li.Add(ch);
            li.Add(new Chunk("\n"));
            li.Add(new Chunk(pid.PatientIdLabAccessionNumber, PDFFonts.Courier(9, Font.NORMAL)));
            li.Add(new Chunk("\n"));
            li.Add(new Chunk("Health#: ", PDFFonts.Courier(9, Font.BOLDITALIC)));
            li.Add(new Chunk(pid.HealthInsuranceNumber, PDFFonts.Courier(9, Font.NORMAL)));

            li.Add(new Chunk("\n"));
            li.Add(new Chunk("BIRTH DATE: ", PDFFonts.Courier(9, Font.BOLDITALIC)));
            if (pid.DateOfBirth != null)
            {
                li.Add(new Chunk("\n" + DateTime.Parse(pid.DateOfBirth.ToString()).ToString("dd-MMM-yyyy").ToUpper(), PDFFonts.Courier(9, Font.NORMAL)));
                li.Add(new Chunk("\n"));
                li.Add(new Chunk("AGE: ", PDFFonts.Courier(9, Font.BOLDITALIC)));
                li.Add(new Chunk(Age(pid.DateOfBirth, obr.RequestedDateTime).ToString() + " YEARS(S)", PDFFonts.Courier(9, Font.NORMAL)));
            }
            li.Add(new Chunk("\n"));
            li.Add(new Chunk("SEX: ", PDFFonts.Courier(9, Font.BOLDITALIC)));
            if (pid.Sex != null)
            {
                li.Add(new Chunk(pid.Sex, PDFFonts.Courier(9, Font.NORMAL)));
            }
            return li;
        }
        private Phrase WriteServiceInfo()
        {
            ObservationRequest obr = _pr.ObservationRequest.FirstOrDefault();
            var si = new Phrase();
            si.Add(new Chunk("SERVICE DATE: ", PDFFonts.Courier(10, Font.BOLDITALIC)));
            si.Add(new Chunk(DateTime.Parse(obr.RequestedDateTime.ToString()).ToString("yyyy/MM/dd"), PDFFonts.Courier(10, Font.NORMAL)));
            si.Add(new Chunk("\n"));
            si.Add(new Chunk("COLLECTION DATE TIME: ", PDFFonts.Courier(10, Font.BOLDITALIC)));
            si.Add(new Chunk("\n"));
            si.Add(new Chunk(DateTime.Parse(obr.RequestedDateTime.ToString()).ToString("yyyy/MM/dd HH:mm:ss"), PDFFonts.Courier(10, Font.NORMAL)));
            si.Add(new Chunk("\n"));
            si.Add(new Chunk("STATUS: ", PDFFonts.Courier(10, Font.BOLDITALIC)));
            var status = ReportStatus.Status().Where(s => s.Key.Equals(obr.ResultStatus[0])).FirstOrDefault().Value;

            si.Add(new Chunk(status, PDFFonts.Courier(10, Font.NORMAL)));
            si.Add(new Chunk("\n"));
            return si;
        }
        private Phrase WritePhysicians()
        {
            IList<ObservationRequest> obr = _pr.ObservationRequest;
            var pyi = new Phrase();
            pyi.Add(new Chunk("PHYSICIAN(S) :\n", PDFFonts.Courier(10, Font.BOLDITALIC)));
            if (obr != null)
            {
                foreach (var ob in obr)
                {
                    var phyName = ob.PhysicianFirstName + " " + ob.PhysicianLastName;

                    var fnt = phyName.Length > 22
                                  ? PDFFonts.Courier(10, Font.NORMAL)
                                  : PDFFonts.Courier(12, Font.NORMAL);
                    pyi.Add(new Chunk(phyName, fnt));
                    pyi.Add(new Chunk("\n"));
                }
            }
            return pyi;
        }
        public static int Age(DateTime? birthDate, DateTime? serviceDate)
        {
            if (birthDate == null || serviceDate == null)
            {
                return 0;
            }
            else
            {
                var dob = (DateTime)birthDate;
                var srvdt = (DateTime)serviceDate;
                int age = srvdt.Year - dob.Year;
                if (srvdt.Month < dob.Month || (srvdt.Month == dob.Month && srvdt.Day < dob.Day))
                    age--;
                return age;
            }
        }
    }
}
