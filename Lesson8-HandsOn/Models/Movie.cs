using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson8_HandsOn.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LengthMin { get; set; }
        public string Description { get; set; }
        public string Img_Src { get; set; }
    }
}
