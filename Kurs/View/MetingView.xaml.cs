using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Kurs.ViewModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kurs.View
{
    /// <summary>
    /// Логика взаимодействия для MetingView.xaml
    /// </summary>
    public partial class MetingView : Window
    {
        public MetingView()
        {
            InitializeComponent();
            DataContext = new MetingViewModel(this);
        }
    }
}
