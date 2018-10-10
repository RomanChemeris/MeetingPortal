using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.Core.Models
{
    public class MeetingCreateRequest
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
