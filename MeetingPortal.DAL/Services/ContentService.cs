using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingPortal.DAL.Entities;

namespace MeetingPortal.DAL.Services
{
    public class ContentService : IContentService
    {
        private MeetingContext Context { get; set; }

        public ContentService(MeetingContext meetingContext)
        {
            Context = meetingContext;
        }

        public async Task<List<MeetingRoom>> GetMeetingRooms()
        {
            return await Context.MeetingRooms.ToListAsync();
        }

        public async Task CreateMeetingRoom(MeetingRoom meetingRoom)
        {
            Context.MeetingRooms.Add(meetingRoom);
            await Context.SaveChangesAsync();
        }
    }
}
