using GreenClean.DependencyServices;
using GreenClean.Model;
using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    public class PostJobViewModel:ViewBaseModel
    {
        #region fields
        Appointment appointment;
        bool enabled;
        #endregion

        public int Rating { get; set; }
        public ICommand Rate { get; set; }
        
        public INavigation Navigation { get; set; }

        public static PostJobViewModel Current { get; set; }

        public ICommand SendRating { get; set; }
        public bool IsEnabled {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        
        public string Comment { get; set; }

        public PostJobViewModel()
        {
            IsEnabled = true;
            Rating = 1;
            SendRating = new Command(async () => await SendRatingAsync());
            Stars = new List<StarControl>(){
                    new StarControl(1){
                        Image = ImageSource.FromResource("GreenClean.Assets.star_rate_white.png")
                    },
                    new StarControl(2),
                    new StarControl(3),
                    new StarControl(4),
                    new StarControl(5)
                };
            Rate = new Command<string>((string args) =>
            {
                var index = int.Parse(args);
                
                if(Stars[index].Rating < Rating)
                {
                    for (int x = 4; x > index; x--)
                    {
                        Stars[x].Image = ImageSource.FromResource("GreenClean.Assets.star_rate.png");
                    }
                }
                else
                {
                    for (int x = 0; x <= index; x++)
                    {
                        if(Stars[x].Rating > Rating)
                            Stars[x].Image = ImageSource.FromResource("GreenClean.Assets.star_rate_white.png");
                    }
                }
                Rating = Stars[index].Rating;
            });
        }

        public async Task SendRatingAsync()
        {
            IsEnabled = false;
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;

            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));
            var feedback = new
            {
                service_cleaning_id = AppointmentData.BookingRequestId,
                customer_id = Customer.Current.CustomerId,
                housekeeper = AppointmentData.Housekeeper,
                rating = Rating,
                comment = Comment
            };
            var feedbackJson = JsonConvert.SerializeObject(feedback);
            var postRequest = new StringContent(feedbackJson, Encoding.UTF8, "application/json");

            var request = await client.PostAsync(string.Format("{0}/api/feedback/", Constants.BaseUri), postRequest);

            if (request.IsSuccessStatusCode)
            {
                DependencyService.Get<IMessage>().ShortAlert("Feedback sent!");
                AppointmentDashboardViewmodel.isOrigin = true;
                await Navigation.PopAsync();
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(string.Format("{0}: Error sending feedback", request.StatusCode.ToString()));
                IsEnabled = true;
            }

            
        }

        public Appointment AppointmentData
        {
            get
            {
                return appointment;
            }
            set
            {
                appointment = value;
                OnPropertyChanged("AppointmentData");
            }
        }

        public List<StarControl> Stars { get; set; }

    }

    public class StarControl:ViewBaseModel
    {
        #region fields
        ImageSource source;
        #endregion
        public StarControl(int value)
        {
            Rating = value;
            Image = ImageSource.FromResource("GreenClean.Assets.star_rate.png");
        }
        public int Rating { get; set; }

        public ImageSource Image
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                OnPropertyChanged("Image");
            }
        }
        
    }
}
