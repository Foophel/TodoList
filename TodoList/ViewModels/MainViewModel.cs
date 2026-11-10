using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;

namespace TodoList.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public ObservableCollection<TodoItem> Tasks { get; } = new();

    [ObservableProperty]
    private string _newTaskTitle = string.Empty;

    public bool IsAddButtonEnabled => !string.IsNullOrWhiteSpace(NewTaskTitle);

    partial void OnNewTaskTitleChanged(string value)
    {
        OnPropertyChanged(nameof(IsAddButtonEnabled));
    }

    [RelayCommand]
    private void AddTask()
    {
        if (string.IsNullOrWhiteSpace(NewTaskTitle)) return;
        Tasks.Insert(0, new TodoItem { Title = NewTaskTitle.Trim() });
        NewTaskTitle = string.Empty;
    }

    [RelayCommand]
    private void DeleteTask(TodoItem task)
    {
        if (task != null && Tasks.Contains(task)) Tasks.Remove(task);
    }

    public void SortTask(TodoItem task)
    {
        if (task == null) return;

        int oldIndex = Tasks.IndexOf(task);
        if (oldIndex == -1) return;

        Tasks.RemoveAt(oldIndex);

        int targetIndex;
        if (task.IsCompleted)
        {
            int firstCompleted = -1;
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].IsCompleted)
                {
                    firstCompleted = i;
                    break;
                }
            }
            targetIndex = firstCompleted != -1 ? firstCompleted : Tasks.Count;
        }
        else
        {
            targetIndex = 0;
        }

        Tasks.Insert(targetIndex, task);
    }
}