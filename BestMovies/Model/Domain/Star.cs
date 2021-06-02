using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Star
    {
        public Star(int person_id, string star_name)
        {
            this.person_id = person_id;
            this.star_name = star_name;
        }
        public int person_id {get; set;}

        public string star_name { get; set; }
        public int birth { get; set; }
        public float average_movie_rating { get; set; }

        public string GetStarName()
        {
            if(star_name==null || star_name.Equals("Not in database"))
            {
                return "";
            }
            return star_name;
        }
        public override bool Equals(object obj)
        {
            return obj is Star star &&
                   person_id == star.person_id &&
                   star_name == star.star_name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(person_id, star_name);
        }
    }
}
