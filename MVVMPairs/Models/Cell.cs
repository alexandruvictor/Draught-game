using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMPairs.Models
{
    
    class Cell : INotifyPropertyChanged
    {
        public Cell(int x, int y, string hidden, string displayed)
        {
            this.X = x;
            this.Y = y;
            this.HidenImage = hidden;
            this.DisplayedImage = displayed;
        }

        /* Am optat sa fac proprietati notificabile aici; o alta varianta ar fi fost sa lucrez in Services cu obiecte ViewModel
        care sunt notificabile, dar aceasta optiune o gasesc mai potrivita pentru MVVM */
        //public int X { get; set; }
        //public int Y { get; set; }
        //public string DisplayedImage { get; set; }
        //public string HidenImage { get; set; }

        private int x;
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged("X");
            }
        }
        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged("Y");
            }
        }

        private string hidenImage;
        public string HidenImage
        {
            get { return hidenImage; }
            set
            {
                hidenImage = value;
               // NotifyPropertyChanged("DisplayedImage");
                NotifyPropertyChanged("HidenImage");
            }
        }




        private string displayedImage;
        public string DisplayedImage
        {
            get { return displayedImage; }
            set
            {
                displayedImage = value;
                //hidenImage = "/MVVMPairs;component/Resources/p1.jpg";
                NotifyPropertyChanged("DisplayedImage");



            }
        }





        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
