using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Mu.NETcms.Models;
using System.Text.RegularExpressions;

namespace Mu.NETcms
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

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
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
                
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new CustomValidator();
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = false,
            //    RequireUppercase = false,
            //};

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            
            return manager;
        }
        /**
         * Override user creation. Create game account.
         **/
        override
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            string pattern = @"^[a-zA-Z0-9]*$";
            if(String.IsNullOrEmpty(user.GameId) || !Regex.IsMatch(user.GameId,pattern) || user.GameId.Length < 6 || user.GameId.Length > 10)
                return IdentityResult.Failed("Invalid game id.");
            using (var v =  new GameModels.GameDbContext()){
                if (v.Accounts.Find(user.GameId)!=null) return IdentityResult.Failed("Game id unavailable.");
            }
            
            
            var task = await base.CreateAsync(user, password);
            if (task.Succeeded)
            {
                using (var c = new GameModels.GameDbContext())
                {
                    c.Accounts.Add(new GameModels.Account()
                    {
                        memb___id = user.GameId,
                        memb__pwd = password,
                        memb_name = user.GameId,
                        mail_addr = user.Email,
                        sno__numb = "123456789123456789",
                        bloc_code = "0",
                        ctl1_code = "1"
                    
                    });
                    c.SaveChanges();
                }
            }
            return task;
            
        }
        public async Task<IdentityResult> ResetPasswordAsync(string Id, string token, string password)
        {
            var task = await base.ResetPasswordAsync(Id, token, password);
            if (task.Succeeded)
            {
                //string usr = "";
                using (var context = new ApplicationDbContext()){
                    string usr = context.Users.Find(Id).GameId;
                    var args = new System.Data.Common.DbParameter[] { new System.Data.SqlClient.SqlParameter { ParameterName = "pwd", Value = password },
                                                                                new System.Data.SqlClient.SqlParameter { ParameterName = "uid", Value = usr }};
                    context.Database.ExecuteSqlCommand("Update MEMB_INFO SET memb__pwd = @pwd where memb___id=@uid", args);
                }
            }
            return task;
        }
        override
        public async Task<IdentityResult> ChangePasswordAsync(string Id, string old_pw, string new_pw)
        {
            var task = await base.ChangePasswordAsync(Id, old_pw, new_pw);
            if (task.Succeeded)
            {
                using (var context = new ApplicationDbContext())
                {
                    string usr = context.Users.Find(Id).GameId;
                    var args = new System.Data.Common.DbParameter[] { new System.Data.SqlClient.SqlParameter { ParameterName = "pwd", Value = new_pw },
                                                                                new System.Data.SqlClient.SqlParameter { ParameterName = "uid", Value = usr }};
                    context.Database.ExecuteSqlCommand("Update MEMB_INFO SET memb__pwd = @pwd where memb___id=@uid", args);
                }
            }
            return task;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
    public class CustomValidator : PasswordValidator
    {
        private int MaximumLength = 10;
        public CustomValidator()
        {
            this.RequiredLength = 6;
            //this.RequireDigit = false;
            //this.RequireLowercase = false;
            //this.RequireNonLetterOrDigit = false;
            //this.RequireUppercase = false;
        }
        override
        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (String.IsNullOrEmpty(item) || item.Length < RequiredLength || item.Length > MaximumLength)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("Password length should be between {0} and {1}.", RequiredLength, MaximumLength)));
            }
            string pattern = @"^[a-zA-Z0-9]*$";
            if (!Regex.IsMatch(item, pattern))
            {
                return Task.FromResult(IdentityResult.Failed("Password can only contain alphanumerical characters."));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
