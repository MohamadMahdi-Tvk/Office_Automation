using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Personnel.Controllers
{
    [Authorize(Roles = "Admin,Personnel")]
    public class MessageController : Controller
    {
        OfficeAutomationContext db = new OfficeAutomationContext();
        UserService _userService;
        MessageService _messageService;

        public MessageController()
        {
            _userService = new UserService(db);
            _messageService = new MessageService(db);
        }

        [HttpGet]
        public ActionResult SendMessage()
        {
            var userSend = User.Identity.Name;
            var userFind = _userService.GetAll().First(t => t.PersonnelID == userSend);
            ViewBag.FullName = userFind.Name + " " + userFind.Family;

            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "Family");

            return View();
        }

        [HttpPost]
        public ActionResult SendMessage([Bind(Include = "MessageTitle,MessageContent,UserId")] MessageViewModel messageViewModel)
        {
            var userSend = User.Identity.Name;
            var userFind = _userService.GetAll().First(t => t.PersonnelID == userSend);

            if (ModelState.IsValid)
            {
                var message = AutoMapperConfig.mapper.Map<MessageViewModel, Message>(messageViewModel);

                message.UserSendMessage = userFind.Name + " " + userFind.Family;

                _messageService.Add(message);
                _messageService.Save();
                return Redirect("/Personnel/Dashboard/Index");
            }

            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "Family");
            return View(messageViewModel);
        }

        public ActionResult MessageList(int pageid = 1)
        {
            var userSend = User.Identity.Name;
            var userFind = _userService.GetAll().First(t => t.PersonnelID == userSend);
            int skip = (pageid - 1) * 8;

            int count = _messageService.GetAll().Where(t => t.UserId == userFind.UserId).Count();
            ViewBag.PageID = pageid;
            ViewBag.PageCount = count / 8;

            var message = _messageService.GetAll().ToList().Where(t => t.UserId == userFind.UserId).OrderByDescending(t => t.MessageId).Skip(skip).Take(8).ToList();


            var messageViewModel = AutoMapperConfig.mapper.Map<List<Message>, List<MessageViewModel>>(message);



            return View(messageViewModel);
        }

        public ActionResult MessageInfo(int id)
        {

            Message message = _messageService.GetEntity(id);

            if (message == null)
            {
                return HttpNotFound();
            }

            var messageViewModel = AutoMapperConfig.mapper.Map<Message, MessageViewModel>(message);

            return View(messageViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var message = _messageService.GetEntity(id.Value);

            if (message == null)
            {
                return HttpNotFound();
            }

            _messageService.Delete(message);
            _messageService.Save();
            return RedirectToAction("MessageList");
        }
    }
}