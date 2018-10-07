using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MeetingPortal.Core.Models;
using MeetingPortal.DAL.ServiceInterfaces;
using Microsoft.Owin.Security.Provider;

namespace MeetingPortal.Controllers.Api
{
    public class MeetingRoomsController : ApiController
    {
        private IApiContentService ApiContentService { get; set; }

        public MeetingRoomsController(IApiContentService apiContentService)
        {
            ApiContentService = apiContentService;
        }

        public async Task<List<MeetingRoomApiModel>> GetMeetingRooms()
        {
            return await ApiContentService.GetMeetingRooms();
        }

        public async Task<List<RoomBookingDateApiModel>> GetMeetingRoomInfo(int id)
        {
            return await ApiContentService.GetRoomInfo(id);
        }

        public async Task AddMeetingRequest(int roomId, string name, DateTime fromTime, DateTime toTime)
        {
            await ApiContentService.CreateMeetingRequest(roomId, name, fromTime, toTime);
        }
    }
}
