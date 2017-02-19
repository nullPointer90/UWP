using Couple.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Couple.ViewModel
{
    class TimerMgn : BindableBase
    {
        public event Action GameOverEventHandler = null;
        private DispatcherTimer _timerCountDown;

        private int _valCountDown;
        public int ValCountDown
        {
            get { return _valCountDown; }
            set { SetProperty(ref _valCountDown, value); }
        }

        public TimerMgn()
        {
            _timerCountDown = new DispatcherTimer();
            _timerCountDown.Tick += _timerCountDown_Tick;
            _timerCountDown.Interval = new TimeSpan(0, 0, 1);
            _valCountDown = GameDef.TIME_5;
        }

        private void _timerCountDown_Tick(object sender, object e)
        {
            ValCountDown--;
            if(ValCountDown <= 0)
            {
                GameOverEventHandler?.Invoke();
            }
        }

        public void Start()
        {
            _timerCountDown.Start();
        }
        public void Stop()
        {
            _timerCountDown.Stop();
        }
    }
}
