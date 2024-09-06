using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using wxRobotApp.Common;

namespace wxRobotApp.Models
{
    public class MainViewModel
    {
        public ObservableCollection<WeChatProcessInfo> WeChatProcesses { get; set; }
        public ICommand ClickMeCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        private Timer _scanTimer = new Timer();
        public string OutputText { get; set; } = string.Empty;
        public string SettingsText => "Settings";

        public MainViewModel()
        {

        }

    }
}
