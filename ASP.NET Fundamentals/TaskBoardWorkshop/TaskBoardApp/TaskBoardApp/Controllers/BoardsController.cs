﻿namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskBoardApp.Data;
    using TaskBoardApp.Models.Board;
    using TaskBoardApp.Models.Task;

    public class BoardsController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public BoardsController(TaskBoardAppDbContext data)
        {
            this.data = data;
        }

        public IActionResult All()
        {
            var boards = this.data.Boards.Select(b => new BoardViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Tasks = b.Tasks.Select(t => new TaskViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Owner = t.Owner.UserName
                })
            })
            .ToList();

            return View(boards);
        }
    }
}
