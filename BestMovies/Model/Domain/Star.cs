using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Star
    {
        public Star(int starID, string starName)
        {
            this.starID = starID;
            this.starName = starName;
        }

        private int starID {get; set;}

        private String starName { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Star star &&
                   starID == star.starID &&
                   starName == star.starName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(starID, starName);
        }
    }
}
