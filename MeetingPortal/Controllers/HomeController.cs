using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MeetingPortal.Core.ViewModels;
using MeetingPortal.DAL.Entities;
using MeetingPortal.DAL.ServiceInterfaces;
using MeetingPortal.DAL.Services;

namespace MeetingPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IContentService ContentService { get; set; }

        public HomeController(IContentService contentService)
        {
            ContentService = contentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMeetingRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddMeetingRoom(MeetingRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ContentService.CreateMeetingRoom(new MeetingRoom
                {
                    Name = model.Name,
                    HaveMarkerBoard = model.HaveMarkerBoard,
                    HaveProjector = model.HaveProjector,
                    NumberOfChair = model.NumberOfChair
                });
                ViewBag.IsSuccess = "ok";
            }
            return View(model);
        }

        public async Task<ActionResult> EditMeetingRoom(int id)
        {
            return View(await ContentService.GetMeetingRoomById(id));
        }

        [HttpPost]
        public async Task<ActionResult> EditMeetingRoom(MeetingRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ContentService.EditMeetingRoom(model);
                ViewBag.IsSuccess = "ok";
            }
            return View(model);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public async Task<ActionResult> Rooms()
        {
            return Json(await ContentService.GetMeetingRooms(), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public async Task<ActionResult> RoomRequests()
        {
            return Json(await ContentService.GetRoomRequests(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None)]
        public async Task<ActionResult> ModerateRoomRequest(int id, bool accept)
        {
            await ContentService.ModerateRoomRequest(id, accept);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}