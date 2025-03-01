using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobSyncWebApi.Models;
using JobSyncWebApi.Repository;
using JobSyncWebApi.Models.DTO;
using System.Data;
using AutoMapper;

namespace JobSyncWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
       
        private readonly IJobRepository repository;
        private readonly IMapper mapper;

        public JobsController(IJobRepository jobRepository,IMapper mapper)
        {
            repository=jobRepository;
            this.mapper = mapper;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobSet([FromQuery] string? jobtype, [FromQuery] string? jobname, [FromQuery] string? location, [FromQuery] string? companyname) 
        {

            return await repository.GetAllJobs(jobtype, jobname, location, companyname);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await repository.GetByIDAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
           
            try
            {
                var jobdata= await repository.UpdateJobAsync( id, job);
                if (jobdata==null)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (repository.GetByIDAsync(id)==null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostJob(JobDto jobdto)
        {
          
            var jobmodel=mapper.Map<Job>(jobdto);
            jobmodel = await repository.CreateJobAsync(jobmodel);
            

            return CreatedAtAction("GetJob", new { id = jobmodel.Id }, jobmodel);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await repository.DeleteJobAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return NoContent();
        }

       
    }
}
