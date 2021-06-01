using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BestMovies.Pages;
using BestMovies.Model.Domain;
using System.Text;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

public interface IDataLayer
{
    Task<IList<Movie>> AllItems();
    Task<IList<Movie>> RequestAllItems();
    Task<IList<Movie>> Top100(string place);
    Task<IList<Movie>> MostVoted(string place);
    Task<IList<Movie>> AllUserFavorites();
    Task<Movie> ItemById(int id);
    Task<Movie> ItemByName(string name);
    

}

namespace BestMovies.Pages
{
    public class DataLayer : IDataLayer
    { 
        public IList<Movie> movies { get; set; }
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

        public IList<Movie> AllItems() => movies;

        public async Task<IList<Movie>> RequestAllItems()
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies";  //https://movies-app-310106.nw.r.appspot.com/api/movies
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            var moviex = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return moviex;
        }

        public async Task<IList<Movie>> Top100(string place)
        {
            string uri = "https://europe-central2-functions-test-314508.cloudfunctions.net/getTop100?place="+place;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            List<Movie> moviez = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return moviez;
        }

        public async Task<IList<Movie>> MostVoted(string place)
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/" + place+ "-votes/";
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            List<Movie> moviez = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return moviez;
        }

        public async Task<IList<Movie>> AllUserFavorites()
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> ItemByName(string name)
        {
            string jsonString= @"{""name"": """+name+ @"""}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var streamTask = client.PostAsync("https://movies-app-310106.nw.r.appspot.com/api/movies/name/", content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            Movie moviez = JsonConvert.DeserializeObject<Movie>(result);
            return moviez;
        }

        public async Task<Movie> ItemById(int id)
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/" + id;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            Movie moviez = JsonConvert.DeserializeObject<Movie>(stream);
            uri = "https://europe-central2-functions-test-314508.cloudfunctions.net/getMoviePoster?title=" + moviez.title;
            streamTask = client.GetAsync(uri);
            stream = await streamTask.Result.Content.ReadAsStringAsync();
            moviez.poster = stream;
            return moviez;
        }

        Task<IList<Movie>> IDataLayer.AllItems()
        {
            throw new NotImplementedException();
        }

    }
}
