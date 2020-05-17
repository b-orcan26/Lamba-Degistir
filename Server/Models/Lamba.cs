using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Lamba : INotifyPropertyChanged
    {
        private Boolean acikMi;

        public Boolean AcikMi
        {
            get
            {
                return acikMi;
            }
            set
            {
                acikMi = value;
                OnPropertyChanged("AcikMi");
            }
        }

        private int sayi;
        public int Sayi
        {
            get
            {
                return sayi;
            }
            set
            {
                sayi = value;
                OnPropertyChanged("Sayi");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
