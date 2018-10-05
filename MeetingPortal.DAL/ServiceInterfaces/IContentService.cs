using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingPortal.DAL.Entities;

namespace MeetingPortal.DAL.Services
{
    public interface IContentService
    {
        Task<List<MeetingRoom>> GetMeetingRooms();
        Task CreateMeetingRoom(MeetingRoom meetingRoom);
    }
}
