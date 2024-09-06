using Avalonia.Controls;
using wxRobotApp.Common;

namespace wxRobotApp.Views
{
    public partial class DetailsWindow : Window
    {
        public DetailsWindow()
        {
            InitializeComponent();
        }
        public DetailsWindow(WeChatProcessInfo processInfo) : this()
        {
            DataContext = processInfo;
        }
    }
}
