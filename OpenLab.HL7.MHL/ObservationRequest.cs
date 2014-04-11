using System;

namespace OpenLab.HL7.MHL
{
    public class ObservationRequest
    {
        public string SetId { get; set; }
        public string PlaceOrderNumber { get; set; }
        public string FillerOrderNumber { get; set; }
        public string UniverslServiceId { get; set; }
        public string SpecimenPriority { get; set; }
        public DateTime? RequestedDateTime { get; set; }
        public DateTime? ObservationDateTime { get; set; }
        public DateTime? ObservationEndDateTime { get; set; }
        public string CollectionVolume { get; set; }
        public string CollectionIdentifier { get; set; }
        public string SpecimenActionCode { get; set; }
        public string DangerCode { get; set; }
        public string RelevantClinicalInfo { get; set; }
        public DateTime? SpecimenReceivedDateTime { get; set; }
        public string SpecimenSource { get; set; }
        public string PhysicianNumber { get; set; }
        public string PhysicianLastName { get; set; }
        public string PhysicianFirstName { get; set; }
        public string OrderingProvider { get; set; }
        public string OrderCallBackPhone { get; set; }
        public string PlacerField1 { get; set; }
        public string PlacerField2 { get; set; }
        public string FillerField1 { get; set; }
        public string FillerField2 { get; set; }
        public DateTime? ResultingReportStatusChangeDateTime { get; set; }
        public string ChargeToPractice { get; set; }
        public string DiagnosticServiceSectionId { get; set; }
        public string ResultStatus { get; set; }
        public string ParentResult { get; set; }
        public string QuantityTiming { get; set; }
        public string ResultCopiesTo { get; set; }
        public string ParentNumber { get; set; }
        public string TransportationMode { get; set; }
        public string ReasonForStudy { get; set; }
        public string PrincipalResultInterpreter { get; set; }
        public string AssistantResultInterpreter { get; set; }
        public string Technician { get; set; }
        public string Transcriptionist { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public int? NumberOfSampleContainers { get; set; }
        public string TransportLogisticsOfCollectedSample { get; set; }
        public string CollectorsComment { get; set; }
        public string TransportArrangementResponsibility { get; set; }
        public string TransportArranged { get; set; }
        public string EscortRequired { get; set; }
        public string PlannedPatientTransportComment { get; set; }
        private string TrimIt(string val)
        {
            if (val == null || val.Trim() == "")
                return "";
            return val;
        }
        public ObservationRequest Read(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] arr = line.Split('|');
                if (arr.Length > 0)
                {
                    var obr = new ObservationRequest();
                    obr.SetId = arr[1];
                    obr.PlaceOrderNumber = arr[2];
                    obr.FillerOrderNumber = arr[3];
                    obr.UniverslServiceId = arr[4];
                    obr.SpecimenPriority = arr[5];
                    if (!string.IsNullOrEmpty(arr[6]))
                    {

                        int y = int.Parse(arr[6].Substring(0, 4));
                        int m = int.Parse(arr[6].Substring(4, 2));
                        int d = int.Parse(arr[6].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obr.RequestedDateTime = dt;
                    }
                    else
                    {
                        obr.RequestedDateTime = null;
                    }
                    if (!string.IsNullOrEmpty(arr[7]))
                    {

                        int y = int.Parse(arr[7].Substring(0, 4));
                        int m = int.Parse(arr[7].Substring(4, 2));
                        int d = int.Parse(arr[7].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obr.ObservationDateTime = dt;
                    }
                    else
                    {
                        obr.ObservationDateTime = null;
                    }
                    if (!string.IsNullOrEmpty(arr[7]))
                    {

                        int y = int.Parse(arr[8].Substring(0, 4));
                        int m = int.Parse(arr[8].Substring(4, 2));
                        int d = int.Parse(arr[8].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obr.ObservationEndDateTime = dt;
                    }
                    else
                    {
                        obr.ObservationEndDateTime = null;
                    }
                    obr.CollectionVolume = arr[9];
                    obr.CollectionIdentifier = arr[10];
                    obr.SpecimenActionCode = arr[11];
                    obr.DangerCode = arr[12];
                    obr.RelevantClinicalInfo = arr[13];

                    if (!string.IsNullOrEmpty(arr[14]))
                    {

                        int y = int.Parse(arr[14].Substring(0, 4));
                        int m = int.Parse(arr[14].Substring(4, 2));
                        int d = int.Parse(arr[14].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obr.SpecimenReceivedDateTime = dt;
                    }
                    else
                    {
                        obr.SpecimenReceivedDateTime = null;
                    }
                    obr.SpecimenSource = arr[15];
                    string[] phy = arr[16].Split('^');
                    obr.PhysicianLastName = phy[1];
                    obr.PhysicianFirstName = phy[2];
                    obr.OrderingProvider = string.IsNullOrEmpty(arr[16]) ? "" : arr[16].Replace('^', ' ');
                    obr.OrderCallBackPhone = arr[17];
                    obr.PlacerField1 = arr[18];
                    obr.PlacerField2 = arr[19];
                    obr.FillerField1 = arr[20];
                    obr.FillerField2 = arr[21];
                    if (!string.IsNullOrEmpty(arr[22]))
                    {

                        int y = int.Parse(arr[22].Substring(0, 4));
                        int m = int.Parse(arr[22].Substring(4, 2));
                        int d = int.Parse(arr[22].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obr.ResultingReportStatusChangeDateTime = dt;
                    }
                    else
                    {
                        obr.ResultingReportStatusChangeDateTime = null;
                    }
                    obr.ChargeToPractice = arr[23];
                    obr.DiagnosticServiceSectionId = arr[24];
                    obr.ResultStatus = arr[25];
                    obr.ParentResult = arr[26];
                    obr.QuantityTiming = arr[27];
                    obr.ResultCopiesTo = arr[28];
                    obr.ParentNumber = arr[29];
                    obr.TransportationMode = arr[30];
                    obr.ReasonForStudy = arr[31];
                    obr.PrincipalResultInterpreter = arr[32];
                    obr.AssistantResultInterpreter = arr[33];
                    obr.Technician = arr[34];
                    obr.Transcriptionist = arr[35];
                    if (!string.IsNullOrEmpty(arr[36]))
                    {

                        int y = int.Parse(arr[36].Substring(0, 4));
                        int m = int.Parse(arr[36].Substring(4, 2));
                        int d = int.Parse(arr[36].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obr.ScheduledDateTime = dt;
                    }
                    else
                    {
                        obr.ScheduledDateTime = null;
                    }
                    obr.NumberOfSampleContainers = string.IsNullOrEmpty(arr[37]) ? null : (int?)int.Parse(arr[37]);
                    obr.TransportLogisticsOfCollectedSample = arr[38];
                    obr.CollectorsComment = arr[39];
                    obr.TransportArrangementResponsibility = arr[40];
                    obr.TransportArranged = arr[41];
                    obr.EscortRequired = arr[42];
                    obr.PlannedPatientTransportComment = arr[43];
                    return obr;
                }
            }

            return null;
        }
    }
}
