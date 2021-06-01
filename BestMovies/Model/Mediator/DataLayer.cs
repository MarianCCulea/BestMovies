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
    IList<Movie> AllItems();
    Movie ItemById(int id);
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
               new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
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
        public async Task<IList<Movie>> RequestAllItemsAsync()
        {
            string uri = "https://localhost:8080/items";
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            movies = JsonConvert.DeserializeObject<List<Movie>>(stream);
            return movies;
        }
    }
}
