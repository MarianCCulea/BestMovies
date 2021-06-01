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

        private int person_id {get; set;}

        private String star_name { get; set; }

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
