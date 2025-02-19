using AutoMapper;
using JobSyncWebApi.Models;
using JobSyncWebApi.Models.DTO;

namespace JobSyncWebApi.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JobDto,Job>().ReverseMap();
        }
    }
}
