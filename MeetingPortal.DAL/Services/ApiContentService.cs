using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingPortal.Core.Models;
using MeetingPortal.DAL.Entities;
using MeetingPortal.DAL.ServiceInterfaces;

namespace MeetingPortal.DAL.Services
{
    public class ApiContentService : IApiContentService
    {
        private MeetingContext Context { get; set; }

        public ApiContentService(MeetingContext meetingContext)
        {
            Context = meetingContext;
        }

        public async Task<List<MeetingRoomApiModel>> GetMeetingRooms()
        {
            return await Context.MeetingRooms
                .GroupJoin(
                    Context.MeetingRequests.Where(x => x.IsAccepted == true),
                    x => x.Id,
                    y => y.Room.Id,
                    (x, y) => new { Room = x, Requests = y })
                .SelectMany(
                    x => x.Requests.DefaultIfEmpty(),
                    (x, y) => new MeetingRoomApiModel
                    {
                        Id = x.Room.Id,
                        HaveMarkerBoard = x.Room.HaveMarkerBoard,
                        HaveProjector = x.Room.HaveProjector,
                        NumberOfChair = x.Room.NumberOfChair,
                        Name = x.Room.Name,
                        BookingTimeFrom = y.BookingTimeFrom,
                        BookingTimeTo = y.BookingTimeTo
                    }).ToListAsync();
        }

        public async Task<List<RoomBookingDateApiModel>> GetRoomInfo(int id)
        {
            return await Context.MeetingRooms
                .Where(x => x.Id == id)
                .Join(Context.MeetingRequests.Where(x => x.IsAccepted == true), room => room.Id, req => req.Room.Id, (room, req) => req)
                .Select(x => new RoomBookingDateApiModel
                {
                    Name = x.Name,
                    BookingFrom = x.BookingTimeFrom,
                    BookingTo = x.BookingTimeTo
                })
                .ToListAsync();
        }

        public async Task CreateMeetingRequest(int roomId, string name, DateTime fromTime, DateTime toTime)
        {
            var room = await Context.MeetingRooms.FirstOrDefaultAsync(x => x.Id == roomId);
            if (room != null)
            {
                Context.MeetingRequests.Add(new MeetingRequest
                {
                    BookingTimeFrom = fromTime,
                    BookingTimeTo = toTime,
                    CreatedDate = DateTime.Now,
                    IsAccepted = null,
                    Name = name,
                    Room = room
                });
                await Context.SaveChangesAsync();
            }
        }
    }
}
