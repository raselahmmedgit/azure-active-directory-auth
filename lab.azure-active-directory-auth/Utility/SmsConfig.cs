namespace lab.azure_active_directory_auth.Utility
{
    public class SmsConfig
    {
        public static string Name = "SmsConfig";
        public bool IsSmsSend { get; set; }
        public string? TestMobileNumber { get; set; }
    }
}
