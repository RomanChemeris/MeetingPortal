using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using MeetingPortal.DAL;
using MeetingPortal.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace MeetingPortal.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplicationUserManager : UserManager<PortalUser>
    {
        public ApplicationUserManager(IUserStore<PortalUser> store)
            : base(store)
        {
            var dataProtectionProvider = Startup.DataProtectionProvider;
            UserTokenProvider = new DataProtectorTokenProvider<PortalUser>(dataProtectionProvider.Create("meeting-portal-ex Identity"))
            {
                TokenLifespan = TimeSpan.FromDays(1)
            };
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<PortalUser>(context.Get<MeetingContext>()));
            manager.UserValidator = new UserValidator<PortalUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = Startup.DataProtectionProvider;
            manager.UserTokenProvider = new DataProtectorTokenProvider<PortalUser>(dataProtectionProvider.Create("meeting-portal-ex Identity"))
            {
                TokenLifespan = TimeSpan.FromDays(1)
            };

            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<PortalUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(PortalUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}