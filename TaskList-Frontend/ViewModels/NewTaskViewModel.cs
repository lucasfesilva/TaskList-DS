using System;
using System.Collections.Generic;
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

namespace TaskList_Frontend.ViewModels
{
    public class NewTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly TaskService _taskService = new TaskService();
        private readonly IWindowService _windowService;
        private readonly HomeViewModel _homeViewModel;
        public event Action RequestClose;

        public TaskEntity Task {  get; set; } = new TaskEntity();

        public List<string> StatusList { get; set; } = new List<string> { "Pendente", "Em Progresso", "Concluído" };

        public string _selectedStatus = "Pendente";

        

        public string Title
        {
            get => Task.TaskTitle;
            set
            {
                Task.TaskTitle = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => Task.TaskDescription;
            set
            {
                Task.TaskDescription = value;
                OnPropertyChanged();
            }
        }

        public DateTime? SelectedDate
        {
            get => Task.DoneAt;
            set
            {
                Task.DoneAt = value;
                OnPropertyChanged();
            }
        }

        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (_selectedStatus == value) return;

                _selectedStatus = value;
                OnPropertyChanged();

                Task.Status = value switch
                {
                    "Pendente" => 0,
                    "Em Progresso" => 1,
                    "Concluído" => 2,
                    _ => 0
                };

                if (value == "Concluído")
                {
                    Task.DoneAt = DateTime.Now;
                    OnPropertyChanged(nameof(SelectedDate));
                }
                else
                {
                    Task.DoneAt = null;
                    OnPropertyChanged(nameof(SelectedDate));
                }

                OnPropertyChanged(nameof(IsDatePickerEnabled));
            }
        }

        public bool IsStatusEnabled => Task.StatusText != "Concluída";

        public bool IsDatePickerEnabled => Task.Status == 2;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }


        public NewTaskViewModel(IWindowService windowService, HomeViewModel homeViewModel, TaskEntity? task = null)
        {
            _windowService = windowService;
            _homeViewModel = homeViewModel;

            if (task != null)
            {
                Task = task;
                _selectedStatus = task.Status switch
                {
                    0 => "Pendente",
                    1 => "Em Progresso",
                    2 => "Concluído",
                    _ => "Pendente"
                };
            }
            else
            {
                Task = new TaskEntity
                {
                    Status = 0,
                    CreatedAt = DateTime.Now
                };
                _selectedStatus = "Pendente";
            }

                SaveCommand = new AsyncRelayCommand(async () => await SaveTask());
            CancelCommand = new AsyncRelayCommand(() => Close());
        }

        

        private async Task SaveTask()
        {
            if (string.IsNullOrWhiteSpace(Task.TaskTitle))
            {
                MessageBox.Show("O título da tarefa é obrigatório.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(Task.TaskDescription) && Task.TaskDescription.Length > 100)
            {
                MessageBox.Show("A descrição deve ter no máximo 100 caracteres.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Task.DoneAt.HasValue && Task.DoneAt < Task.CreatedAt)
            {
                MessageBox.Show("A data de conclusão não pode ser anterior à data de criação.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(Task.Id == 0)
            {
                Task.Status = 0;
                await _taskService.AddTask(Task);
                MessageBox.Show("Tarefa criada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                await _taskService.UpdateTask(Task);
                MessageBox.Show("Tarefa atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            await _homeViewModel.LoadTasks();

            RequestClose?.Invoke();
        }

        private async Task Close()
        {
            RequestClose?.Invoke();
        }



        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
