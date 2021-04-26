using MVVMPairs.Commands;
using MVVMPairs.Models;
using MVVMPairs.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMPairs.ViewModels
{
    class GameVM : BaseNotification
    {
        private GameBusinessLogic bl;
        public GameBusinessLogic Bl
        {
            get { return bl; }
            set
            {
                bl = value;
                NotifyPropertyChanged("Message");
            }
        }
       
        public GameVM()
        {
            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGameBoard();
            bl = new GameBusinessLogic(board);
            GameBoard = CellBoardToCellVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(c.X, c.Y, c.HidenImage, c.DisplayedImage, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }

        public ObservableCollection<ObservableCollection<CellVM>> GameBoard { get; set; }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }
        public void MyAction()
        {
            Dataxml data = new Dataxml();
            data.AfisareStatics();
        }



        private ICommand _clickCommand2;
        public ICommand ClickCommand2
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction2(), () => CanExecute));
            }
        }
       

        public void MyAction2()
        {
          
        }

    }

}


