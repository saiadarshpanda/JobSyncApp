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
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace JobSyncWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
       
        private readonly IJobRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<JobsController> logger;
        public JobsController(IJobRepository jobRepository,IMapper mapper, ILogger<JobsController> logger)
        {
            repository = jobRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Jobs
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobSet([FromQuery] string? jobtype, [FromQuery] string? jobname, [FromQuery] string? location, [FromQuery] string? companyname, [FromQuery] string? sortBy, [FromQuery] bool isDescending,
            [FromQuery] int pagenumber=1, [FromQuery] int pagesize=10) 
        {
            logger.LogInformation("Your log starts from here");
            return await repository.GetAllJobs(jobtype, jobname, location, companyname, sortBy, isDescending,pagenumber,pagesize);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Reader")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await repository.GetByIDAsync(id);

            if (job == null)
            {
                return NotFound();
            }
            //throw   new Exception("its a new exception");
            //logger.LogWarning("Here you go");
            //logger.LogInformation($"Your log starts from here:{JsonSerializer.Serialize( job)}");
            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult> PostJob(JobDto jobdto)
        {
          
            var jobmodel=mapper.Map<Job>(jobdto);
            jobmodel = await repository.CreateJobAsync(jobmodel);
            

            return CreatedAtAction("GetJob", new { id = jobmodel.Id }, jobmodel);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
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
