using iNKORE.UI.WPF.Modern.Common.IconKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncNotify.Controls
{
    /// <summary>
    /// Expander.xaml 的交互逻辑
    /// </summary>
    public partial class CostumeExpander : UserControl
    {
        public CostumeExpander()
        {
            InitializeComponent();
        }


        //Icon依赖属性
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("ExpanderIcon", typeof(FontIconData), typeof(IconText), new PropertyMetadata(null));
        public object ExpanderIcon
        {
            get { return (object)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }


        //Text依赖属性
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ExpanderText", typeof(string), typeof(IconText), new PropertyMetadata(string.Empty));

        public string ExpanderText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }



        public object ContentControl
        {
            get { return (object)GetValue(ContentControlProperty); }
            set { SetValue(ContentControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentControlProperty =
            DependencyProperty.Register("ContentControl", typeof(object), typeof(Expander), new PropertyMetadata(null));
    }
}
