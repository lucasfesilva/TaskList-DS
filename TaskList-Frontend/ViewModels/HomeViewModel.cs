using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskList_Frontend.Models;
using TaskList_Frontend.Services;
using TaskList_Frontend.Services.Interfaces;
using TaskList_Frontend.Views;

namespace TaskList_Frontend.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<TaskEntity> Tasks { get; set; } = new ObservableCollection<TaskEntity>();
        private List<TaskEntity> AllTasks { get; set; } = new List<TaskEntity>();

        private TaskEntity _selectedTask;

        private readonly TaskService _taskService = new TaskService();
        private readonly IWindowService _windowService;

        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand OpenNewTaskWindowCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ViewTaskCommand { get; }

        public List<string> StatusList { get; } = new List<string>
        {
            "Todos",
            "Pendente",
            "Em Progresso",
            "Concluído"
        };

        private string _selectedStatus = "Todos";

        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (_selectedStatus != value)
                {
                    _selectedStatus = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }


        public TaskEntity SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            LoadTasksCommand = new AsyncRelayCommand(LoadTasks);
            AddTaskCommand = new AsyncRelayCommand(AddTask);
            OpenNewTaskWindowCommand = new AsyncRelayCommand(OpenNewTaskWindow);
            EditCommand = new RelayCommand<int>(OpenEditTaskWindow);
            DeleteTaskCommand = new RelayCommand<int>(PrepareTaskDelete);

            _ = LoadTasks();
        }

        public async Task LoadTasks()
        {
            var tasks = await _taskService.GetTasksAsync();

            AllTasks = tasks.ToList();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            Tasks.Clear();

            IEnumerable<TaskEntity> filtered = AllTasks;

            if (SelectedStatus != "Todos")
            {
                filtered = filtered.Where(t => t.StatusText == SelectedStatus);
            }

            foreach (var task in filtered)
            {
                Tasks.Add(task);
            }
        }

        private async Task AddTask()
        {
            if (SelectedTask != null)
            {
                await _taskService.AddTask(SelectedTask);
                await LoadTasks();
            }
        }

        private void PrepareTaskDelete(int taskId)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                MessageBox.Show("Tarefa não encontrada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Tem certeza que deseja excluir a tarefa \"{task.TaskTitle}\"?", "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DeleteTask(taskId);
                MessageBox.Show("Tarefa excluída com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task DeleteTask(int taskId)
        {
            await _taskService.DeleteTask(taskId);
            await LoadTasks();
        }

        private async Task OpenNewTaskWindow()
        {
            var window = new NewTaskView();
            var vm = new NewTaskViewModel(_windowService, this);

            vm.RequestClose += async () =>
            {
                window.Close();
                await LoadTasks();
            };

            window.DataContext = vm;
            window.ShowDialog();
        }

        private void OpenEditTaskWindow(int taskId)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                var window = new NewTaskView();
                var vm = new NewTaskViewModel(_windowService, this, task);

                vm.RequestClose += () => window.Close();

                window.DataContext = vm;
                window.ShowDialog();
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
