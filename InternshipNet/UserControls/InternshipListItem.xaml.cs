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
using System.Windows.Shapes;
using InternshipNet.ViewModels;

namespace InternshipNet.UserControls
{
    public partial class InternshipListItem : UserControl
    {
        public InternshipListItem() { InitializeComponent(); }

        public static readonly DependencyProperty InternshipProperty =
            DependencyProperty.Register(
                "Internship",
                typeof(InternshipViewModel),
                typeof(InternshipListItem),
                new PropertyMetadata(null));

        public InternshipViewModel Internship 
        {
            get { return (InternshipViewModel)GetValue(InternshipProperty); } 
            set { SetValue(InternshipProperty, value); }
        }
    }
}
