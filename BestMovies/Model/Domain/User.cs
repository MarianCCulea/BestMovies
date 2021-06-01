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

        public User(string email, string passWord, List<int> favMoviesIDs)
        {
            Email = email;
            PassWord = passWord;
            FavMoviesIDs = favMoviesIDs;
        }

        private String Email { get; set; }
        private String PassWord { get; set; }

        private List<int> FavMoviesIDs { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Email == user.Email &&
                   PassWord == user.PassWord &&
                   EqualityComparer<List<int>>.Default.Equals(FavMoviesIDs, user.FavMoviesIDs);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, PassWord, FavMoviesIDs);
        }
    }
}
