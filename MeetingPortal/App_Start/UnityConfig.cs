using MeetingPortal.DAL;
using MeetingPortal.DAL.Services;
using System;
using MeetingPortal.DAL.Entities;
using Unity;
using Unity.AspNet.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Unity.Injection;
using System.Data.Entity;
using MeetingPortal.App_Start;
using System.Web;

namespace MeetingPortal
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterType<DbContext, MeetingContext>(new PerRequestLifetimeManager());
            container.RegisterType<IContentService, ContentService>(new PerRequestLifetimeManager());

            container.RegisterType<IUserService, UserService>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<PortalUser>, UserStore<PortalUser>>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
        }
    }
}