namespace OpenLab.HL7.MHL
{
    public class NTE
    {
        public string SetId { get; set; }
        public string SourceOfComment { get; set; }
        public string Comment { get; set; }
        public string CommentType { get; set; }
       
        public NTE Read(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] arr = line.Split('|');
                if(arr.Length>0)
                {
                    var nte = new NTE();
                    nte.SetId = arr[1];
                    nte.SourceOfComment = arr[2];
                    nte.Comment = arr[3];
                    nte.CommentType = arr[4];
                    return nte;
                }
            }
            return null;
        }
       
    }
    
}
