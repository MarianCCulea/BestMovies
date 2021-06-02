using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using BestMovies.Model.Domain;
using System.Text;

namespace BestMovies.Pages
{
    public interface IDataLayer
{
    Task<IList<Movie>> RequestAllItems();
    Task<IList<Movie>> Top100(string place);
    Task<IList<Movie>> MostVoted(string place);
    Task<IList<Movie>> AllUserFavorites();
    Task<IList<Movie>> ProcessPlace(string place);
    Task<Movie> ItemById(int id);
    Task<IList<Movie>> ItemByName(string name);
    Task<Star> StarByName(string name);
}

    public class DataLayer : IDataLayer
    {
        public static DataLayer instance = null;
        private static readonly object padlock = new object();
        HttpClient client = new HttpClient();
        public DataLayer()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        public static DataLayer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataLayer();
                    }
                    return instance;
                }
            }
        }

   

        public async Task<IList<Movie>> RequestAllItems()
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies";  //https://movies-app-310106.nw.r.appspot.com/api/movies
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            var moviex = JsonConvert.DeserializeObject<List<Movie>>(stream);
            Console.WriteLine(moviex.Count +"-------------------count");
            return moviex;
        }

        public async Task<IList<Movie>> Top100(string place)
        {
            string uri = "https://europe-central2-movies-app-310106.cloudfunctions.net/getTop100?place=" + place;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            List<Movie> moviez = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return moviez;
        }

        public async Task<IList<Movie>> MostVoted(string place)
        {
            string uri = "https://europe-central2-movies-app-310106.cloudfunctions.net/getMostVoted?place=" + place;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            List<Movie> moviez = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return moviez;
        }

        public async Task<IList<Movie>> AllUserFavorites()
        {
            /*
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/favorites/";
            string jsonString = @"{ ""ids"":";
            IList<int> milbei = new List<int> { 68646, 71562, 68202, 70951 };
            var json = System.Text.Json.JsonSerializer.Serialize(milbei);
            jsonString = jsonString + json + "}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            */
            return null;
        }

        public async Task<IList<Movie>> ItemByName(string name)
        {
            string jsonString= @"{""name"": """+name+ @"""}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/name/";
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            IList<Movie> moviez = JsonConvert.DeserializeObject<IList<Movie>>(result);
            return moviez;
        }

        public async Task<Movie> ItemById(int id)
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/" + id;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            Movie moviez = JsonConvert.DeserializeObject<Movie>(stream);
            uri = "https://europe-central2-movies-app-310106.cloudfunctions.net/getPoster?title=" + moviez.title;
            streamTask = client.GetAsync(uri);
            stream = await streamTask.Result.Content.ReadAsStringAsync();
            moviez.poster = stream; 
            uri = "https://europe-central2-movies-app-310106.cloudfunctions.net/getPlot?title=" + moviez.title;
            streamTask = client.GetAsync(uri);
            stream = await streamTask.Result.Content.ReadAsStringAsync();
            moviez.plot = stream;
            for(int i=0;i< moviez.stars.Count;i++)
            {
                 Star newstar= StarByName(moviez.stars[i].star_name).Result;
                moviez.stars[i].average_movie_rating = newstar.average_movie_rating;
                moviez.stars[i].birth = newstar.birth;
            }

            return moviez;
        }

        public Task<IList<Movie>> ProcessPlace(string place)
        {
            switch (place)
            {
                case "Bot 50 Movies": return Top100("bot");
                case "Most 50 Voted Movies": return MostVoted("most");
                case "Least 50 Voted Movies": return Top100("less");
                default : return Top100("top");
            }
        }

        public async Task<Star> StarByName(string name)
        {
            string jsonString = @"{""name"": """ + name + @"""}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/person/name/";
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            IList<Model.Domain.Star> stars = JsonConvert.DeserializeObject<IList<Model.Domain.Star>>(result);
            try { return stars[0]; }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
