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
        public static bool isOrigin = false;
        public AppointmentDashboardViewmodel(Appointment appointmentargs)
        {
            appointment = appointmentargs;
            var status = (appointment.IsFinished)? "[NEEDS REVIEW]" : "[PENDING]";
            Header = string.Format("{0} {1}",status,appointment.Service.ServiceName);
            Definition = string.Format("Housekeepers: {0} \nLocation: {1} \nPrice: {2}", appointment.Housekeeper.Count.ToString(), appointment.Places.PlaceDetail, appointment.Price);
            MicroText = string.Format("{0} {1}-{2}", appointment.ScheduleDate.ToString("yyyy-MM-dd"), appointment.ScheduleTimeStart.ToString("hh:mm tt"), appointment.ScheduleTimeEnd.ToString("hh:mm tt"));
            ButtonLabel = "View booking";
            SelectTile = new Command(async () =>
            {
                if (!appointmentargs.IsFinished)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new BookingDetailPage(appointment));
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new PostJobPage(appointment.BookingRequestId));
                }
                    
            });
        }



        public string Header { get; set; }
        public string Definition { get; set; }
        public string MicroText { get; set; }
        public string ButtonLabel { get; set; }
        
        public Appointment appointment { get; set; }

        public ICommand SelectTile { protected set; get; }

        public static ObservableCollection<AppointmentDashboardViewmodel> Pending = new ObservableCollection<AppointmentDashboardViewmodel>();
        public static ObservableCollection<AppointmentDashboardViewmodel> Finished = new ObservableCollection<AppointmentDashboardViewmodel>();
        public async static Task GetList()
        {
            await Appointment.GetAppointmentsAsync();
        }
    }
}
