using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class MainViewModel:BindableBase
    {
        private ObservableCollection<string> _datas;

        public ObservableCollection<string> Datas
        {
            get { return _datas; }
            set { _datas = value; RaisePropertyChanged(); }
        }
        public MainViewModel()
        {
            Datas = new ObservableCollection<string>();
            for (int i = 0; i < 88; i++)
            {
                Datas.Add(i.ToString());
            }
        }

    }
}
