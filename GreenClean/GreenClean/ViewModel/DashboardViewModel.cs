using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using GreenClean.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using GreenClean.Utilities;

namespace GreenClean.ViewModel
{
    class DashboardViewModel:ViewBaseModel
    {
        static string serviceUri = Constants.BaseUri + "/api/services";

        

        public DashboardViewModel(Services serviceargs)
        {
            service = serviceargs;
            Header = service.ServiceName;
            Definition = service.Description;
            MicroText = service.Price;
            ButtonLabel = "Book now";
            SelectTile = new Command(async () =>
            {
                var appointmentRequest = new AppointmentRequest()
                {
                    Service = service,
                    Place = PlacesModel.PlacesList.FirstOrDefault(),
                    Customer = Customer.Current,
                    Payment = PaymentModel.GetFirstPaymentData(),
                    Date = null,
                    Time = null
                };
                await Application.Current.MainPage.Navigation.PushAsync(new PreBooking(appointmentRequest));
            });
        }

        

        public string Header { get; set; }
        public string Definition { get; set; }
        public string MicroText { get; set; }
        public string ButtonLabel { get; set; }

        public Services service { get; set; }

        public ICommand SelectTile { protected set; get; }

        public static ObservableCollection<DashboardViewModel> All = new ObservableCollection<DashboardViewModel>();

        public async static Task GetList()
        {
            HttpClient client = new HttpClient();

            var request = await client.GetAsync(serviceUri).ConfigureAwait(false);

            if (request.IsSuccessStatusCode && All.Count == 0)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<IList<Services>>(content);
                foreach(var x in model)
                {
                    All.Add(new DashboardViewModel(x));
                }
            }
            
        }
    }
}
