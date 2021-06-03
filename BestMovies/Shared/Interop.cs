using System.Threading.Tasks;
using System;
using Microsoft.JSInterop;

namespace BestMovies.Shared
{
    public class Interop
    {
        private readonly IJSRuntime jsRuntime;

        public Interop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public ValueTask<string> GetElementByName(string name)
        {
            try
            {
                return jsRuntime.InvokeAsync<string>(
                    "interop.getElementByName",
                    name);
            }
            catch
            {
                return new ValueTask<string>(Task.FromResult(string.Empty));
            }
        }

        public Task SubmitForm(string path, object fields)
        {
            try
            {
                jsRuntime.InvokeAsync<string>(
                    "interop.submitForm",
                    path, fields);
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
        }

    }
}