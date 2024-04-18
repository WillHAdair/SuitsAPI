namespace API.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsReady { get; set; }
        public bool IsAdmin { get; set; }
        public enum DemoType { Mammoth, Wiggle, Undefined }
        public DemoType Demo { get; set; }

        public User(string username, string password, bool isReady, bool isAdmin, DemoType demo)
        {
            Username = username;
            Password = password;
            IsReady = isReady;
            IsAdmin = isAdmin;
            Demo = demo;
        }
    }
}
