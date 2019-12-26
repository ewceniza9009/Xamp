using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;

namespace Xamp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CmdLogin_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    bool isUserEmpty = string.IsNullOrEmpty(userMem.Text.Trim());
                    bool isPassEmpty = string.IsNullOrEmpty(passMem.Text.Trim());

                    if (isUserEmpty || isPassEmpty)
                    {
                        loginStat.Text = "User or password can neither be empty";

                        return;
                    }

                    if (!string.IsNullOrEmpty(urlLinkMem.Text))
                    {
                        Services.APIRequest.requestUriBase = urlLinkMem.Text;
                    }

                    var response = await client.GetStringAsync(
                        Services.APIRequest.GetLoginRequestUri(userMem.Text, passMem.Text));
                    var retval = JsonConvert.DeserializeObject<object>(response.ToString());

                    if (bool.Parse(retval.ToString()))
                    {
                        loginStat.Text = "Successfully Logged-In.";

                        await Navigation.PushAsync(new MasterPage());
                    }
                    else
                    {
                        loginStat.Text = "User not found.";
                    }
                }
                catch (Exception ex)
                {
                    loginStat.Text = "There is an error: " + ex.Message;
                }
            }
        }   
    }
}