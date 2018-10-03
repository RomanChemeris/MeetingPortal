using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.DAL.Entities
{
    public class MeetingRoom
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfChair { get; set; }

        public bool HaveProjector { get; set; }

        public bool HaveMarkerBoard { get; set; }

        public List<MeetingRequest> Requests { get; set; }
    }
}
