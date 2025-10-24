using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList_Frontend.Services.Interfaces
{
    public interface IWindowService
    {
        void ShowWindow<TViewModel>() where TViewModel : class;
        bool? ShowDialog<TViewModel>() where TViewModel : class;
    }
}
