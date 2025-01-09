namespace lab.azure_active_directory_auth.Utility
{
    public class EmailConfig
    {
        public static string Name = "EmailConfig";
        public bool IsEmailSend { get; set; }
        public string? FromEmailAddress { get; set; }
        public string? DisplayName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int? Port { get; set; }
        public bool? EnableSsl { get; set; }
        public string? TestEmailAddress { get; set; }
    }
}
