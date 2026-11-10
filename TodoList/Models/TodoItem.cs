using CommunityToolkit.Mvvm.ComponentModel;

namespace TodoList.Models;

public partial class TodoItem : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isCompleted;
}