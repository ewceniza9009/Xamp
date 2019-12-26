using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace Xamp.Views.OrderPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderList : ContentPage
    {
        public OrderList()
        {
            InitializeComponent();

            ShowOrderList();
        }

        protected override void OnAppearing()
        {
            ShowOrderList();
        }

        public async void ShowOrderList()
        {
            var lstInventory = new List<Models.WithdrawalOrder>();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(Services.APIRequest.GetOrderList());
                    var retval = JsonConvert.DeserializeObject<List<Models.WithdrawalOrder>>(response.ToString());

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
                    lstInventory.Add(new Models.WithdrawalOrder()
                    {
                        Id = 0,
                        WithdrawalOrderNumber = "Error: " + ex.Message
                    });
                }
            }

            listOrder.ItemsSource = lstInventory;
        }

        private async void ListOrder_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Models.WithdrawalOrder;
            await Navigation.PushAsync(new OrderDetail(Id: content.Id));
        }
    }
}