using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.DAL.Entities
{
    public class RequestNotification
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public virtual MeetingRequest Request { get; set; }
    }
}
