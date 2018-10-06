using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingPortal.Core.ViewModels;
using MeetingPortal.DAL.Entities;
using MeetingPortal.DAL.ServiceInterfaces;

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

        public async Task<List<MeetingRequestViewModel>> GetRoomRequests()
        {
            return await Context.MeetingRequests
                .Include(x => x.Room)
                .Where(x => x.IsAccepted == null)
                .Select(x => new MeetingRequestViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BookingTimeTo = x.BookingTimeTo,
                    BookingTimeFrom = x.BookingTimeFrom,
                    RoomName = x.Room.Name
                })
                .ToListAsync();
        }

        public async Task ModerateRoomRequest(int id, bool accept)
        {
            var request = await Context.MeetingRequests.FirstOrDefaultAsync(x => x.Id == id && x.IsAccepted == null);
            if (request != null)
            {
                request.IsAccepted = accept;
                await Context.SaveChangesAsync();
            }
        }
    }
}
