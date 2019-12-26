using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public List<MenuItems.MenuPageItem> menuList { get; set; }

        public MasterPage()
        {
            InitializeComponent();

            menuList = new List<MenuItems.MenuPageItem>();

            // Adding menu items to menuList and you can define title ,page and icon
            menuList.Add(new MenuItems.MenuPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(Views.HomePage) });
            menuList.Add(new MenuItems.MenuPageItem() { Title = "Orders", Icon = "purchase_order.png", TargetType = typeof(Views.OrdersPage) });
            menuList.Add(new MenuItems.MenuPageItem() { Title = "Setting", Icon = "setting.png", TargetType = typeof(Views.SettingPage) });
            menuList.Add(new MenuItems.MenuPageItem() { Title = "Help", Icon = "help.png", TargetType = typeof(Views.HelpPage) });
            menuList.Add(new MenuItems.MenuPageItem() { Title = "LogOut", Icon = "logout.png", TargetType = typeof(Views.LogoutPage) });

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Views.HomePage)));

        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuItems.MenuPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if (page != typeof(Views.LogoutPage))
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                IsPresented = false;
            }
            else
            {
                Navigation.PushAsync(new MainPage());
            }
        }

        private void CmdHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MasterPage());
        }
    }
}