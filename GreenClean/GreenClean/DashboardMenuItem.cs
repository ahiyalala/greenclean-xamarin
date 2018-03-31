using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean
{

    public class DashboardMenuItem
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public Page TargetType { get; set; }

        public string VerticalLayout { get; set; }
    }
}