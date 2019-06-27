using ProbaWPF.mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbaWPF
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Test Application";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private double _Position;
        public double Position
        {
            get => _Position;
            set => Set(ref _Position, value);
        }

        public MainWindowViewModel()
        {

        }
    }
}
