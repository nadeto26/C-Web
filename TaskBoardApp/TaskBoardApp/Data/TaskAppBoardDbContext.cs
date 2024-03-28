using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskBoardApp.Data.Models.Board;
using TaskBoardApp.Data.Models.Task;
 

namespace TaskBoardApp.Data
{
   
    public class TaskAppBoardDbContext : IdentityDbContext
    {
       

        public TaskAppBoardDbContext(DbContextOptions<TaskAppBoardDbContext> options)
            : base(options)
        {
        }

        

        public DbSet<TaskModel> Tasks { get; set; } = null!;

        public DbSet<BoardModel> Boards { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.
                Entity<TaskModel>()
                .HasOne(t=> t.Board)
                .WithMany(b=>b.Tasks)
                .HasForeignKey(b=>b.BoardId)
                .OnDelete(DeleteBehavior.Restrict);


            SeedUsers();
            builder.Entity<IdentityUser>()
                .HasData(TestUser);

            SeedBoards();
            builder.Entity<BoardModel>()
                .HasData(OpenBoard, InProgressBoard,DoneBoard);

            builder.
                Entity<TaskModel>()
                .HasData(new TaskModel()
                {
                    Id = 1,
                    Title = "Improve your CSS",
                    Description = "Implement better styling",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id
                },
                new TaskModel()
                {
                    Id = 2,
                    Title = "Improve your CSS",
                    Description = "Implement better styling",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = InProgressBoard.Id
                },
                new TaskModel()
                {
                    Id = 3,
                    Title = "Improve your CSS",
                    Description = "Implement better styling",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = DoneBoard.Id
                });
               

            base.OnModelCreating(builder);
        }

        private void SeedUsers() // юзъра, който ще тества 
        {
            var hasher = new PasswordHasher<IdentityUser>();

            TestUser = new IdentityUser()
            {
                UserName = "nadej.karapetr@gmail.com",
                NormalizedEmail = "NADEJ.KARAPETR@GMAIL.COM"
            };

            TestUser.PasswordHash = hasher.HashPassword(TestUser, "123456");
        }

        private void SeedBoards()
        {
            OpenBoard = new BoardModel()
            {
                Id = 1,
                Name = "Open"
            };

            InProgressBoard = new BoardModel()
            {
                Id=2,
                Name = "In Progress"
            };

            DoneBoard = new BoardModel()
            {
                Id = 3,
                Name = "Done Board"
            };
        }

        private IdentityUser TestUser { get; set; }

        private BoardModel OpenBoard { get; set; }

        private BoardModel InProgressBoard { get; set; }

        private BoardModel DoneBoard { get; set; }

    }
}