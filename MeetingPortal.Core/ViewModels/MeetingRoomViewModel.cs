using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPortal.Core.ViewModels
{
    public class MeetingRoomViewModel
    {
        [Required]
        [Display(Name = "Название комнаты")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Количество кресел")]
        public int NumberOfChair { get; set; }

        [Display(Name = "Наличие проектора")]
        public bool HaveProjector { get; set; }

        [Display(Name = "Наличие маркерной доски")]
        public bool HaveMarkerBoard { get; set; }
    }
}
