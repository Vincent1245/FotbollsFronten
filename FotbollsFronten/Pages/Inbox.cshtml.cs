using FotbollsFronten.Data;
using FotbollsFronten.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



namespace FotbollsFronten.Pages
{
    public class InboxModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public InboxModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<Message> Messages { get; set; }

        [BindProperty]
        public string ReceiverId { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public List<IdentityUser> AllUsers { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Messages = await _context.Messages
                    .Where(m => m.ReceiverId == user.Id)
                    .OrderByDescending(m => m.Timestamp)
                    .ToListAsync();
            }

            AllUsers = await _userManager.Users.ToListAsync();
        }

        //public async Task<int> CountUnreadMessagesAsync()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user != null)
        //    {
        //        return await _context.Messages
        //            .CountAsync(m => m.ReceiverId == user.Id && !m.IsRead);
        //    }

        //    return 0;
        //}




        public async Task<IActionResult> OnPostAsync()
        {
            var sender = await _userManager.GetUserAsync(User);

            if (sender == null || string.IsNullOrEmpty(ReceiverId) || string.IsNullOrEmpty(Content))
            {
                return Page();
            }

            var message = new Message
            {
                SenderId = sender.Id,
                SenderEmail = sender.Email,
                ReceiverId = ReceiverId,
                Content = Content,
                Timestamp = DateTime.Now,
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
