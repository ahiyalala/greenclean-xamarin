using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetail : CarouselPage
    {
        public DashboardDetail()
        {
            InitializeComponent();
        }

        async void BookService(object sender, EventArgs a)
        {

            await Navigation.PushAsync(new BookMeUp());
        }
    }
}