using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Rating
    {
        public Rating(int id, string rating, int votes)
        {
            this.id = id;
            this.rating = rating;
            this.votes = votes;
        }

        private int id { get; set; }

        public string rating { get; set; }

        private int votes { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Rating rating &&
                   id == rating.id &&
                   rating.Equals(rating.rating) &&
                   votes == rating.votes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, rating, votes);
        }
    }
}
