using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncNotify.ViewModels
{
    public class AboutViewModel : ObservableRecipient
    {
        private string _license = "";

        public string License
        {
            get => _license;
            set
            {
                if (value == _license) return;
                _license = value;
                OnPropertyChanged();
            } 
        }
    }
}
