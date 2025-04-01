using Microsoft.EntityFrameworkCore;

namespace JobSyncWebApi.Models
{
    public class JobContext:DbContext
    {
        public JobContext(DbContextOptions<JobContext> options):base(options)
        {
            
        }
        public DbSet<Job> JobSet { get; set; }
    }
}
