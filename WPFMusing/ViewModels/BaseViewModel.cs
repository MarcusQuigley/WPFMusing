using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPFMusing.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
             
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
