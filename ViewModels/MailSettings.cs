namespace ByteInsights.ViewModels
{
    public class MailSettings
    {
        /* So that we can configure and use smtp server from google for example
         * 
         */

        public string? Mail { get; set; }

        public string? DisplayName { get; set; }

        public string? Password { get; set; }

        public string? Host { get; set; } // It is the smtp server. In our case, it is going to be gmail

        public int Port { get; set; }

    }
}
