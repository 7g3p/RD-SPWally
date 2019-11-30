using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Runtime.CompilerServices;

namespace SPWally
{
    class ViewModelValueOriented : INotifyPropertyChanged
    {
        //Data members
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set
            {
                if(_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged();
                }
            }
        }


        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
