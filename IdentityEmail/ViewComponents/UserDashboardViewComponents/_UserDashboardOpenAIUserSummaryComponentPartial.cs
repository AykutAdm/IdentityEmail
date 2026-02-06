using IdentityEmail.Context;
using IdentityEmail.Entities;
using IdentityEmail.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _UserDashboardOpenAIUserSummaryComponentPartial : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly OpenAIService _openAI;

        public _UserDashboardOpenAIUserSummaryComponentPartial(EmailContext context, UserManager<AppUser> userManager, OpenAIService openAI)
        {
            _context = context;
            _userManager = userManager;
            _openAI = openAI;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var sentCount = await _context.SentMessages.Where(x => x.SenderEmail == user.Email).CountAsync();
            var receivedCount = await _context.Messages.Where(x => x.ReceiverEmail == user.Email).CountAsync();

            var recentSent = await _context.SentMessages
                .Where(x => x.SenderEmail == user.Email)
                .OrderByDescending(x => x.SendDate)
                .Take(3)
                .Select(x => x.Subject + ": " + x.MessageDetail.Substring(0, Math.Min(100, x.MessageDetail.Length)))
                .ToListAsync();

            var recentMessages = string.Join("\n", recentSent);

            var summary = await _openAI.GenerateUserSummaryAsync(sentCount, receivedCount, recentMessages);

            ViewBag.Summary = summary;
            ViewBag.SentCount = sentCount;
            ViewBag.ReceivedCount = receivedCount;

            return View();
        }
    }
}
