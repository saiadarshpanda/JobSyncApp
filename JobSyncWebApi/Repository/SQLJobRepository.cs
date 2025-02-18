using JobSyncWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSyncWebApi.Repository
{
    public class SQLJobRepository : IJobRepository
    {
        private readonly JobContext _context;
        public SQLJobRepository(JobContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetAllJobs()
        {
           return await _context.JobSet.ToListAsync();
        }
        public async Task<Job> GetByIDAsync(int id)
        {
            return await _context.JobSet.FindAsync(id);
        }
        public async Task<Job> CreateJobAsync(Job job)
        {
            await _context.JobSet.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> UpdateJobAsync(int id, Job job) 
        {
            if (id != job.Id)
            {
                return null;
            }

            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return job;
        }
        public async Task<Job> DeleteJobAsync(int id)
        {
            var job = await _context.JobSet.FindAsync(id);
            if (job == null)
            {
                return null;
            }

            _context.JobSet.Remove(job);
            await _context.SaveChangesAsync();
            return job;


        }
        
    }
}
