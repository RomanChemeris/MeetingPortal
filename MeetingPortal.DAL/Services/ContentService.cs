using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingPortal.Core.Models;
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

                Context.RequestNotifications.Add(new RequestNotification
                {
                    Created = DateTime.Now,
                    Request = request
                });

                await Context.SaveChangesAsync();
            }
        }

        public async Task<MeetingRoomViewModel> GetMeetingRoomById(int id)
        {
            return await Context.MeetingRooms
                .Where(x => x.Id == id)
                .Select(x => new MeetingRoomViewModel
                {
                    Id = x.Id,
                    HaveMarkerBoard = x.HaveMarkerBoard,
                    HaveProjector = x.HaveProjector,
                    NumberOfChair = x.NumberOfChair,
                    Name = x.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditMeetingRoom(MeetingRoomViewModel model)
        {
            var room = await Context.MeetingRooms.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (room != null)
            {
                room.Name = model.Name;
                room.HaveMarkerBoard = model.HaveMarkerBoard;
                room.HaveProjector = model.HaveProjector;
                room.NumberOfChair = model.NumberOfChair;
                await Context.SaveChangesAsync();
            }
        }

        public async Task RemoveMeetingRoom(int id)
        {
            var room = await Context.MeetingRooms.FirstOrDefaultAsync(x => x.Id == id);
            if (room != null)
            {
                Context.RequestNotifications.RemoveRange(Context.RequestNotifications.Join(
                    Context.MeetingRequests.Where(x => x.Room.Id == id), n => n.Request.Id, r => r.Id, (n, r) => n));
                Context.MeetingRequests.RemoveRange(Context.MeetingRequests.Where(x => x.Room.Id == id));
                Context.MeetingRooms.Remove(room);
                await Context.SaveChangesAsync();
            }
        }
    }
}
