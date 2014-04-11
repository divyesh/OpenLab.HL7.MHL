using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenLab.HL7.Test
{
    public static class ReportStatus
    {
        public static IDictionary<char, string> Status()
        {
            var status = new Dictionary<char, string>();
            status.Add('O', "Order received; specimen not yet received");
            status.Add('I', "No results available; specimen received, producer incomplete");
            status.Add('S', "No results available; procedure scheduled, but not done");
            status.Add('A', "Some, but not all, results available");
            status.Add('P', "Preliminary: A verified early result is available, final results not yet obtained");

            status.Add('C', "No results available; procedure scheduled, but not done");
            status.Add('R', "Results stored; not yet verified");
            status.Add('F', "Final results; results stored and verified. Can only be change with a corrected result.");
            status.Add('X', "No results available; Order canceled.");
            status.Add('Y', "No order on record for this test. (Used only on queries)");
            status.Add('Z', "No record of this patient.( Used only on queries)");

            return status;
        }
    }
}
