using MVVMPairs.Models;
using MVVMPairs.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace MVVMPairs.Services
{

    class GameBusinessLogic : INotifyPropertyChanged
    {
        private static string COLOR_BLACK = "/MVVMPairs;component/Resources/black.jpg";
        private static string COLOR_GRAY =  "/MVVMPairs;component/Resources/gray.png";
        private static string KING_WHITE = "/MVVMPairs;component/Resources/whitepieces.png";
        private static string KING_BLACK = "/MVVMPairs;component/Resources/blackpieces.png";
        private bool turn = false;
        private bool capture = false;
        private string _message="White turn!";


       

        public string Message
        {
            private set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
            get
            {
                return _message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ObservableCollection<Cell>> cells;
        private int[,] board = {
                                { 0, 1, 0, 1, 0, 1, 0, 1},
                                { 1, 0, 1, 0, 1, 0, 1, 0},
                                { 0, 1, 0, 1, 0, 1, 0, 1},
                                { 0, 0, 0, 0, 0, 0, 0, 0},
                                { 0, 0, 0, 0, 0, 0, 0, 0},
                                { 2, 0, 2, 0, 2, 0, 2, 0},
                                { 0, 2, 0, 2, 0, 2, 0, 2},
                                { 2, 0, 2, 0, 2, 0, 2, 0}
        };

        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> cells)
        {
            this.cells = cells;
        }

        private void TurnCard(Cell cell, string player)
        {
            cell.HidenImage = player;
        }

        private void TurnCardBack(Cell cell)
        {
            cell.HidenImage = "";
        }


        public bool verify(Cell current, Cell previous)
        {
            if (previous.DisplayedImage != COLOR_GRAY
                || current.DisplayedImage != COLOR_BLACK)
                return false;

            if (current.HidenImage != "" && previous.HidenImage != "")
                return false;

            if (!logicMove(current, previous))
                return false;

            return true;
        }

        public bool verifyIndex(int x)
        {
            return (x >= 0 && x < 8);
        }

        public bool verifyCapture(Cell cell, int val, int val2)
        {
            if (verifyIndex(cell.X - val) && verifyIndex(cell.Y - 1) && (board[cell.X - val, cell.Y - 1] == val2 || board[cell.X - val, cell.Y - 1] == val2 + 2))
            {
                if (verifyIndex(cell.X - val * 2) && verifyIndex(cell.Y - 2) && board[cell.X - val * 2, cell.Y - 2] == 0)
                    return true;
            }

            if (verifyIndex(cell.X - val) && verifyIndex(cell.Y + 1) && (board[cell.X - val, cell.Y + 1] == val2 || board[cell.X - val, cell.Y + 1] == val2 + 2))
            {
                if (verifyIndex(cell.X - val * 2) && verifyIndex(cell.Y + 2) && board[cell.X - val * 2, cell.Y + 2] == 0)
                    return true;
            }

            return false;
        }

        public bool verifyMove(Cell current, Cell previous, int val, int val2)
        {
            if (current.X == previous.X + (val * 1))
            {
                if (!(current.Y == previous.Y - 1 || current.Y == previous.Y + 1))
                    return false;
            }
            else
                if (current.X == previous.X + (val * 2))
            {
                if (current.Y == previous.Y - 2)
                {
                    if (!(board[previous.X + (val * 1), previous.Y - 1] == val2 || board[previous.X + (val * 1), previous.Y - 1] == val2 + 2))
                        return false;
                    else
                    {
                        board[previous.X + (val * 1), previous.Y - 1] = 0;
                        cells[previous.X + (val * 1)][previous.Y - 1].HidenImage = "";
                        if (board[previous.X, previous.Y] > 2)
                        {
                            capture = verifyCapture(current, 1, val2) || verifyCapture(current, -1, val2);
                        }
                        else
                           if (board[previous.X, previous.Y] == 2)
                            capture = verifyCapture(current, 1, val2);
                        else
                            capture = verifyCapture(current, -1, val2);
                    }
                }
                else
                    if (current.Y == previous.Y + 2)
                {
                    if (!(board[previous.X + (val * 1), previous.Y + 1] == val2 || board[previous.X + (val * 1), previous.Y + 1] == val2 + 2))
                        return false;
                    else
                    {
                        board[previous.X + (val * 1), previous.Y + 1] = 0;
                        cells[previous.X + (val * 1)][previous.Y + 1].HidenImage = "";
                        if (board[previous.X, previous.Y] > 2)
                        {

                            capture = verifyCapture(current, 1, val2) || verifyCapture(current, -1, val2);
                        }
                        else
                                    if (board[previous.X, previous.Y] == 2)
                            capture = verifyCapture(current, 1, val2);
                        else
                            capture = verifyCapture(current, -1, val2);
                    }
                }
                else
                    return false;
            }
            else
                    if (!(current.X == previous.X + (val * 2) || current.X == previous.X + (val * 1)))
            {
                return false;
            }

            return true;
        }

        public bool logicMove(Cell current, Cell previous)
        {

            if (board[previous.X, previous.Y] == 1)
            {
                if (!verifyMove(current, previous, +1, 2))
                    return false;
            }

            if (board[previous.X, previous.Y] == 2)
            {
                if (!verifyMove(current, previous, -1, 1))
                    return false;
            }

            if (board[previous.X, previous.Y] == 3)
            {
                if (!verifyMove(current, previous, +1, 2) && !verifyMove(current, previous, -1, 2))
                    return false;
            }

            if (board[previous.X, previous.Y] == 4)
            {
                if (!verifyMove(current, previous, +1, 1) && !verifyMove(current, previous, -1, 1))
                    return false;
            }
            return true;
        }

        public void checkKing(Cell current)
        {
            if (current.X == 0 && board[current.X, current.Y] == 2)
            {
                board[current.X, current.Y] = 4;
                current.HidenImage = KING_WHITE;
            }

            if (current.X == 7 && board[current.X, current.Y] == 1)
            {
                board[current.X, current.Y] = 3;
                current.HidenImage = KING_BLACK;
            }
        }

        public void afisare(int[,] board)
        {
            for(int index=0;index<8;index++)
            {
                for (int index2 = 0; index2 < 8; index2++)
                {
                    Console.Write(board[index, index2]+" ");
                }
                Console.WriteLine();
            }
        }

        public bool checkTurn(Cell current)
        {
            if(turn)
            {
                if (board[current.X, current.Y] == 1 || board[current.X, current.Y] == 3)
                        return true;
                return false;
            }
            else
            {
                if (board[current.X, current.Y] == 2 || board[current.X, current.Y] == 4)
                    return true;
                return false;
            }
        }

        private void showMessageTurn(bool turn)
        {
            if(turn)
            {
                {
                    _message = "Black turn!";
                    NotifyPropertyChanged("Message");
                }
            }
            else
            {
                {
                    _message = "White turn!";
                    NotifyPropertyChanged("Message");               
                }
            }
        }

        private bool checkWinner()
        {
            Dataxml data = new Dataxml();
            bool piecesWhite = false;
            bool piecesBlack = false;
            for (int index = 0; index < 8; index++)
            {
                for (int index2 = 0; index2 < 8; index2++)
                {
                    if (board[index, index2] == 2 || board[index, index2] == 4)
                    { 
                        piecesWhite = true; 
                    }
                    else
                        if (board[index, index2] == 1 || board[index, index2] == 3)
                    {
                        piecesBlack = true;
                    }
                }  
            }
            
            if (piecesWhite == false)
            {
                _message = "Black Winner!";
                NotifyPropertyChanged("Message");
                data.ModificareStatistics(1);
                return true;
               
            }
            else
                if (piecesBlack == false)
            {
                _message = "White Winner!";
                NotifyPropertyChanged("Message");
                data.ModificareStatistics(2);
                return true;
            }
            else
            {
                return false ;
            }
            
        }

        private int x;
        private int y;

       

        public void Move(Cell currentCell)
        {
            
            bool existCurrentCell = false;
            if (capture)
            {
                if (currentCell.HidenImage != "" && checkTurn(currentCell) && currentCell.X==x && currentCell.Y==y)
                {
                    Helper.CurrentCell = currentCell;
                    existCurrentCell = true;
                    currentCell.DisplayedImage = COLOR_GRAY;
                }

            }
            else
            if (currentCell.HidenImage != "" && checkTurn(currentCell))
            {
                Helper.CurrentCell = currentCell;
                existCurrentCell = true;
                currentCell.DisplayedImage = COLOR_GRAY; 
            }
            else existCurrentCell = false;

            if (Helper.PreviousCell != null && verify(currentCell, Helper.PreviousCell))
            {
                if (Helper.PreviousCell.HidenImage != "" )
                {
                   
                    TurnCard(currentCell, Helper.PreviousCell.HidenImage);
                    TurnCardBack(Helper.PreviousCell);
                    Helper.PreviousCell.DisplayedImage = COLOR_BLACK;
                    SwapNum(ref board[currentCell.X, currentCell.Y], ref board[Helper.PreviousCell.X, Helper.PreviousCell.Y]);
                    afisare(board);
                    checkKing(currentCell);
                    if(capture)
                    {
                        x = currentCell.X;
                        y = currentCell.Y;
                    }
                    if (!capture)
                    {
                        turn = !turn;
                        if (checkWinner() == false)
                        {
                            showMessageTurn(turn);
                        }
                    }

                }
                existCurrentCell = false;
                Helper.PreviousCell = null;
            }
            
          
            if (Helper.PreviousCell != null && Helper.PreviousCell.HidenImage != "")
            {
                Helper.PreviousCell.DisplayedImage = COLOR_BLACK;
                Helper.PreviousCell = null;
            }


            if (existCurrentCell)
                Helper.PreviousCell = currentCell;
        }

        private static void SwapNum(ref int x, ref int y)
        {
            int tempswap = x;
            x = y;
            y = tempswap;
        }

        public void ClickAction(Cell obj)
        {
            Move(obj);
        }
    }
}
