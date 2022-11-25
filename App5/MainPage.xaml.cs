using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RestSharp;
using Newtonsoft.Json;
using App5.Model;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace App5
{
    public partial class MainPage : ContentPage
    {
        private string apikey = "68AD6A04-198E-40BC-AAF7-89217784451F";
        private string baseImageUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_256/";
        private List<Coin> coin;
        public MainPage()
        {
            InitializeComponent();
            coinListView.ItemsSource = Getcoin();

        }


        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            coinListView.ItemsSource = Getcoin();
        }

        private List<Coin> Getcoin()
        {
            List<Coin> coin;

            var client = new RestClient("http://rest.coinapi.io/v1/assets?filter_asset_id=BTC;LTC;ETH;DOGE;KRW");
            var request = new RestRequest();
            request.AddHeader("X-CoinAPI-Key", apikey);

            var response = client.Execute(request);

            coin = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach(var c in coin)
            {
                if (!String.IsNullOrEmpty(c.Id_icon))
                    c.Icon_Url = baseImageUrl + c.Id_icon.Replace("-", "") + ".png";
                else
                    c.Icon_Url = "";
            }

            return coin;
        }
    }
}
