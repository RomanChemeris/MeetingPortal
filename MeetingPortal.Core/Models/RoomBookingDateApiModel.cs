using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.Core.Models
{
    public class RoomBookingDateApiModel
    {
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public string Name { get; set; }

        public string BookingTime => $"{BookingFrom:D} {BookingFrom:HH:mm}-{BookingTo:HH:mm}";
    }
}
