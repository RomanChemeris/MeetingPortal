using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.Core.Models
{
    public class NotificationApiModel
    {
        public DateTime Created { get; set; }
        public string RoomName { get; set; }
        public string RequestName { get; set; }
        public DateTime BookingTimeFrom { get; set; }
        public DateTime BookingTimeTo { get; set; }
        public bool? Status { get; set; }

        public string BookingTime => $"{BookingTimeFrom:D} {BookingTimeFrom:HH:mm}-{BookingTimeTo:HH:mm}";
        public string CreatedTime => $"{Created:f}";
    }
}
