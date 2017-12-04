using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismExample.Domain
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
