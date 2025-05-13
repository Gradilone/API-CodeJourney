namespace API.Codejourney.Authorization
{
    public class LoginResponse
    {
        public string TokenName { get; set; }

        public string Password { get; set; }

        public DateTime ExpireAt { get; set; }

    }
}
