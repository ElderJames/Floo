using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Floo.App.Web.Utils
{
    public class PreFetchedState
    {
        IJSRuntime jS;
        NavigationManager navigationManager;

        public PreFetchedState(IJSRuntime jS, NavigationManager navigationManager)
        {
            this.jS = jS;
            this.navigationManager = navigationManager;
        }

        IDictionary<string, string> _states;
        bool hasRender;

        public event Action<IDictionary<string, string>> StateChanged;

        public void Save<TData>(string key, TData data)
        {
            //var module = await jS.InvokeAsync<JSObjectReference>("import", "./js/floo.js");
            //var uri = navigationManager.Uri;
            //var type = typeof(TData);
            var dataJson = JsonSerializer.Serialize(data);
            _states ??= new Dictionary<string, string>();
            if (_states.ContainsKey(key))
            {
                _states[key] = dataJson;
            }
            else
            {
                _states.Add(key, dataJson);
            }

            StateChanged?.Invoke(_states);
        }

        public async ValueTask<TData> GetAsync<TData>(string key)
        {
            if (!hasRender)
                return default;

            var module = await jS.InvokeAsync<JSObjectReference>("import", "./js/floo.js");
            var uri = navigationManager.Uri;
            var type = typeof(TData).Name;
            var dataJson = await module.InvokeAsync<string>("GetPreFetchedData", key);
            if (string.IsNullOrWhiteSpace(dataJson) || dataJson == "null")
            {
                return default;
            }

            return JsonSerializer.Deserialize<TData>(dataJson);
        }

        public void HasRender()
        {
            this.hasRender = true;
        }
    }
}
