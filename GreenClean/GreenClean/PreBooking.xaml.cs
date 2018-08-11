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
        private float price;
        
        public PreBooking(AppointmentRequest appointmentargs)
        {
            InitializeComponent();

            ServiceImage.Source = string.Format("{0}/img/{1}",Constants.BaseUri, appointmentargs.Service.ServiceImage);
            float.TryParse(appointmentargs.Service.Price, out price);
            ServicePrice.BindingContext = appointmentargs.Service;
            ServiceDescription.Text = appointmentargs.Service.ServiceName;

            SelectedLocation.Text = appointmentargs.Place.PlaceDetail;
            SelectedPayment.Text = appointmentargs.Payment.PaymentDetail;
            SelectedTime.Text = "Pick a time";
            SelectedDate.Text = "Pick a date";
            HousekeeperCount.Text = appointmentargs.Housekeepers.ToString();
            Sub.IsEnabled = false;
            Add.IsEnabled = true;

            InvisibleDatePicker.SetValue(DatePicker.MinimumDateProperty, System.DateTime.Now);
            appointmentRequest = appointmentargs;
            hasAppeared = false;
            Title = "Set your booking details";
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
                var response = await DisplayAlert("No places available", "Go to Places and add a place", "Ok","Go back");
                if (response)
                {
                    var placeform = new PlacesForm();
                    placeform.DataSender += AddList;
                    await Navigation.PushAsync(placeform);
                }
                else
                {
                    await Navigation.PopAsync();
                }
                
            }
        }

        public async void AddList(object sender, FormEvent args)
        {
            var obj = args.Object as PlacesModel;
            var isSuccessful = await PlacesModel.AddToList(obj);
            if (isSuccessful)
            {
                appointmentRequest.Place = PlacesModel.PlacesList.FirstOrDefault();
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Try again");
            }
        }


        public void ListOptions()
        {
            prebooking.Add(new PreBookingViewModel("Place", appointmentRequest.Place.PlaceDetail, 0));
            prebooking.Add(new PreBookingViewModel("Price", appointmentRequest.Payment.PaymentDetail, 1));
            prebooking.Add(new PreBookingViewModel("Date", "Select a date", 2));
            prebooking.Add(new PreBookingViewModel("Time", "Select a time", 3));
        }

        public async void UpdatePlace(object sender, SelectedItemChangedEventArgs a)
        {
            (sender as ListView).SelectedItem = null;
            if (a.SelectedItem != null)
            {
                PlacesModel subpageData = a.SelectedItem as PlacesModel;
                appointmentRequest.Place = subpageData;
                SelectedLocation.Text = appointmentRequest.Place.PlaceDetail;
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
                SelectedPayment.Text = appointmentRequest.Payment.PaymentDetail;
                await Navigation.PopAsync();
            }

        }

        private async void SelectPayment(object sender,EventArgs args)
        {
            var payments = new Payments();
            payments.DataSender += UpdatePaymentType;
            await Navigation.PushAsync(payments);
        }

        private void SelectDate(object sender, EventArgs args)
        {
            InvisibleDatePicker.Focus();
            InvisibleDatePicker.DateSelected += (sen, e) =>
            {
            appointmentRequest.Date = e.NewDate.ToString("yyyy-MM-dd");
            SelectedDate.Text = appointmentRequest.Date;
            };
        }

        private void SelectTime(object sender, EventArgs args)
        {
            InvisiblePicker.Focus();
            InvisiblePicker.SelectedIndexChanged += (sen, e) =>
            {
            appointmentRequest.Time = InvisiblePicker.SelectedItem.ToString();
            SelectedTime.Text = appointmentRequest.Time;
            };
        }

        private async void SelectLocation(object sender, EventArgs args)
        {
            var places = new Places();
            places.DataSender += UpdatePlace;
            await Navigation.PushAsync(places);
                    
        }

        private void SubtractHousekeeper(object sender, EventArgs args)
        {
            appointmentRequest.Housekeepers--;
            
            Enabler();

        }

        private void Enabler()
        {
            HousekeeperCount.Text = appointmentRequest.Housekeepers.ToString();
            if (appointmentRequest.Housekeepers == 1)
            {
                Sub.IsEnabled = false;
            }
            else
            {
                Sub.IsEnabled = true;
            }

            if (appointmentRequest.Housekeepers == 3)
            {
                Add.IsEnabled = false;
            }
            else
            {
                Add.IsEnabled = true;
            }
        }

        private void AddHousekeeper(object sender, EventArgs args)
        {
            appointmentRequest.Housekeepers++;

            Enabler();

        }

        async void FindAnAppointment(object sender, EventArgs e)
        {
            if(appointmentRequest.Time == null)
            {
                await DisplayAlert("Invalid request", "Select a time", "Try again");
            }
            else if(appointmentRequest.Date == null)
            {
                await DisplayAlert("Invalid request", "Select a date", "Try again");
            }
            else
            {
                Navigation.InsertPageBefore(new BookMeUp(appointmentRequest), this);
                await Navigation.PopAsync();
            }
        }

        
	}
}