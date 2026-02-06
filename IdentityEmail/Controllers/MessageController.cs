using IdentityEmail.Context;
using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Threading.Tasks;

namespace IdentityEmail.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly EmailContext _emailContext;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _emailContext.Messages.Where(x => x.ReceiverEmail == user.Email).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MailRequestDto mailRequestDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", user.Email);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequestDto.ReceiverEmail);
            mimeMessage.To.Add(mailboxAddressTo);


            mimeMessage.Subject = mailRequestDto.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequestDto.MessageDetail;
            mimeMessage.Body = bodyBuilder.ToMessageBody();


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate("tourifyx0@gmail.com", "wcwh xvlw drtr blwb");
            client.Send(mimeMessage);
            client.Disconnect(true);


            var sentMessage = new SentMessage
            {
                SenderEmail = user.Email,
                ReceiverEmail = mailRequestDto.ReceiverEmail,
                Subject = mailRequestDto.Subject,
                MessageDetail = mailRequestDto.MessageDetail,
                SendDate = DateTime.Now,
                IsRead = false
            };

            _emailContext.SentMessages.Add(sentMessage);
            _emailContext.SaveChanges();

            TempData["SuccessMessage"] = "Mail başarıyla gönderildi!";
            return RedirectToAction("SendMessage");

        }


        public async Task<IActionResult> MessageDetail(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _emailContext.Messages.FirstOrDefault(x => x.MessageId == id && x.ReceiverEmail == user.Email);
            return View(values);
        }


        public async Task<IActionResult> SentMessage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var sentMessages = _emailContext.SentMessages.Where(x => x.SenderEmail == user.Email).OrderByDescending(x => x.SendDate).ToList();
            return View(sentMessages);
        }


        public async Task<IActionResult> SentMessageDetail(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _emailContext.SentMessages.FirstOrDefault(x => x.SentMessageId == id && x.SenderEmail == user.Email);
            return View(values);
        }

        public async Task<IActionResult> MessageDelete(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _emailContext.Messages.FirstOrDefault(x => x.MessageId == id && x.ReceiverEmail == user.Email);
            _emailContext.Remove(value);
            _emailContext.SaveChanges();
            return RedirectToAction("Inbox");
        }
    }
}
