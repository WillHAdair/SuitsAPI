using static suitsAPI.Models.User;

namespace suitsAPI.Models
{
    public static class UserBase
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static void AddUser(string apiKey, string userName, bool isAdmin, string demoType)
        {
            DemoType dt = GetDemoType(demoType);
            User user = new User(userName, apiKey, false, isAdmin, dt);
            Users.Add(user);
        }

        public static DemoType GetDemoType(string demoType)
        {
            switch (demoType)
            {
                case "Mammoth":
                    return DemoType.Mammoth;
                case "Wiggle":
                    return DemoType.Wiggle;
                default:
                    throw new ArgumentException("Invalid demo type");
            }
        }

        public static List<User> GetUserByDemoType(DemoType demoType)
        {
            return Users.Where(u => u.Demo == demoType).ToList();
        }

        public static List<User> GetReadyUsers(DemoType demoType)
        {
            return Users.Where(u => u.Demo == demoType && u.IsReady).ToList();
        }

        public static List<User> GetUnreadyUsers(DemoType demoType)
        {
            return Users.Where(u => u.Demo == demoType && !u.IsReady).ToList();
        }

        public static List<User> GetAdmins()
        {
            return Users.Where(u => u.IsAdmin).ToList();
        }
    }
}
