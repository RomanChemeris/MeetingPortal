using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.Core.Models
{
    public class MeetingRoomApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfChair { get; set; }

        public bool HaveProjector { get; set; }

        public bool HaveMarkerBoard { get; set; }

        public DateTime? BookingTimeFrom { get; set; }
        public DateTime? BookingTimeTo { get; set; }
    }
}
