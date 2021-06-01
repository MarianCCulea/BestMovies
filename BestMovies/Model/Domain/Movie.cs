﻿using System;
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

        public Movie(int id, string title, Director director, Rating rating, List<Star> stars,string poster)
        {
            this.id = id;
            this.title = title;
            this.director = director;
            this.rating = rating;
            this.stars = stars;
            this.poster = poster;
        }

        public int id { get; set; }

        public string title { get; set; }

        public Director director { get; set; }

        public Rating rating { get; set; }

        public List<Star> stars { get; set; }
        public string poster { get; set; }

        public string GetPoster()
        {
            if (poster.Equals("N/A")){
                return "https://cdn.discordapp.com/attachments/819280914319933510/849390722502885386/matrix_the_1999_3133_poster.png";
            }
            return poster;
        }

        public override bool Equals(object obj)
        {
            return obj is Movie movie &&
                   id == movie.id &&
                   title == movie.title &&
                   EqualityComparer<Director>.Default.Equals(director, movie.director) &&
                   EqualityComparer<Rating>.Default.Equals(rating, movie.rating) &&
                   EqualityComparer<List<Star>>.Default.Equals(stars, movie.stars);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, title, director, rating, stars,poster);
        }
    }
}
