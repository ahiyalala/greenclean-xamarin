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


        private AppointmentRequest appointmentRequest;
        private float Price {
            get
            {
                if (appointmentRequest.Service.ServiceName.Contains("Commercial"))
                {
                    var additional_area = appointmentRequest.Area - 50;
                    return price + (additional_area / 10 * 150);
                }
                else
                {
                    var basePrice = int.Parse(appointmentRequest.Service.Price);
                    return appointmentRequest.Housekeepers * basePrice;
                }
                
            }
        }
        private string SubDetails
        {
            get
            {
                return string.Format("Php {0} with {1} housekeepers", Price.ToString(),appointmentRequest.Housekeepers.ToString());
            }
        }
        private float price;
        
        public PreBooking(AppointmentRequest appointmentargs)
        {
            InitializeComponent();

            ServiceImage.Source = string.Format("{0}/img/{1}",Constants.BaseUri, appointmentargs.Service.ServiceImage);
            float.TryParse(appointmentargs.Service.Price, out price);
            ServiceDescription.Text = appointmentargs.Service.ServiceName;

            SelectedLocation.Text = appointmentargs.Place.PlaceDetail;
            SelectedPayment.Text = appointmentargs.Payment.PaymentDetail;
            SelectedTime.Text = "Pick a time";
            SelectedDate.Text = "Pick a date";
            AreaStepper.IsVisible = false;
            HousekeeperCount.Text = appointmentargs.Housekeepers.ToString();
            Sub.IsEnabled = false;
            Sub.BorderColor = SubSymbol.TextColor = Color.LightGray;
            Add.IsEnabled = true;

            InvisibleDatePicker.SetValue(DatePicker.MinimumDateProperty, System.DateTime.Now);
            appointmentRequest = appointmentargs;
            
            if (appointmentRequest.Service.ServiceName.Contains("Commercial"))
            {
                appointmentRequest.Area = 50;
                AreaCount.Text = appointmentRequest.Area.ToString();
                HousekeeperStepper.IsVisible = false;
                AreaStepper.IsVisible = true;
                appointmentRequest.Area = 50;
                SubArea.BorderColor = SubSymbolArea.TextColor = Color.LightGray;
            }
            ServicePrice.Text = this.SubDetails;
            Title = "Set your booking details";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (PlacesModel.PlacesList.Count == 0)
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

            AreaEnabler();
            Enabler();

            if(appointmentRequest.Date != null && appointmentRequest.Time != null && appointmentRequest.Place != null)
            {
                SelectedDate.Text = appointmentRequest.Date;
                SelectedTime.Text = appointmentRequest.Time;
                SelectedLocation.Text = appointmentRequest.Place.PlaceDetail;
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
            ServicePrice.Text = SubDetails;
            HousekeeperCount.Text = appointmentRequest.Housekeepers.ToString();
            if (appointmentRequest.Housekeepers == 1)
            {
                Sub.IsEnabled = false;
                Sub.BorderColor = SubSymbol.TextColor = Color.LightGray;
                
            }
            else
            {
                Sub.IsEnabled = true;
                Sub.BorderColor = SubSymbol.TextColor = Color.FromHex("#4CAF50");
            }

            if (appointmentRequest.Housekeepers == 3)
            {
                Add.IsEnabled = false;
                Add.BorderColor = AddSymbol.TextColor = Color.LightGray;

            }
            else
            {
                Add.IsEnabled = true;
                Add.BorderColor = AddSymbol.TextColor = Color.FromHex("#4CAF50");
            }
        }

        private void AddHousekeeper(object sender, EventArgs args)
        {

            appointmentRequest.Housekeepers++;
            Enabler();

        }

        private void SubtractArea(object sender, EventArgs args)
        {
            appointmentRequest.Area -= 10;
            AreaEnabler();
        }

        private void AddArea(object sender, EventArgs args)
        {
            appointmentRequest.Area += 10;
            AreaEnabler();
        }

        private void AreaEnabler()
        {
            appointmentRequest.Housekeepers = (appointmentRequest.Area - 1) / 100 + 1;
            ServicePrice.Text = SubDetails;
            AreaCount.Text = appointmentRequest.Area.ToString();
            if (appointmentRequest.Area == 50)
            {
                SubArea.IsEnabled = false;
                SubArea.BorderColor = SubSymbolArea.TextColor = Color.LightGray;

            }
            else
            {
                SubArea.IsEnabled = true;
                SubArea.BorderColor = SubSymbolArea.TextColor = Color.FromHex("#4CAF50");
            }
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