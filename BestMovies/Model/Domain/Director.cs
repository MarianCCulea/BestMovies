using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Director
    {
        public Director(int id, string name, int birth)
        {
            this.id = id;
            this.name = name;
            this.birth = birth;
        }

        private int id {get; set;}
        public string name { get; set; }
        private int birth { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Director director &&
                   id == director.id &&
                   name == director.name &&
                   birth == director.birth;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name, birth);
        }
    }
}
