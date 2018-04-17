using GreenClean.Model;
using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    class AppointmentDashboardViewmodel:ViewBaseModel
    {
        static string serviceUri = Constants.BaseUri + "/api/services";

        public AppointmentDashboardViewmodel(Appointment appointmentargs)
        {
            appointment = appointmentargs;
            Header = "[BOOKED] " + appointment.Service.ServiceName;
            Definition = string.Format("Household keeper: {0} \nLocation: {1} \nPrice: {2}", appointment.Housekeeper.FullName, appointment.Places.PlaceDetail, appointment.Service.Price);
            MicroText = string.Format("{0} {1}-{2}", appointment.ScheduleDate.ToString("yyyy-MM-dd"), appointment.ScheduleTimeStart.ToString("hh:00 tt"), appointment.ScheduleTimeEnd.ToString("hh:00 tt"));
            ButtonLabel = "View booking";
            SelectTile = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new BookingDetailPage(appointment));
            });
        }



        public string Header { get; set; }
        public string Definition { get; set; }
        public string MicroText { get; set; }
        public string ButtonLabel { get; set; }
        
        public Appointment appointment { get; set; }

        public ICommand SelectTile { protected set; get; }

        public static ObservableCollection<AppointmentDashboardViewmodel> All = new ObservableCollection<AppointmentDashboardViewmodel>();

        public async static Task GetList()
        {
            await Task.Delay(1000);

        }
    }
}
