using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Storytime.Models;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Storytime
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new CustomUserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                 RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            manager.EmailService = new EmailService();

            return manager;
        }
    }

    public class CustomUserValidator<TUser> : UserValidator<TUser, string > where TUser : ApplicationUser
    {
        public CustomUserValidator(UserManager<TUser, string> manager) : base(manager)
        {

        }
        public override async Task<IdentityResult> ValidateAsync(TUser applicationuser)
        {
            IdentityResult result = await base.ValidateAsync(applicationuser);

            if (result.Succeeded)
            {
                var errors = new System.Collections.Generic.List<string>();

                await ValidatePhoneNumber(applicationuser, errors);

                if (errors.Count > 0)
                    return IdentityResult.Failed(errors.ToArray());
                else
                    return result;
            }
            else
            {
                return result;
            }
        }

            private async Task ValidatePhoneNumber(TUser applicationuser, System.Collections.Generic.List<string> errors)
            {
                var db = new PetaPoco.Database("AGSoftware");

                var iscontact = db.SingleOrDefault<Entities.AspNetUsers>("Select * From ASPNetUsers Where PhoneNumber = @0", applicationuser.PhoneNumber);

                if (iscontact != null)
                {
                    errors.Add("Phone number " + applicationuser.PhoneNumber + " is already taken.");
                }
            }
            
        }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("greg@agsoftwareinc.com", "2012Antoinette"),
                EnableSsl = true
            };

            MailMessage mailmessage = new MailMessage("greg@agsoftwareinc.com", message.Destination);
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = message.Body;
            mailmessage.Subject = message.Subject;

            return client.SendMailAsync(mailmessage);
        }
    }
}
