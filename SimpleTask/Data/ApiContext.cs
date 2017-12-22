using Microsoft.EntityFrameworkCore;
using SimpleTask.DbModels;

namespace SimpleTask.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
