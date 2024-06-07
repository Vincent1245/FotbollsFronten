using FotbollsFronten.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace FotbollsFronten.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public BlogDetailsModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Blog Blog { get; set; }

        public List<Comment> Comments { get; set; }

        [BindProperty]
        public Comment NewComment { get; set; } = new Comment();
        public List<IdentityUser> Users { get; set; }
        public bool IsAdmin { get; set; }
        public string CurrentUserId { get; set; }
        
        [BindProperty]
        public Report NewReport { get; set; } = new Report();






        public async Task<IActionResult> OnGetAsync(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null )
            {
                CurrentUserId = user.Id;
            }
            else
            {
                CurrentUserId = null; 
            }
          
            Blog = await _context.Blog.FirstOrDefaultAsync(m => m.Id == id);
            Users = await _userManager.Users.ToListAsync();
            Comments = await _context.Comment
                                     .Where(c => c.BlogId == id)                                    
                                     .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Du måste vara inloggad för att lämna en kommentar.");
                return Page();
            }

            NewComment.CommentDate = DateTime.Now;
            NewComment.BlogId = id;
            NewComment.UserId = user.Id;

            _context.Comment.Add(NewComment);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = id });
        }
        
        public async Task<IActionResult> OnPostDeleteCommentAsync(int commentId)
        {
            var user = await _userManager.GetUserAsync(User);
            
                Models.Comment CommentToBeDeleted = await _context.Comment.FindAsync(commentId);
                if (CommentToBeDeleted != null)
                {
                    _context.Comment.Remove(CommentToBeDeleted);
                    await _context.SaveChangesAsync();
                }

            
            Comments = await _context.Comment.ToListAsync();


            return RedirectToPage(); 
        }

        public async Task<IActionResult> OnPostReportAsync(int? postId, int? commentId)
        {
            var user = await _userManager.GetUserAsync(User);
            var report = new Report
            {
                Reason = NewReport.Reason,
                UserId = user?.Id ?? "Anonymous",
                PostId = postId,
                CommentId = commentId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }


    }
}









