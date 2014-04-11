using System;

namespace OpenLab.HL7.MHL
{
    public class ObservationResultX
    {
        public string SetId { get; set; }
        public string ValueType { get; set; }
        public string ObservationIdentifier { get; set; }
        public string TestGroup { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int? ObservationSubId { get; set; }
        public string ObservationValue { get; set; }
        public string Units { get; set; }
        public string ReferenceRange { get; set; }
        public string AbnormalFlags { get; set; }
        public string Probability { get; set; }
        public string NatureOfAbnormalFlag { get; set; }
        public string ObservationResultStatus { get; set; }
        public DateTime? DateLastObserverNormalValue { get; set; }
        public string UserDefinedAccessCheck { get; set; }
        public DateTime? DateTimeObservation { get; set; }
        public string ProducersId { get; set; }
        public string ResponsibleObserver { get; set; }
        public string ObservationMethod { get; set; }

        public ObservationResultX Read(string line)
        {
            if(!string.IsNullOrEmpty(line))
            {
                string[] arr = line.Split('|');
                if(arr.Length>0)
                {
                    var obx = new ObservationResultX();
                    obx.SetId = arr[1];
                    obx.ValueType = arr[2];
                    obx.ObservationIdentifier = arr[3];
                    string[] test = obx.ObservationIdentifier.Split('^');
                    
                    obx.TestGroup = test[0];
                    obx.TestCode = test[1];
                    if (test.Length ==3)
                    obx.TestName = test[2];
                    obx.ObservationSubId = string.IsNullOrEmpty(arr[4])?null:(int?)int.Parse(arr[4]);
                    obx.ObservationValue = arr[5];
                    obx.Units = arr[6];
                    obx.ReferenceRange = arr[7];
                    obx.AbnormalFlags = arr[8];
                    obx.Probability = arr[9];
                    obx.NatureOfAbnormalFlag = arr[10];
                    obx.ObservationResultStatus = arr[11];
                    if (!string.IsNullOrEmpty(arr[12]))
                    {
                        int y = int.Parse(arr[12].Substring(0, 4));
                        int m = int.Parse(arr[12].Substring(4, 2));
                        int d = int.Parse(arr[12].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obx.DateLastObserverNormalValue = dt;
                    }
                    else
                    {
                        obx.DateLastObserverNormalValue = null;
                    }
                    obx.UserDefinedAccessCheck = arr[13];
                    if (!string.IsNullOrEmpty(arr[14]))
                    {

                        int y = int.Parse(arr[14].Substring(0, 4));
                        int m = int.Parse(arr[14].Substring(4, 2));
                        int d = int.Parse(arr[14].Substring(6, 2));

                        var dt = new DateTime(y, m, d);
                        obx.DateTimeObservation = dt;
                    }
                    else
                    {
                        obx.DateLastObserverNormalValue = null;
                    }
                    obx.ProducersId = arr[15];
                    obx.ResponsibleObserver = arr[16];
                    obx.ObservationMethod = arr[17];
                    return obx;
                }
            }
            return null;
        }
        
    }
}
