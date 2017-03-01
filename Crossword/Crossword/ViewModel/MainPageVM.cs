using Crossword.Command;
using Crossword.Common;
using Crossword.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Crossword.ViewModel
{
    class MainPageVM
    {
        private int _typeGame;
        public MainPageVM()
        {
            _typeGame = (int)GameDef.Type.CLASSIC;
        }

        #region Command
        private DelegateCommand _btnPlayClick;
        public DelegateCommand PlayClick
        {
            get
            {
                return _btnPlayClick = _btnPlayClick ?? new DelegateCommand(
                    param =>
                    {
                        Frame rootFrame = Window.Current.Content as Frame;
                        switch(_typeGame)
                        {
                            case (int)GameDef.Type.CLASSIC:
                                rootFrame?.Navigate(typeof(Classic));
                                break;
                            case (int)GameDef.Type.INTELLIGEN:
                                rootFrame?.Navigate(typeof(Intelligen));
                                break;
                            default:
                                break;
                        }
                    });
            }
        }

        private DelegateCommand _typeGameChecked;
        public DelegateCommand TypeGameChecked
        {
            get
            {
                return _typeGameChecked = _typeGameChecked ?? new DelegateCommand(
                    param =>
                    {
                        string type = param as string;
                        if(type == "1")
                        {
                            _typeGame = (int)GameDef.Type.CLASSIC;
                        }
                        else if(type == "2")
                        {
                            _typeGame = (int)GameDef.Type.INTELLIGEN;
                        }
                        else
                        {
                            //error type
                        }
                    });
            }
        }
        #endregion
    }
}
