namespace JwtStore.Core
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new SecretsConfiguration();
        public static EmailConfiguration Email { get; set; } = new EmailConfiguration();
        public static SendGridConfiguration SendGrid { get; set; } = new SendGridConfiguration();
        public static DatabaseConfiguration Database { get; set; } = new DatabaseConfiguration();
        public class DatabaseConfiguration
        {
            public string ConnectionString { get; set; } = string.Empty;
        }
        public class EmailConfiguration
        {
            public string DefaultFromEmail { get; set; } = "test@leo.com";
            public string DefaultFromName { get; set; } = "leo";

        }
        public class SendGridConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
        }
        public class SecretsConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
            public string JwtPrivateKey { get; set; } = string.Empty;
            public string PasswordSaltKey { get; set; } = string.Empty;
        }
    }
}
