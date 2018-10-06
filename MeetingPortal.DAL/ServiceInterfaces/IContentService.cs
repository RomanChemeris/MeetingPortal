using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingPortal.Core.ViewModels;
using MeetingPortal.DAL.Entities;

namespace MeetingPortal.DAL.ServiceInterfaces
{
    public interface IContentService
    {
        Task<List<MeetingRoom>> GetMeetingRooms();
        Task CreateMeetingRoom(MeetingRoom meetingRoom);
        Task<List<MeetingRequestViewModel>> GetRoomRequests();
        Task ModerateRoomRequest(int id, bool accept);
        Task<MeetingRoomViewModel> GetMeetingRoomById(int id);
        Task EditMeetingRoom(MeetingRoomViewModel model);
    }
}
