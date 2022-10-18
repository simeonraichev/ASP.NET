namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using TaskBoardApp.Data;
    using TaskBoardApp.Models.Task;

    public class TasksController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public TasksController(TaskBoardAppDbContext data)
        {
            this.data = data;
        }

        public IActionResult Create()
        {
            var bords = GetBoards();
            var taskModel = new TaskFormModel()
            {
                Boards = bords
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Create(TaskFormModel model)
        {
            if (!model.Boards.Any(b => b.Id == model.BoardId))
            {
                this.ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            var currentUserId = GetUserId();

            var task = new Data.Entities.Task()
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = currentUserId
            };

            this.data.Tasks.Add(task);
            this.data.SaveChanges();

            var boards = this.data.Boards;
            return RedirectToAction("All", "Boards");
        }

        public IActionResult Details(int id)
        {
            var task = this.data
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                Board = t.Board.Name,
                Owner = t.Owner.UserName
            }).FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = this.data
                .Tasks
                .Find(id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            var taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskFormModel model)
        {
            var task = this.data
                .Tasks
                .Find(id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            if (!GetBoards().Any(b => b.Id == model.BoardId))
            {
                this.ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            this.data.SaveChanges();

            return RedirectToAction("All", "Boards");
        }

        public IActionResult Delete(int id)
        {
            var task = this.data
                .Tasks
                .Find(id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            var taskModel = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Delete(TaskViewModel model)
        {
            var task = this.data
                .Tasks
                .Find(model.Id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            this.data.Tasks.Remove(task);
            this.data.SaveChanges();

            return RedirectToAction("All", "Boards");
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        private IEnumerable<TaskBoardModel> GetBoards()
            => this.data.Boards.Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name
            })
            .ToList();
    }
}
