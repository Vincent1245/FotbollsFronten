using FotbollsFronten.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FotbollsFronten.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        
        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
          
        }

        [BindProperty]
        public Blog Blog { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public List<Blog> Blogs { get; set; }
        
        public async Task OnGetAsync(int? deleteId)
        {
            if(deleteId != 0)
            {
                Blog BlogToBeDeleted = await _context.Blog.FindAsync(deleteId);
                if(BlogToBeDeleted != null)
                {
                        if (System.IO.File.Exists("./wwwroot/userImages/" + BlogToBeDeleted.Image))
                        {
                            System.IO.File.Delete("./wwwroot/userImages/" + BlogToBeDeleted.Image);
                        }
                        _context.Blog.Remove(BlogToBeDeleted);
                        await _context.SaveChangesAsync();   
                }
                
            }
            Blogs = await _context.Blog.ToListAsync();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var image = UploadedImage;
            string fileName = "";

            if (image != null)
            {
                Random rnd = new();
                fileName = rnd.Next(0, 1000000).ToString() + image.FileName;

                using (var fileStream = new FileStream("./wwwroot/userImages/" + fileName, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

                Blog.Date = DateTime.Now;
                Blog.Image = fileName;
                Blog.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Blog.Add(Blog);
                await _context.SaveChangesAsync();

              
                return RedirectToPage("./Index");

        }
        
       
       
    }
}
