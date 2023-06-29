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
    /// Логика взаимодействия для CommissionView.xaml
    /// </summary>
    public partial class CommissionView : Window
    {
        public CommissionView()
        {
            InitializeComponent();
            DataContext = new CommissionViewModel(this);
        }

        public void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WorkerView workerView = new WorkerView();
            workerView.Show();

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MetingView metingView = new MetingView();
            metingView.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            VisitView visitView = new VisitView();
            visitView.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MembersView membersView = new MembersView();
            membersView.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
