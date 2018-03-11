using GreenClean.Model;
using GreenClean.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentForm : ContentPage
	{
        public event EventHandler<FormEvent> DataSender;

        public PaymentForm()
        {
            InitializeComponent();
        }

		public PaymentForm (PaymentModel obj)
		{
			InitializeComponent ();
		}

        
	}
}