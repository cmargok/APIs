namespace ChuckNorris.Models
{
    public class ChuckResponse
    {
        public string[] categories { get; set; }
        public string created_at { get; set; }
        public string icon_url { get; set; }
        public string id { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string value { get; set; }

        public ChuckResponse()
        {
            icon_url = string.Empty;
            id = string.Empty;
            value = string.Empty;
            url = string.Empty;
            created_at = string.Empty;
            updated_at = string.Empty;

        }



    }
}
