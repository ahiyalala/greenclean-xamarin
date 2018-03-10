using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreBooking : ContentPage, INotifyPropertyChanged
	{
        

        ObservableCollection<PreBookingViewModel> prebooking = new ObservableCollection<PreBookingViewModel>();
        PreBookingViewModel preBookingViewModel;

        public string ServiceName;

        public PreBooking(string servicename)
        {
            InitializeComponent();
            InvisibleDatePicker.SetValue(DatePicker.MinimumDateProperty, System.DateTime.Now);
            ServiceName = servicename;
            Title = "Set your booking details";
            ListOptions();
            Options.ItemsSource = prebooking;
        }

        public void ListOptions()
        {
            prebooking.Add(new PreBookingViewModel("Service", ServiceName, -1,false));
            prebooking.Add(new PreBookingViewModel("Place", "B1 L1 Example Street, Quezon City",0));
            prebooking.Add(new PreBookingViewModel("Price", "Cash", 1));
            prebooking.Add(new PreBookingViewModel("Date", DateTime.Now.ToShortDateString(), 2));
            prebooking.Add(new PreBookingViewModel("Time", DateTime.Now.ToString("hh:00 tt"), 3));
        }

        public async void UpdateOptions(object sender, SelectedItemChangedEventArgs a)
        {
            (sender as ListView).SelectedItem = null;
            if (a.SelectedItem != null)
            {
                PlacesModel subpageData = a.SelectedItem as PlacesModel;
                preBookingViewModel.OptionValue = subpageData.PlaceDetail;
                await Navigation.PopAsync();
            }
                
        }
        
        private async void OnItemSelect(object sender, SelectedItemChangedEventArgs args)
        {
            (sender as ListView).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                preBookingViewModel = args.SelectedItem as PreBookingViewModel;
                switch (preBookingViewModel.OptionType)
                {
                    case 0:
                        var places = new Places(preBookingViewModel);
                        places.DataSender += UpdateOptions;
                        await Navigation.PushAsync(places);
                        break;
                    case 1:
                        await Navigation.PushAsync(new Payments());
                        break;
                    case 2:
                        InvisibleDatePicker.Focus();
                        InvisibleDatePicker.DateSelected += (sen, e) =>
                        {
                            preBookingViewModel.OptionValue = e.NewDate.ToShortDateString();
                        };
                        break;
                    case 3:
                        InvisiblePicker.Focus();
                        InvisiblePicker.SelectedIndexChanged += (sen, e) =>
                        {
                            preBookingViewModel.OptionValue = InvisiblePicker.SelectedItem.ToString();
                        };
                        break;
                }
            }
            
        }

        async void FindAnAppointment(object sender, EventArgs e)
        {
            prebooking.Add(new PreBookingViewModel("Service type", ServiceName, -1, false));
            Navigation.InsertPageBefore(new BookMeUp(prebooking), this);
            await Navigation.PopAsync();
        }

        
	}
}