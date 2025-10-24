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
using TaskList_Frontend.ViewModels;

namespace TaskList_Frontend.Views
{
    /// <summary>
    /// Lógica interna para NewTaskView.xaml
    /// </summary>
    public partial class NewTaskView : Window
    {
        public NewTaskView()
        {
            InitializeComponent();
            if (DataContext is NewTaskViewModel vm)
            {
                vm.RequestClose += () => this.Close();
            }
        }
    }
}
