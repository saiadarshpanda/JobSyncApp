using JobSyncWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JobSyncWebApi.Repository
{
    public class SQLJobRepository : IJobRepository
    {
        private readonly JobContext _context;
        public SQLJobRepository(JobContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetAllJobs(string? jobtype = null, string? jobname = null, string? location = null, string? companyname = null,string ? sortBy = null, bool isDescending = false, int pagenumber = 1, int pagesize = 10)
        {
            var query = _context.JobSet.AsNoTracking().AsQueryable(); // AsNoTracking for read-only optimization,AsQueryable allows query construction before execution
            // IQueryable<Job> query = _context.JobSet; // No need for AsQueryable(),AsQueryable() is a method that converts an IEnumerable<T> into an IQueryable<T>.

            if (!string.IsNullOrEmpty(jobtype))
            {
                query = query.Where(e => e.JobType.Contains(jobtype));
            }
            if (!string.IsNullOrEmpty(jobname))
            {
                query = query.Where(e => e.JobListingName.Contains(jobname));
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location));
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                query = query.Where(e => e.CompanyName.Contains(companyname));
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = isDescending? query.OrderByDescending(x=>x.Location) : query.OrderBy(x => x.Location);
            }
            //Pagination
            var skipresult = (pagenumber - 1) * pagesize;

            return await query.Skip(skipresult).Take(pagesize).ToListAsync();
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
