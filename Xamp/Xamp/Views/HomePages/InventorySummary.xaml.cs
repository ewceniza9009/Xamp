using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamp.Views.HomePages
{
    public delegate void Click(object o);
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventorySummary : ContentPage
    {
        public InventorySummary()
        {
            InitializeComponent();

            ShowInventorySummary();
        }

        public async void ShowInventorySummary()
        {
            var search = "";
            var lstInventory = new List<Models.InventorySummaryModel>();

            using (var client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SearchItem.Text))
                    {
                        search = "=";
                    }
                    else
                    {
                        search = SearchItem.Text;
                    }

                    var response = await client.GetStringAsync(Services.APIRequest.GetInventorySummaryRequestUri(search));
                    var retval = JsonConvert.DeserializeObject<List<Models.InventorySummaryModel>>(response.ToString());

                    if (retval.Count() > 0)
                    {
                        foreach (var inv in retval.ToList())
                        {
                            lstInventory.Add(inv);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lstInventory.Add(new Models.InventorySummaryModel()
                    {
                        MaterialId = 0,
                        Material = "Error: " + ex.Message
                    });
                }
            }

            listInventory.ItemsSource = lstInventory;
        }

        private void CmdSearchMem_Clicked(object sender, EventArgs e)
        {
            ShowInventorySummary();
        }
    }
}