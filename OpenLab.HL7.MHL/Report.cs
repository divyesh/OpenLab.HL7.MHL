using System;
using System.Collections.Generic;

namespace OpenLab.HL7.MHL
{
    public class Report
    {
        public MessageHeader MessageHeader { get; set; }
        public PatientIdentification PatientIdentification { get; set; }
        public IList<ObservationRequest> ObservationRequest { get; set; }
        public IList<ObservationResultX> ObservationResultXs { get; set; }
        public IList<NTE> NTEs { get; set; }

        public Report ReadHL7(string hl7FilePath)
        {
            var objReader = new System.IO.StreamReader(hl7FilePath);
            var pr = new Report();
            pr.ObservationRequest = new List<ObservationRequest>();
            pr.ObservationResultXs = new List<ObservationResultX>();
            try
            {
                using (var sr = objReader)
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
                                    var obr = (new ObservationRequest()).Read(line);
                                    pr.ObservationRequest.Add(obr);
                                }
                                break;
                            case "OBX":
                                {
                                    var obx = new ObservationResultX().Read(line);
                                    pr.ObservationResultXs.Add(obx);
                                }
                                break;
                            case "NTE":
                                {
                                    var nte = new NTE().Read(line);
                                    pr.NTEs.Add(nte);
                                }
                                break;

                        }
                        //Console.WriteLine(line);
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
