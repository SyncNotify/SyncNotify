using System.Windows.Controls;

namespace SyncNotify
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            TextBlock_Version_Number.Text = InternalProper.getVersion();
        }

        private void OpensourceLicense(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
