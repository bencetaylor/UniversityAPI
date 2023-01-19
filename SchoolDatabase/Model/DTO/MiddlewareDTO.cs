namespace SchoolDatabase.Model.DTO
{
    public class MiddlewareDTO
    {
        public string statusCode { get; set; }
        public object content { get; set; }
        public string identity { get; set; }
        public MiddlewareDTO(string statusCode, object content, string identity)
        {
            this.statusCode = statusCode;
            this.content = content;
            this.identity = identity;
        }
    }
}
