using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson8_HandsOn.Models
{
    public class UserData
    {
        public string Id { get; set; }
        public string MoviesWatched { get; set; }
        public string Email { get; set; }

        public static List<int> ParseId(string MoviesWatched)
        {
            List<int> Ids = new List<int>();
            //serialized format "id,id,id,..."
            if(MoviesWatched == null)
            {
                return null;
            }else
            {
                string[] sIds = MoviesWatched.Split(',');
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
