using IdentityEmail.Context;
using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace IdentityEmail.Controllers
{
    public class EmailController : Controller
    {
        private readonly EmailContext _context;

        public EmailController(EmailContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(MailRequestDto mailRequestDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "tourifyx0@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequestDto.ReceiverEmail);
            mimeMessage.To.Add(mailboxAddressTo);


            mimeMessage.Subject = mailRequestDto.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequestDto.MessageDetail;
            mimeMessage.Body = bodyBuilder.ToMessageBody();


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate("tourifyx0@gmail.com", "#");
            client.Send(mimeMessage);
            client.Disconnect(true);

            TempData["SuccessMessage"] = "Mail başarıyla gönderildi!";
            return View();
        }
    }
}
