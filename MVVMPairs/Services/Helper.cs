using MVVMPairs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMPairs.Services
{
    class Helper
    {
        private static string COLOR_BLACK = "/MVVMPairs;component/Resources/black.jpg";
        private static string COLOR_WHITE = "/MVVMPairs;component/Resources/white.jpg";
        private static string PIECES_WHITE= "/MVVMPairs;component/Resources/white_man.png";
        private static string PIECES_BLACK = "/MVVMPairs;component/Resources/black_man.png";
        public static Cell CurrentCell { get; set; }
        public static Cell PreviousCell { get; set; }


        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> collection = new ObservableCollection<ObservableCollection<Cell>>();

            for (int i = 0; i < 8; ++i)
            {
                ObservableCollection<Cell> cells = new ObservableCollection<Cell>();
                for (int j = 0; j < 8; ++j)
                {
                    if ((i + j) % 2 == 0)
                    {
                        cells.Add(new Cell(i, j, "", COLOR_WHITE));
                    }
                    else
                    {
                        if (i < 3)
                            cells.Add(new Cell(i, j, PIECES_BLACK, COLOR_BLACK));
                        else
                            if (i > 4)
                            cells.Add(new Cell(i, j, PIECES_WHITE, COLOR_BLACK));
                        else
                            cells.Add(new Cell(i, j, "", COLOR_BLACK));
                    }
                }
                collection.Add(cells);
            }


            return collection;

        }
    }
}
