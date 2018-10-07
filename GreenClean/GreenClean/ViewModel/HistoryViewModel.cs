using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    public class HistoryViewModel : ViewBaseModel
    {
        public HistoryViewModel(INavigation navigator)
        {
            Navigator = navigator;
        }

        public INavigation Navigator { get; set; }

        public ObservableCollection<HistoryEntry> FinishedAppointments = new ObservableCollection<HistoryEntry>();

        public async void OpenDetailsPage(Appointment appointment)
        {
            await Navigator.PushAsync(new BookingDetailPage(appointment));
        }

        public async Task GetFinishedAppointments()
        {
            var appointmentList = await Appointment.GetFinishedList();
            foreach(var appointment in appointmentList)
            {
                FinishedAppointments.Add(new HistoryEntry(appointment, Navigator));
            }
        }

        public class HistoryEntry : ViewBaseModel
        {
            public HistoryEntry(Appointment appointment, INavigation navigation)
            {
                Service = appointment.Service.ServiceName;
                Schedule = string.Format("{0} {1} - {2}", appointment.ScheduleDate.ToString("yyyy-MM-dd"), appointment.ScheduleTimeStart.ToString("hh:mm tt"), appointment.ScheduleTimeEnd.ToString("hh:mm tt"));
                RatingAndPrice = string.Format("Php {0} | {1}", appointment.Price, appointment.Rating);
                Basis = appointment;
                Navigator = navigation;
                SelectAppointment = new Command(async () =>
                {
                    await Navigator.PushAsync(new BookingDetailPage(Basis));
                });
            }
            public INavigation Navigator { get; set; }
            public Appointment Basis { get; set; }
            public string Service { get; set; }
            public string Schedule { get; set; }
            public string RatingAndPrice { get; set; }
            public ICommand SelectAppointment { get; set; }
        }
    }
}
