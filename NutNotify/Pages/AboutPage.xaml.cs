using SyncNotify.ViewModels;
using System.IO;
using System;
using System.Windows.Controls;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using System.Windows.Data;

namespace SyncNotify
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : iNKORE.UI.WPF.Modern.Controls.Page
    {

        public AboutViewModel ViewModel
        {
            get;
            set;
        } = new();

        public AboutPage()
        {
            InitializeComponent();
            TextBlock_Version_Number.Text = InternalProper.getVersion();
            //数据上下文别忘了写！！！！！！！！
            DataContext = this;
        }

        

        private void OpensourceLicense(object sender, RoutedEventArgs e)
        {

        }

        private void IconText_Click(object sender,RoutedEventArgs e)
        {
            ContentDialog.Visibility = Visibility.Visible;
            iNKORE.UI.WPF.Modern.Controls.ContentDialog dialog = ContentDialog;
            dialog.Title = "开放源代码许可";
            dialog.PrimaryButtonText = "确定";
            dialog.DefaultButton = iNKORE.UI.WPF.Modern.Controls.ContentDialogButton.Primary;
            dialog.ShowAsync();
        }
        protected override void OnInitialized(EventArgs e)
        {
            var r = new StreamReader(Application.GetResourceStream(new Uri("/Assets/LICENSE.txt", UriKind.Relative))!.Stream);
            ViewModel.License = r.ReadToEnd();
            base.OnInitialized(e);
        }



    }
}
