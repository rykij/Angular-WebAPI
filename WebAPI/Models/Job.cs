using System;

namespace WebAPI.Models
{
    public class Job
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Int16 Status { get; set; }
        public string Group { get; set; }
    }
}