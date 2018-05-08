using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    public class PostJobViewModel:ViewBaseModel
    {
        #region fields
        Appointment appointment;
        StarControl star;
        #endregion

        public int Rating { get; set; }
        public ICommand Rate { get; set; }

        public static PostJobViewModel Current { get; set; }

        public PostJobViewModel()
        {
            Rating = 1;
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
