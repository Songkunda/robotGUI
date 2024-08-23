using Avalonia.Controls;

namespace wxRobotApp
{
    public partial class DetailsWindow : Window
    {
        public DetailsWindow(WeChatProcessInfo processInfo)
        {
            InitializeComponent();
            DataContext = processInfo;
        }
    }
}
