using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MvvmSample.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {

        const string TITLE_CONST = "MvvmSample";

        private string sampleText;

        public string SampleText
        {
            get { return sampleText; }
            set
            {
                sampleText = value;
                NotifyOfPropertyChange(() => (SampleText));
            }
        }

        private string timer;

        public string Timer
        {
            get { return timer; }
            set
            {
                timer = value;
                NotifyOfPropertyChange(() => (Timer));
            }
        }


        private string inputTxt;

        public string InputTxt
        {
            get { return inputTxt; }
            set
            {
                inputTxt = value;
                NotifyOfPropertyChange(() => (InputTxt));
            }
        }

        private int seconds;

        public int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                NotifyOfPropertyChange(() => (Seconds));
            }
        }




        public MainViewModel()
        {
            SampleText = TITLE_CONST;
            //RunTimer();
        }

        private void RunTimer()
        {

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer.Tick += (s, e) =>
            {
                //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff"))

                Timer = DateTime.Now.ToString("hh:mm:ss:ff");
            };
            dispatcherTimer.Start();
        }

    }
}
