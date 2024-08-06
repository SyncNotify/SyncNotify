using iNKORE.UI.WPF.Modern.Common.IconKeys;
using System.Windows;
using System.Windows.Controls;

namespace SyncNotify.Controls
{
    /// <summary>
    /// IconText.xaml 的交互逻辑
    /// </summary>
    public partial class IconText : UserControl
    {
        public IconText()
        {
            InitializeComponent();
        }


        //Icon依赖属性
        public static readonly DependencyProperty IconSourceProperty = 
            DependencyProperty.Register("Icon", typeof(FontIconData), typeof(IconText), new PropertyMetadata(null));
        public object Icon
        {
            get { return (object)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }


        //Text依赖属性
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IconText), new PropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public event RoutedEventHandler Click;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
