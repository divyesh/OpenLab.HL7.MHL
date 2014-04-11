using System;
using System.Text;

namespace OpenLab.HL7.MHL
{
    public class MessageHeader
    {
        public string FieldSeparator1 { get; set; }
        public string EncodingCharacters2 { get; set; }
        public string SendingApplication3 { get; set; }
        public string SendingFacility4 { get; set; }
        public string ReceivingApplication5 { get; set; }
        public string ReceivingFacility6 { get; set; }
        public DateTime? DateTimeOfMessage7 { get; set; }
        public string Security8 { get; set; }
        public string MessageType9 { get; set; }
        public string MessageControlID10 { get; set; }
        public string ProcessingID11 { get; set; }
        public string VersionID12 { get; set; }
        public string SequenceNumber13 { get; set; }
        public string ContinuationPointer14 { get; set; }
        public string AcceptAcknowledgementType15 { get; set; }
        public string ApplicationAcknowledgementType16 { get; set; }
        public string CountryCode17 { get; set; }
        public string CharacterSet18 { get; set; }
        public string PrincipalLanguageOfMessage19 { get; set; }
        public MessageHeader Read(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] arr = line.Split('|');
                var msh = new MessageHeader();
                msh.FieldSeparator1 = arr[1];
                msh.SendingApplication3 = arr[2];
                msh.SendingFacility4 = arr[3];
                msh.SendingFacility4 = arr[4];
                msh.ReceivingApplication5 = arr[5];
                msh.ReceivingFacility6 = arr[6];
                if (!string.IsNullOrEmpty(arr[7]))
                {

                    int y = int.Parse(arr[7].Substring(0, 4));
                    int m = int.Parse(arr[7].Substring(4, 2));
                    int d = int.Parse(arr[7].Substring(6, 2));

                    var dt = new DateTime(y, m, d);
                    msh.DateTimeOfMessage7 = dt;
                }
                else
                {
                    msh.DateTimeOfMessage7 = null;
                }
                msh.Security8 = arr[8];
                msh.MessageType9 = arr[9];
                msh.MessageControlID10 = arr[10];
                msh.ProcessingID11 = arr[11];
                msh.VersionID12 = arr[12];
                msh.SequenceNumber13 = arr[13];
                msh.ContinuationPointer14 = arr[14];
                msh.AcceptAcknowledgementType15 = arr[15];
                msh.ApplicationAcknowledgementType16 = arr[16];
                msh.CountryCode17 = arr[17];
                msh.CharacterSet18 = arr[18];
                if (arr.Length > 19)
                    msh.PrincipalLanguageOfMessage19 = arr[19];
                return msh;
            }
            return null;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("MSH");
            sb.Append(FieldSeparator1);
            sb.Append(EncodingCharacters2);
            sb.Append("|");
            sb.Append(SendingApplication3);
            sb.Append("|");
            sb.Append(SendingFacility4);
            sb.Append("|");
            sb.Append(ReceivingApplication5);
            sb.Append("|");
            sb.Append(ReceivingFacility6);
            sb.Append("|");
            sb.Append(DateTimeOfMessage7 != null ? ((DateTime)DateTimeOfMessage7).ToString("yyyyMMddHHMM") : "");
            sb.Append("|");
            sb.Append(Security8);
            sb.Append("|");
            sb.Append(MessageType9);
            sb.Append("|");
            sb.Append(MessageControlID10);
            sb.Append("|");
            sb.Append(ProcessingID11);
            sb.Append("|");
            sb.Append(VersionID12);
            sb.Append("|");
            sb.Append(SequenceNumber13);
            sb.Append("|");
            sb.Append(ContinuationPointer14);
            sb.Append("|");
            sb.Append(AcceptAcknowledgementType15);
            sb.Append("|");
            sb.Append(ApplicationAcknowledgementType16);
            sb.Append("|");
            sb.Append(CountryCode17);
            sb.Append("|");
            sb.Append(CharacterSet18);
            sb.Append("|");
            sb.Append(PrincipalLanguageOfMessage19);
            return sb.ToString();

        }
    }
}
