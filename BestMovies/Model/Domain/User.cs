using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class User
    {
        public User()
        {
        }

        public User(string email, string passWord, List<Movie> favMoviesID)
        {
            Email = email;
            PassWord = passWord;
            FavMoviesID = favMoviesID;
        }

        private String Email { get; set; }
        private String PassWord { get; set; }

        private List<Movie> FavMoviesID { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Email == user.Email &&
                   PassWord == user.PassWord &&
                   EqualityComparer<List<Movie>>.Default.Equals(FavMoviesID, user.FavMoviesID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, PassWord, FavMoviesID);
        }
    }
}
