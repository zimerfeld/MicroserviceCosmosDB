using CompanyAPI.Models;
using CompanyAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {

        private readonly ILogger<JobController> _logger;

        public JobController(ILogger<JobController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetFirstJob")]
        public IEnumerable<Job> GetJobs()
        {
            return null;
        }


        [HttpGet(Name = "CreateJobs")]
        public void Create() {
            for (int i = 0; i < 10; i++)
            {
                var job = new Job
                {
                    Id = Guid.NewGuid(),
                    Address = new Address
                    {
                        Line1 = $"{i} Some Street {i}",
                        Line2 = $"Somewhere{i}",
                        Town = "Birmingham",
                        PostCode = "B90 {i}SS",
                    },
                    Contacts = new List<Contact>()
                    {
                        new Contact { Title = "Mr", FirstName = $"Craig {i}", LastName = "Mellon", TelephoneNumber = "34441234" },
                        new Contact { Title = "Mrs", FirstName = $"Cara {i}", LastName = "Mellon", TelephoneNumber = "53665554" }
                    },
                    AssignedResource = new Resource
                    {
                        Id = Guid.NewGuid(),
                        Title = "Mr",
                        FirstName = "Bob",
                        LastName = "Builder",
                        TelephoneNumber = "0800 1234567"
                    }
                };
                using (var context = new JobContext())
                {
                    context.Database.EnsureCreated();
                    context.Add(job);
                    context.SaveChanges();
                }
            }
        }

    }
}