using System;
namespace EduHome.Service.ExternalServices.Interfaces
{
	public interface IEmailService
	{
        public Task SendEmail(string to, string subject, string body);
    }
}

