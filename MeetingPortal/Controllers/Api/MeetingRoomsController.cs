using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MeetingPortal.DAL.ServiceInterfaces;

namespace MeetingPortal.Controllers.Api
{
    public class MeetingRoomsController : ApiController
    {
        private IContentService ContentService { get; set; }

        public MeetingRoomsController(IContentService contentService)
        {
            ContentService = contentService;
        }
    }
}
