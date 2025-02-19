﻿using JobSyncWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSyncWebApi.Repository
{
    public interface IJobRepository
    {

        Task<List<Job>> GetAllJobs(); 
        Task<Job> GetByIDAsync(int id);   
        Task<Job> UpdateJobAsync(int id, Job job); 
        Task<Job> CreateJobAsync(Job job);
        Task<Job> DeleteJobAsync(int id);
        
    }
}
