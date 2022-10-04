﻿using ForumApp.Data;
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
                .Select(p => new PostViewModel()
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
            var model = new AddPostViewModel();

            ViewData["Title"] = "Add new Post";
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Tilte"] = "Add new Post";
                return View(model);
            }
            context.Posts.Add(new Post()
            {
                Title = model.Title,
                Content = model.Content
            });
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await context.Posts.Where(p => p.Id == id).Select(p => new PostViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content
            }).FirstOrDefaultAsync();

            if (post != null)
            {
                return View(post);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(PostViewModel model)
        {
            var post = await context.Posts.FindAsync(model.Id);

            if (post != null)
            {
                post.Title = model.Title;
                post.Content = model.Content;
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}