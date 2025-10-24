using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskList_Frontend.Services.Interfaces;

namespace TaskList_Frontend.Services
{
    public class WindowService : IWindowService
    {
        public void ShowWindow<TViewModel>() where TViewModel : class
        {
            var window = CreateWindow(typeof(TViewModel));
            window.Show();
        }

        public bool? ShowDialog<TViewModel>() where TViewModel : class
        {
            var window = CreateWindow(typeof(TViewModel));
            return window.ShowDialog();
        }

        private Window CreateWindow(Type windowType)
        {
            Window window = windowType.Name switch
            {
                "NewTaskView" => new Views.NewTaskView(),
            };

            var vm = Activator.CreateInstance(windowType);
            window.DataContext = vm;
            return window;
        }
    }
}
