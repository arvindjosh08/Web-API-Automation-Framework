// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CreateAccountReqDto
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string company { get; set; }
        public string address1 { get; set; }
        public string country { get; set; }
        public int zipcode { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public int mobile_number { get; set; }
    }

