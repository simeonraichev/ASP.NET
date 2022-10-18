using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Entities;

namespace TaskBoardApp.Data
{
    public class TaskBoardAppDbContext : IdentityDbContext<User>
    {
        private User GuestUser { get; set; }

        private Board OpenBoard { get; set; }

        private Board InProgressBoard { get; set; }

        private Board DoneBoard { get; set; }
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<Board> Boards { get; set; }
        
        
        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            this.GuestUser = new User()
            {
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@mail.com",
                NormalizedEmail = "GUESTMAIL@MAIL.COM",
                FirstName = "Guest",
                LastName = "User"
            };
            this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "guest");
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
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<MyTask>()
                .HasOne(t => t.Board)
                .WithMany(b => b.MyTasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);
            //SeedUsers();
            //builder.Entity<User>().HasData(this.GuestUser);

            //SeedBoards();
            //builder.Entity<Board>().HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

            //builder.Entity<MyTask>()
            //    .HasData(new MyTask()
            //    {
            //        Id= 1,
            //        Title = "Prepare for ASP.NET",
            //        Description = "Learn using ASP.NET",
            //        CreatedOn = DateTime.Now.AddMonths(-1),
            //        OwnerId = this.GuestUser.FirstName,
            //        BoardId = this.OpenBoard.Id
            //    },
            //    new MyTask()
            //    {
            //        Id = 2,
            //        Title = "Improve EFCore skills",
            //        Description = "Learn EFCore",
            //        CreatedOn = DateTime.Now.AddMonths(-5),
            //        OwnerId = this.GuestUser.FirstName,
            //        BoardId = this.OpenBoard.Id
            //    },
            //    new MyTask()
            //    {
            //        Id = 3,
            //        Title = "Improve ASP.NET Core skills",
            //        Description = "Learn using ASP.NET Core Identity",
            //        CreatedOn = DateTime.Now.AddMonths(-10),
            //        OwnerId = this.GuestUser.FirstName,
            //        BoardId = this.OpenBoard.Id
            //    },
            //    new MyTask()
            //    {
            //        Id = 4,
            //        Title = "Prepare for C# Fundamentals Exam",
            //        Description = "Prepare by solving old Mid and Final exams",
            //        CreatedOn = DateTime.Now.AddMonths(-1),
            //        OwnerId = this.GuestUser.FirstName,
            //        BoardId = this.OpenBoard.Id
            //    });
           
            base.OnModelCreating(builder);
        }
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

    }
}