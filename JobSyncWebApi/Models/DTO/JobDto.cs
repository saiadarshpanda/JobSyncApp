using System.ComponentModel.DataAnnotations;

namespace JobSyncWebApi.Models.DTO
{
    public class JobDto
    {
       


        [Required]
        [StringLength(100)]
        public string JobType { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string JobListingName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]

        public string Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(500)]
        public string CompanyDescription { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string ContactEmail { get; set; }

        [Required]
        [Phone]
        [StringLength(12)]
        public string ContactPhone { get; set; }
    }
}
