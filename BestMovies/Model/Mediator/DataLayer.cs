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
            Console.WriteLine("ready to get");
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
        public Movie ItemById(int id) => movies.SingleOrDefault();
        public Movie CloneItem(Movie source) => new Movie
        {
        };

        /*
        public async Task<string> SubmitChangesAsync(Movie item)
        {
            var serializer = new DataContractJsonSerializer(typeof(Movie));
            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            string uri = "https://localhost:8080/items/" + item.id;
            string jsonString;
            jsonString = JsonConvert.SerializeObject(item);

            HttpContent content = new StringContent(jsonString);
            var streamTask = client.PutAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            if (result.Equals("true"))
            {
                movies = await RequestAllItemsAsync();
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        */
        public async Task<IList<Movie>> RequestAllItems()
        {
            string uri = "https://europe-central2-functions-test-314508.cloudfunctions.net/getTop100";
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            movies = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return movies;
        }

        public async Task<IList<Movie>> Top100(string place)
        {
            string uri = "https://europe-central2-functions-test-314508.cloudfunctions.net/getTop100?place="+place;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            movies = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return movies;
        }

        public async Task<IList<Movie>> MostVoted(string place)
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/" + place+ "-votes/";
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            movies = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return movies;
        }

        public async Task<IList<Movie>> AllUserFavorites()
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> ItemByName(string name)
        {
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/" + name;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            Movie movie = JsonConvert.DeserializeObject<Movie>(stream);
            return movie;
        }

        Task<Movie> IDataLayer.ItemById(int id)
        {
            throw new NotImplementedException();
        }

        Task<IList<Movie>> IDataLayer.AllItems()
        {
            throw new NotImplementedException();
        }
    }
}
