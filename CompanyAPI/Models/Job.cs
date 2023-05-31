namespace CompanyAPI.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }

        public List<Contact> Contacts { get; set; }

        public Guid AssignedResourceId { get; set; }
        public Resource AssignedResource { get; set; }
    }    
}