using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    public class PostJobViewModel:ViewBaseModel
    {
        public int Rating { get; set; }
        public ICommand Rate { get; set; }


        public PostJobViewModel()
        {
            Rate = new Command<string>((string args) =>
            {
                var index = int.Parse(args);
                Rating = Stars[index].Rating;
                ResetStars();
                while (index >= 0)
                {
                    Stars[index].Image = "star_rate.png";
                    index--;
                }
            });
        }

        public List<StarControl> Stars = new List<StarControl>(){
            new StarControl(1),
            new StarControl(2),
            new StarControl(3),
            new StarControl(4),
            new StarControl(5)
        };
        
        private void ResetStars()
        {
            foreach (var star in Stars)
            {
                star.Image = "star_rate.png";
            }
        }


    }

    public class StarControl
    {
        public StarControl(int value)
        {
            Rating = value;
            Image = "star_rate_white.png";
        }
        public int Rating { get; set; }

        public string Image { get; set; }
    }
}
