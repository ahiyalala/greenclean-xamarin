using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMaster : ContentPage
    {
        public ListView ListView;

        public DashboardMaster()
        {
            InitializeComponent();

            BindingContext = new DashboardMasterViewModel();
            ListView = MenuItemsListView;
        }

        class DashboardMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DashboardMenuItem> MenuItems { get; set; }
            
            public DashboardMasterViewModel()
            {
                MenuItems = new ObservableCollection<DashboardMenuItem>(new[]
                {
                    new DashboardMenuItem { Id = 0, Title = "Page 1" },
                    new DashboardMenuItem { Id = 1, Title = "Page 2" },
                    new DashboardMenuItem { Id = 2, Title = "Page 3" },
                    new DashboardMenuItem { Id = 3, Title = "Page 4" },
                    new DashboardMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}