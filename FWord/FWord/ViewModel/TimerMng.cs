using FWord.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FWord.ViewModel
{
    class TimerMng : BindableBase
    {
        #region event
        public event Action CoundownEventHandler;
        #endregion

        private int _coundown;

        public string _pathImageNumber;
        public string PathImageNumber
        {
            get { return _pathImageNumber; }
            set { SetProperty(ref _pathImageNumber, value); }
        }

        private DispatcherTimer _timer;
        public DispatcherTimer TimerCountDown
        {
            get { return _timer; }
            set { _timer = value; }
        }

        public TimerMng()
        {
            IniTial();
        }

        private void IniTial()
        {
            SetPathImageNumber(9);
            _coundown = 9;
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (_coundown == 0)
            {
                if (CoundownEventHandler != null)
                {
                    CoundownEventHandler();
                }
            }
            else
            {
                _coundown--;
                SetPathImageNumber(_coundown);
            }        
        }

        public void SetPathImageNumber(int number)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("ms-appx:///Assets/Numbers/{0}.png", number);
            PathImageNumber = builder.ToString();
        }

        public void Start()
        {
            TimerCountDown.Start();
        }

        public void Stop()
        {
            TimerCountDown.Stop();
        }

        public void Reset()
        {
            _coundown = 9;
            TimerCountDown.Stop();
            SetPathImageNumber(9);
            TimerCountDown.Start();
        }
    }
}
