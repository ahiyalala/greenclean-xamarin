using GreenClean.Model;
using GreenClean.Utilities;
using GreenClean.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreBooking : ContentPage, INotifyPropertyChanged
	{
        

        ObservableCollection<PreBookingViewModel> prebooking = new ObservableCollection<PreBookingViewModel>();
        PreBookingViewModel preBookingViewModel;
        
        private AppointmentRequest appointmentRequest;
        private bool hasAppeared;

        public PreBooking(AppointmentRequest appointmentargs)
        {
            InitializeComponent();
            InvisibleDatePicker.SetValue(DatePicker.MinimumDateProperty, System.DateTime.Now);
            appointmentRequest = appointmentargs;
            hasAppeared = false;
            Title = "Set your booking details";
            Options.ItemsSource = prebooking;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(PlacesModel.PlacesList.Count > 0 && !hasAppeared)
            {
                ListOptions();
                hasAppeared = true;
            }
            else if (PlacesModel.PlacesList.Count == 0)
            {
                await DisplayAlert("No places available", "Go to Places and add a place", "Take me");
                var placeform = new PlacesForm();
                placeform.DataSender += AddList;
                await Navigation.PushAsync(placeform);
            }
        }

        public async void AddList(object sender, FormEvent args)
        {
            var obj = args.Object as PlacesModel;
            var isSuccessful = await PlacesModel.AddToList(obj);
            if (isSuccessful)
            {
                appointmentRequest.Place = PlacesModel.PlacesList.FirstOrDefault();
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Try again");
            }
            await Navigation.PopAsync();
        }


        public void ListOptions()
        {
            prebooking.Add(new PreBookingViewModel("Service", appointmentRequest.Service.ServiceName, -1,false));
            prebooking.Add(new PreBookingViewModel("Place", appointmentRequest.Place.PlaceDetail, 0));
            prebooking.Add(new PreBookingViewModel("Price", appointmentRequest.Payment.PaymentDetail, 1));
            prebooking.Add(new PreBookingViewModel("Date", appointmentRequest.Date, 2));
            prebooking.Add(new PreBookingViewModel("Time", appointmentRequest.Time, 3));
        }

        public async void UpdatePlace(object sender, SelectedItemChangedEventArgs a)
        {
            (sender as ListView).SelectedItem = null;
            if (a.SelectedItem != null)
            {
                PlacesModel subpageData = a.SelectedItem as PlacesModel;
                appointmentRequest.Place = subpageData;
                preBookingViewModel.OptionValue = appointmentRequest.Place.PlaceDetail;
                await Navigation.PopAsync();
            }
                
        }

        public async void UpdatePaymentType(object sender, SelectedItemChangedEventArgs a)
        {
            (sender as ListView).SelectedItem = null;
            if (a.SelectedItem != null)
            {
                PaymentModel subpageData = a.SelectedItem as PaymentModel;
                appointmentRequest.Payment = subpageData;
                preBookingViewModel.OptionValue = appointmentRequest.Payment.PaymentDetail;
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
                        var places = new Places();
                        places.DataSender += UpdatePlace;
                        await Navigation.PushAsync(places);
                        break;
                    case 1:
                        var payments = new Payments();
                        payments.DataSender += UpdatePaymentType;
                        await Navigation.PushAsync(payments);
                        break;
                    case 2:
                        InvisibleDatePicker.Focus();
                        InvisibleDatePicker.DateSelected += (sen, e) =>
                        {
                            appointmentRequest.Date = e.NewDate.ToString("yyyy-MM-dd");
                            preBookingViewModel.OptionValue = appointmentRequest.Date;
                        };
                        break;
                    case 3:
                        InvisiblePicker.Focus();
                        InvisiblePicker.SelectedIndexChanged += (sen, e) =>
                        {
                            appointmentRequest.Time = InvisiblePicker.SelectedItem.ToString();
                            preBookingViewModel.OptionValue = appointmentRequest.Time;
                        };
                        break;
                }
            }
            
        }

        async void FindAnAppointment(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new BookMeUp(appointmentRequest), this);
            await Navigation.PopAsync();
        }

        
	}
}