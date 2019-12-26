using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamp.Views.OrderPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetail : ContentPage
    {
        public int OrderId { get; set; }

        public OrderDetail(int Id)
        {
            InitializeComponent();

            this.OrderId = Id;

            ShowOrderDetail();
        }

        public async void ShowOrderDetail()
        {
            Models.WithdrawalOrderDetail retval = null;
            var lstOrderMaterial = new List<Models.WithdrawalOrderMaterial>();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(Services.APIRequest.GetOrderDetail(this.OrderId));
                    retval = JsonConvert.DeserializeObject<Models.WithdrawalOrderDetail>(response.ToString());

                    if (retval != null)
                    {
                        foreach (var orderMat in retval.WithdrawalOrderMaterial.ToList())
                        {
                            lstOrderMaterial.Add(orderMat);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lstOrderMaterial.Add(new Models.WithdrawalOrderMaterial()
                    {
                        MaterialName = "Error: " + ex.Message
                    });
                }

                BindingContext = retval;
            }

        }

        private async void CmdApprove_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(Services.APIRequest.ApproveWithdrawalOrder(this.OrderId));
                    JsonConvert.DeserializeObject<Models.WithdrawalOrderDetail>(response.ToString());


                }
            }
            catch { }

            await Navigation.PushAsync(new OrdersPage());
        }
    }
}