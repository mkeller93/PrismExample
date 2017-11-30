using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrismExample.Common
{
    public class Car : INotifyPropertyChanged
    {
        #region Properties

        private string make;
        public string Make
        {
            get { return make; }
            set
            {
                make = value;
                OnPropertyChanged();
            }
        }
        

        #endregion //Properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged

        public override string ToString()
        {
            return Make;
        }
    }
}
