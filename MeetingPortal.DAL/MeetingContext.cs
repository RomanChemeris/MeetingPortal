using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingPortal.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MeetingPortal.DAL
{
    public class MeetingContext : IdentityDbContext<PortalUser>
    {
        public MeetingContext() : base("MeetingConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MeetingContext>());
        }

        public virtual IDbSet<MeetingRoom> MeetingRooms { get; set; }

        public virtual IDbSet<MeetingRequest> MeetingRequests { get; set; }
    }
}
