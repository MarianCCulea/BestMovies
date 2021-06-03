using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestMovies.Model.Domain
{
    public class FireAuthToken
    {
        public string localId { get; set; }
        public string email { get; set; }
        public string idToken { get; set; }
    }
}
