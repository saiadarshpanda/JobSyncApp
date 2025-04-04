﻿using JobSyncWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSyncWebApi.Repository
{
    public interface IJobRepository
    {

        Task<List<Job>> GetAllJobs(string? jobtype=null,string? jobname = null,  string? location = null,  string? companyname = null, string? sortBy = null, bool isDescending=false, int pagenumber = 1, int pagesize = 10); 
        Task<Job> GetByIDAsync(int id);   
        Task<Job> UpdateJobAsync(int id, Job job); 
        Task<Job> CreateJobAsync(Job job);
        Task<Job> DeleteJobAsync(int id);
        
    }
}
