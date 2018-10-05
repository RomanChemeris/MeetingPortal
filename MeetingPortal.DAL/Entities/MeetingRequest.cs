using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.DAL.Entities
{
    public class MeetingRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime BookingTimeFrom { get; set; }

        public DateTime BookingTimeTo { get; set; }

        public bool? IsAccepted { get; set; }

        public virtual MeetingRoom Room { get; set; }
    }
}
