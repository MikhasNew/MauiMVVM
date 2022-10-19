using MauiMVVM.Model;
using System.Net.Http.Json;

namespace MauiMVVM.Service
{
    public class DataItemService
    {
        List<DataItem> dataItemsList = new();
        HttpClient httpClient;
        public DataItemService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<DataItem>> GetDataItems()
        {
            if(dataItemsList.Count>0)
                return dataItemsList;
            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");
            if (response.IsSuccessStatusCode)
            {
                dataItemsList = await response.Content.ReadFromJsonAsync<List<DataItem>>();
                return dataItemsList;
            }
            return dataItemsList;
        }
    }



}
