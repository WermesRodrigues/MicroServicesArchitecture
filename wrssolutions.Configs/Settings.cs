namespace wrssolutions.Configs
{
    public static class Settings
    {
        public static readonly string JwtSecret = "iju2bqfayg46ugnpwn5";
        public static readonly int JwtExpiration = 3600;
        public static readonly string JwtEmissor = "WrsSolutions";
        public static string JwtValidate = "";
        public static string RabbitMQEventMessages = "messages";
    }
}