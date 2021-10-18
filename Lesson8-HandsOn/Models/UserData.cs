using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson8_HandsOn.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string MoviesWatched { get; set; }
        public string Email { get; set; }

        //Returns a deserialized list of ints which represent movie IDs
        public List<int> ParseId()
        {
            List<int> Ids = new List<int>();
            //serialized format "id,id,id,..."
            if(this.MoviesWatched == null)
            {
                return null;
            }else
            {
                string[] sIds = this.MoviesWatched.Split(',');
                for(int i=0; i<sIds.Length; i++)
                {
                    int temp;
                    if(Int32.TryParse(sIds[i], out temp))
                    {
                        Ids.Add(temp);
                    }
                }
            }
            return Ids;
        }
    }
}
