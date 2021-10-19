using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string TimeStamp { get; set; }
        //Get from User.Identity.Name
        public string CreatedBy { get; set; }
    }
}
