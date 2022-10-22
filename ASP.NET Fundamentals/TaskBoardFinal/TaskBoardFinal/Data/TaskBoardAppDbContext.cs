using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardFinal.Data.Models;
    

namespace TaskBoardFinal.Data
{
    public class TaskBoardAppDbContext : IdentityDbContext<User>
    {
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }

        private User GuestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgressBoard { get; set; }
        private Board DoneBoard { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedUsers();
            builder
                .Entity<User>()
                .HasData(this.GuestUser);

            SeedBoards();
            builder
                .Entity<Board>()
                .HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

            builder.Entity<Models.Task>()
                .HasData(new Models.Task()
                {
                    Id = 1,
                    Title = "Prepare for Asp.Net Fundamentals Exam",
                    Description = "Learn using Asp.Net Core Identity",
                    CreatedOn = DateTime.Now.AddMonths(-1),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.OpenBoard.Id
                },
                new Models.Task()
                {
                    Id = 2,
                    Title = "Improve Ef core skills",
                    Description = "Learn using Ef Core and MSSQL",
                    CreatedOn = DateTime.Now.AddMonths(-5),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id
                },
                new Models.Task()
                {
                    Id = 3,
                    Title = "Improve Asp.Net Core skills",
                    Description = "Learn using Asp.Net Core Identity",
                    CreatedOn = DateTime.Now.AddDays(-10),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.InProgressBoard.Id
                },
                new Models.Task()
                {
                    Id = 4,
                    Title = "Prepare for Asp.Net Fundamentals Exam",
                    Description = "Prepare by solving old Mid and Final Exams",
                    CreatedOn = DateTime.Now.AddYears(-1),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id
                });



            builder
                .Entity<Models.Task>()
                .HasOne(b => b.Board)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        private void SeedBoards()
        {
            this.OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };

            this.InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };

            this.DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            this.GuestUser = new User()
            {
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                FirstName = "Guest",
                LastName = "User"
            };

            this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "guest");
        }
    }
}