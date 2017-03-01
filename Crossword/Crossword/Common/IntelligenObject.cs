using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Crossword.Common
{
    class IntelligenObject
    {
        private ObservableCollection<string> _listDataIntro1;
        public ObservableCollection<string> ListDataIntro1
        {
            get { return _listDataIntro1; }
            set { _listDataIntro1 = value; }
        }

        private ObservableCollection<string> _listDataIntro2;
        public ObservableCollection<string> ListDataIntro2
        {
            get { return _listDataIntro2; }
            set { _listDataIntro2 = value; }
        }
        private ObservableCollection<string> _listDataIntro3;
        public ObservableCollection<string> ListDataIntro3
        {
            get { return _listDataIntro3; }
            set { _listDataIntro3 = value; }
        }
        private ObservableCollection<string> _listDataIntro4;
        public ObservableCollection<string> ListDataIntro4
        {
            get { return _listDataIntro4; }
            set { _listDataIntro4 = value; }
        }

        private ObservableCollection<string> _listDataCorrect;
        public ObservableCollection<string> ListDataCorrect
        {
            get { return _listDataCorrect; }
            set { _listDataCorrect = value; }
        }

        private List<string> _listData;
        public List<string> ListData
        {
            get { return _listData; }
            set { _listData = value; }
        }

        public IntelligenObject()
        {
            _listData = new List<string>();
        }

        private void InitListDataCorrect(int count)
        {
            if (_listDataCorrect == null)
                _listDataCorrect = new ObservableCollection<string>();
            _listDataCorrect.Clear();
            for (int i = 0; i < count; i++)
            {
                _listDataCorrect.Add("");
            }
        }

        public int InitListDataIntro1(int count)
        {
            if (_listDataIntro1 == null)
                _listDataIntro1 = new ObservableCollection<string>();
            _listDataIntro1.Clear();
            for (int i = 0; i < count; i++)
            {
                _listDataIntro1.Add(_listData[i]);
            }
            Random rnd = new Random();
            int indexListCorrect = rnd.Next(0, ListDataIntro1.Count);
            _listDataCorrect[indexListCorrect] = ListDataIntro1[indexListCorrect];
            return indexListCorrect;
        }

        public int InitListDataIntro2(int count, int indexCorrect1)
        {
            if (_listDataIntro2 == null)
                _listDataIntro2 = new ObservableCollection<string>();
            _listDataIntro2.Clear();
            for (int i = 3; i < 3 + count; i++)
            {
                _listDataIntro2.Add(_listData[i]);
            }
            Random rnd = new Random();
            int id = rnd.Next(0, _listDataIntro2.Count);
            int indexCorrect2 = rnd.Next(0, count);
            while(indexCorrect2 == indexCorrect1)
            {
                indexCorrect2 = rnd.Next(0, count);
            }
            _listDataCorrect[indexCorrect2] = ListDataIntro2[indexCorrect1];
            return indexCorrect2;
        }

        public void InitListDataIntro3(int count, int indexCorrect1, int indexCorrect2)
        {
            if (_listDataIntro3 == null)
                _listDataIntro3 = new ObservableCollection<string>();
            _listDataIntro3.Clear();
            for(int j = 0; j < count; j++)
            {
                _listDataIntro3.Add("");
            }
            int indexCorrect3 = 0;
            for(int i = 0; i < count; i++)
            {
                if(i != indexCorrect1 && i != indexCorrect2)
                {
                    indexCorrect3 = i;
                    break;
                }
            }
            _listDataIntro3[indexCorrect3] = _listDataCorrect[indexCorrect1];
            Random rnd = new Random();
            for(int i = 6; i < _listData.Count; i++)
            {
                for (int l = 0; l < _listDataIntro3.Count; l++)
                {
                    if (_listDataIntro3[l] == "")
                    {
                        _listDataIntro3[l] = _listData[i];
                        break;
                    }
                }
            }
            _listDataCorrect[indexCorrect3] = _listDataIntro3[indexCorrect2];
        }
        public void InitListDataIntro4()
        {
            if (_listDataIntro4 == null)
                _listDataIntro4 = new ObservableCollection<string>();
            _listDataIntro4.Clear();
            foreach (var item in ListData)
            {
                if (!_listDataCorrect.Contains(item))
                {
                    _listDataIntro4.Add(item);
                    if (_listDataIntro4.Count == GameDef.COUNT_CHARACTER_INTEL)
                        break;
                }
            }

        }
        public void InitListData(int count)
        {
            InitListDataCorrect(count);
            int indexCorrect1 = InitListDataIntro1(count);
            int indexCorrect2 = InitListDataIntro2(count, indexCorrect1);
            InitListDataIntro3(count, indexCorrect1, indexCorrect2);
            InitListDataIntro4();
        }
    }
}
