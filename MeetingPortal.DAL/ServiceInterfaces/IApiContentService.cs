using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingPortal.Core.Models;

namespace MeetingPortal.DAL.ServiceInterfaces
{
    public interface IApiContentService
    {
        Task<List<MeetingRoomApiModel>> GetMeetingRooms();
        Task<List<RoomBookingDateApiModel>> GetRoomInfo(int id);
        Task CreateMeetingRequest(int roomId, string name, DateTime fromTime, DateTime toTime);
    }
}
