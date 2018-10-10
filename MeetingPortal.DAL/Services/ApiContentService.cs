using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
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
                    (room, request) => new { room.Room, request })
                .OrderBy(x => x.request.BookingTimeFrom)
                .GroupBy(x => new
                    {
                        x.Room.Id,
                        x.Room.Name,
                        x.Room.HaveMarkerBoard,
                        x.Room.HaveProjector,
                        x.Room.NumberOfChair
                })
                .Select(x => new MeetingRoomApiModel
                {
                    Id = x.Key.Id,
                    HaveMarkerBoard = x.Key.HaveMarkerBoard,
                    HaveProjector = x.Key.HaveProjector,
                    NumberOfChair = x.Key.NumberOfChair,
                    Name = x.Key.Name,
                    BookingTimeFrom = x.FirstOrDefault().request.BookingTimeFrom,
                    BookingTimeTo = x.FirstOrDefault().request.BookingTimeTo
                })
                .ToListAsync();
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

        public async Task<List<NotificationApiModel>> GetLastNotifications()
        {
            return await Context.RequestNotifications.OrderByDescending(x => x.Created).Take(5).Select(x =>
                new NotificationApiModel
                {
                    Created = x.Created,
                    RoomName = x.Request.Room.Name,
                    BookingTimeFrom = x.Request.BookingTimeFrom,
                    BookingTimeTo = x.Request.BookingTimeTo,
                    Status = x.Request.IsAccepted,
                    RequestName = x.Request.Name
                }).ToListAsync();
        }
    }
}
