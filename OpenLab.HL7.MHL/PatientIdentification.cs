using System;

namespace OpenLab.HL7.MHL
{
    public class PatientIdentification
    {
        public string SetId { get; set; }
        public string PatientIdFromPhysician { get; set; }
        public string PatientIdLabAccessionNumber { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public string VersionCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MothersMaidenName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string PatientAlias { get; set; }
        public string Race { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string HomePhone { get; set; }
        public string BusinessPhone { get; set; }
        public string PrimaryLanguage { get; set; }
        public string MaritialStatus { get; set; }
        public string Religion { get; set; }
        public string PatientAccountNumber { get; set; }
        public string SocialInsuranceNumber { get; set; }
        public string DriversLicense { get; set; }
        public string MotherIdentifier { get; set; }
        public string EthicGroup { get; set; }
        public string BirthPlace { get; set; }
        public string MultipleBirthIndicator { get; set; }
        public string BirthOrder { get; set; }
        public string CitizenShip { get; set; }
        public string VeteransMilitaryStatus { get; set; }
        public string Nationality { get; set; }
        public DateTime? PatientDeathDateTime { get; set; }
        public string DeathIndicator { get; set; }
        private string TrimIt(string val)
        {
            if (val == null || val.Trim() == "")
                return "";
            return val;
        }
        public PatientIdentification Read(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] arr = line.Split('|');
                var pid = new PatientIdentification();
                pid.SetId = arr[1];
                pid.PatientIdFromPhysician = arr[2];
                pid.PatientIdLabAccessionNumber = arr[3];
                if (arr[4].IndexOf('^') > 0)
                {
                    pid.HealthInsuranceNumber = arr[4].Replace('^', ' ');
                }
                else
                {
                    pid.HealthInsuranceNumber = arr[4];
                }
                string[] patienName = arr[5].Split('^');
                pid.LastName = patienName[0];
                pid.FirstName = patienName[1];
                if (patienName.Length > 2)
                {
                    pid.MiddleName = patienName[2];
                }
                pid.MothersMaidenName = arr[6];
                if (!string.IsNullOrEmpty(arr[7]))
                {

                    int y = int.Parse(arr[7].Substring(0, 4));
                    int m = int.Parse(arr[7].Substring(4, 2));
                    int d = int.Parse(arr[7].Substring(6, 2));

                    var dt = new DateTime(y, m, d);
                    pid.DateOfBirth = dt;
                }
                else
                {
                    pid.DateOfBirth = null;
                }
                pid.Sex = arr[8];
                pid.PatientAlias = arr[9];
                pid.Race = arr[10];

                string[] address = arr[11].Split('^');
                pid.Address1 = address[0];
                pid.Address2 = address[1];
                pid.City = address[2];
                pid.Province = address[3];

                pid.CountryCode = arr[12];
                pid.HomePhone = arr[13];
                pid.BusinessPhone = arr[14];
                pid.PrimaryLanguage = arr[15];
                pid.MaritialStatus = arr[16];
                pid.Religion = arr[17];
                pid.PatientAccountNumber = arr[18];
                pid.SocialInsuranceNumber = arr[19];
                pid.DriversLicense = arr[20];
                pid.MotherIdentifier = arr[21];
                pid.EthicGroup = arr[22];
                pid.BirthPlace = arr[23];
                pid.MultipleBirthIndicator = arr[24];
                pid.BirthOrder = arr[25];
                pid.CitizenShip = arr[26];
                pid.VeteransMilitaryStatus = arr[27];
                pid.Nationality = arr[28];
                if (!string.IsNullOrEmpty(arr[29]))
                {

                    int y = int.Parse(arr[29].Substring(0, 4));
                    int m = int.Parse(arr[29].Substring(4, 2));
                    int d = int.Parse(arr[29].Substring(6, 2));

                    var dt = new DateTime(y, m, d);
                    pid.PatientDeathDateTime = dt;
                }
                else
                {
                    pid.PatientDeathDateTime = null;
                }
                pid.DeathIndicator = arr[30];
                return pid;
            }
            return null;
        }


    }
}
