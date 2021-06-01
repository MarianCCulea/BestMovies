using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Director
    {
        public Director(int dirID, string dirName, int dirDay)
        {
            this.dirID = dirID;
            this.dirName = dirName;
            this.dirDay = dirDay;
        }

        private int dirID {get; set;}
        private String dirName { get; set; }
        private int dirDay { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Director director &&
                   dirID == director.dirID &&
                   dirName == director.dirName &&
                   dirDay == director.dirDay;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(dirID, dirName, dirDay);
        }
    }
}
