using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.Core.ViewModels
{
    public class MeetingRequestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RoomName { get; set; }

        public DateTime BookingTimeFrom { get; set; }
        public DateTime BookingTimeTo { get; set; }

        public string BookingTime => $"{BookingTimeFrom:D} {BookingTimeFrom:HH:mm}-{BookingTimeTo:HH:mm}";
    }
}
