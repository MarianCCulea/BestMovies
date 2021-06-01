using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Rating
    {
        public Rating(int rateId, string ratingValue, int ratingVotes)
        {
            this.rateId = rateId;
            this.ratingValue = ratingValue;
            this.ratingVotes = ratingVotes;
        }

        private int rateId { get; set; }

        private String ratingValue { get; set; }

        private int ratingVotes { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Rating rating &&
                   rateId == rating.rateId &&
                   ratingValue == rating.ratingValue &&
                   ratingVotes == rating.ratingVotes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(rateId, ratingValue, ratingVotes);
        }
    }
}
