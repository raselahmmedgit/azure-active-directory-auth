﻿namespace lab.azure_active_directory_auth.Core
{
    public class EmailMessage
    {
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
