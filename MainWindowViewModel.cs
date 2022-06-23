using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SHQueryRecycleBin
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public long RecycleBinSize { get; set; }

        public MainWindowViewModel()
        {
            RecycleBinSize = 10000;
        }
    }
}
