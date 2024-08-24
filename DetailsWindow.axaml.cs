using Avalonia.Controls;

namespace wxRobotApp
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
