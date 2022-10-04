using ForumApp.Data;
using ForumApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumDbContext context;

        public PostController(ForumDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await context.Posts
                .Select(p=> new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }).ToListAsync();


            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new PostViewModel();

            ViewData["Title"] = "Add new Post";
            return View("Edit", model);
        }

        public async Task<IActionResult> Edit(PostViewModel model)
        {
            ViewData["Tilte"] = model.Id == 0 ? "Add new Post" : "Edit post";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.Id == 0)
            {
                context.Posts.Add(new Post()
                {
                    Title = model.Title,
                    Content=model.Content
                });
            }
            else
            {
                var post = await context.Posts.FindAsync(model.Id);

                if(post != null)
                {
                    post.Title = model.Title;
                    post.Content = model.Content; 
                }
            }
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
