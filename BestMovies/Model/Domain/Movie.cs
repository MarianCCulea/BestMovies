using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(string movieID, string movieTitle, Director director, Rating rating, List<Star> stars)
        {
            MovieID = movieID;
            MovieTitle = movieTitle;
            Director = director;
            Rating = rating;
            Stars = stars;
        }

        private String MovieID { get; set; }

        private String MovieTitle { get; set; }

        private Director Director { get; set; }

        private Rating Rating { get; set; }

        private List<Star> Stars { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Movie movie &&
                   MovieID == movie.MovieID &&
                   MovieTitle == movie.MovieTitle &&
                   EqualityComparer<Director>.Default.Equals(Director, movie.Director) &&
                   EqualityComparer<Rating>.Default.Equals(Rating, movie.Rating) &&
                   EqualityComparer<List<Star>>.Default.Equals(Stars, movie.Stars);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MovieID, MovieTitle, Director, Rating, Stars);
        }
    }
}
