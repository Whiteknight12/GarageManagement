using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetAsync(string toEmail, string resetLink);
    }
}
