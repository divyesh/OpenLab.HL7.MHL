using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenLab.HL7.MHL
{
    public class PatientReport
    {
        private StreamReader objReader;

        public PatientReport(Stream filestream)
            : this(filestream, null)
        {
        }

        public PatientReport(Stream filestream, Encoding enc)
        {
            //check the Pass Stream whether it is readable or not
            if (!filestream.CanRead)
            {
                return;
            }
            objReader = (enc != null) ? new StreamReader(filestream, enc) : new StreamReader(filestream);
        }

        public PatientReport(StreamReader sr)
        {
            objReader = sr;
        }
        public Report GetReport()
        {
            var pr = new Report();
            pr.ObservationRequest=new List<ObservationRequest>();
            pr.ObservationResultXs = new List<ObservationResultX>();
            try
            {
                using (var sr = objReader) // new StreamReader(@"C:\Users\divyeshk\Desktop\PatientXMLTest\hl7.txt"))
                {
                    String line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        switch (line.Substring(0, 3))
                        {
                            case "MSH":
                                pr.MessageHeader = (new MessageHeader()).Read(line);
                                break;
                            case "PID":
                                {
                                    pr.PatientIdentification = (new PatientIdentification()).Read(line);
                                }
                                break;
                            case "OBR":
                                {
                                    var obr= (new ObservationRequest()).Read(line);
                                    pr.ObservationRequest.Add(obr);
                                }
                                break;
                            case "OBX":
                                {
                                    var obx = new ObservationResultX().Read(line);
                                    pr.ObservationResultXs.Add(obx);
                                }
                                break;

                        }
                        Console.WriteLine(line);
                    }
                   
                }
                return pr;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
