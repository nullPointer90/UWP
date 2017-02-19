using Couple.Command;
using Couple.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Couple.ViewModel
{
    class GameMgn : BindableBase
    {
        private Data _data;

        private MatrixMgn _matrixMgn;

        public MatrixMgn MatrixData
        {
            get { return _matrixMgn; }
            set { _matrixMgn = value; }
        }

        private int _typeGame;

        public int TypeGame
        {
            get { return _typeGame; }
            set { SetProperty(ref _typeGame, value); }
        }

        private ObservableCollection<ItemMgn> _listData;
        public ObservableCollection<ItemMgn> ListData
        {
            get { return _listData; }
            set { SetProperty(ref _listData,value); }
        }

        private int _stateGame;
        public int StateGame
        {
            get { return _stateGame; }
            set { SetProperty(ref _stateGame, value); }
        }

        private bool _isEnableControl;
        public bool IsEnableControl
        {
            get { return _isEnableControl; }
            set { SetProperty(ref _isEnableControl, value); }
        }

        private Visibility _visibleBkgndGameOver;
        public Visibility VisibleBkgndGameOver
        {
            get { return _visibleBkgndGameOver; }
            set { SetProperty(ref _visibleBkgndGameOver, value); }
        }

        public GameMgn()
        {
            InitGame();
        }

        public void InitGame()
        {
            _matrixMgn = new MatrixMgn();
            _typeGame = (int)GameDef.Type.CHARACTER;
            _isEnableControl = true;
            _visibleBkgndGameOver = Visibility.Collapsed;
            SetSizeMatrix(GameDef.SIZE_5);
            CreateListData();
        }

        public void CreateListData()
        {
            if (ListData == null)
                _listData = new ObservableCollection<ItemMgn>();
            ListData.Clear();
            if(_typeGame == (int)GameDef.Type.CHARACTER)
            {
                _data = new Data(new DataCharacters());
            }
            else if(_typeGame == (int)GameDef.Type.COLOR)
            {
                _data = new Data(new DataColors());
            }
            List<string> datas = _data.GetRndListData(CalCountListData());
            AddDataToList(datas);
        }
 
        private int CalCountListData()
        {
            return (int)((MatrixData.SizeMatrix * MatrixData.SizeMatrix) / 2);
        }

        private void AddDataToList(List<string> data)
        {
            if (data == null || ListData ==null)
                return;
            List<ItemMgn> tempData = new List<ItemMgn>();
            //tempData.Add(new ItemMgn() { Content = " ", Color = "#000000", Visible = Visibility.Visible });
            if (_typeGame == (int)GameDef.Type.CHARACTER)
            {
                foreach(var item in data)
                {
                    tempData.Add(new ItemMgn() { Content = item, Color = ""});
                    tempData.Add(new ItemMgn() { Content = item, Color = ""});
                }
            }
            else if (_typeGame == (int)GameDef.Type.COLOR)
            {
                foreach (var item in data)
                {
                    tempData.Add(new ItemMgn() { Color = item, Content = ""});
                    tempData.Add(new ItemMgn() { Color = item, Content = ""});
                }
            }
            CreaateRndList(tempData);
        }

        private  void CreaateRndList(List<ItemMgn> tempData)
        {
            if (tempData == null)
                return;
            ListData?.Add(new ItemMgn() { Content = " ", Color = "#000000", Visible = Visibility.Visible });
            Random rnd = new Random();
            while(tempData.Count > 0)
            {
                int rndItem = rnd.Next(0, tempData.Count);
                ListData?.Add(tempData.ElementAt(rndItem));
                tempData?.RemoveAt(rndItem);
            }
        }

        internal void SetAllListVisible()
        {
            throw new NotImplementedException();
        }

        public void SetSizeMatrix(int size)
        {
            _matrixMgn.SizeMatrix = size;
            switch(size)
            {
                case GameDef.SIZE_5:
                    _matrixMgn.WidthMatrix = GameDef.WIDTH_MATRIX_5;
                    break;
                case GameDef.SIZE_7:
                    _matrixMgn.WidthMatrix = GameDef.WIDTH_MATRIX_7;
                    break;
                case GameDef.SIZE_9:
                    _matrixMgn.WidthMatrix = GameDef.WIDTH_MATRIX_9;
                    break;
                default:
                    break;
            }
        }

        public async void SetAllListVisible(int timeDelay)
        {
            if (ListData == null)
                return;
            for (int i = 0; i < ListData.Count; i++)
            {
                ListData[i].TextVisible = Visibility.Visible;
            }
            await Task.Delay(timeDelay);
            for (int i = 0; i < ListData.Count; i++)
            {
                ListData[i].TextVisible = Visibility.Collapsed;
            }
        }

        public void ShowDataWhenGameOver()
        {
            if (ListData == null)
                return;
            for(int i = 0; i < ListData.Count; i++)
            {
                if(ListData[i].Visible == Visibility.Visible)
                {
                    ListData[i].TextVisible = Visibility.Visible;
                }
            }
        }
    }
}
