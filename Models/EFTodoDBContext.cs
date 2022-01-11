using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJWS.Models
{
    public class EFTodoDBContext : DbContext
    {
        public EFTodoDBContext(DbContextOptions<EFTodoDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem[]
                {
                    new TodoItem { Id=1,IsComplete=false, TaskDescription="Починить"},
                    new TodoItem { Id=2,IsComplete=false, TaskDescription="Прочитать"},
                }
                );
        }

    }
}
