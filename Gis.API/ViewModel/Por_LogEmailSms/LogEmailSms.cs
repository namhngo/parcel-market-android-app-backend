using System;

namespace Gis.API.ViewModel.Por_LogEmailSms
{
    public class LogEmailSms
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Time_create { get; set; }
        public string Time_send { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Contentemail { get; set; }
        public string Statusmail { get; set; }
    }
}
