namespace API.Codejourney.Authorization
{
    public class LoginResponse
    {
        public string TokenName { get; set; }

        public int id { get; set; }

        public DateTime ExpireAt { get; set; }

    }
}
