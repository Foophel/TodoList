using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void OnEntryCompleted(object sender, EventArgs e)
    {
        var vm = (MainViewModel)BindingContext;
        if (!vm.IsAddButtonEnabled) return;

        await AnimateAddButton();
        vm.AddTaskCommand.Execute(null);
        TaskEntry.Unfocus();
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        var vm = (MainViewModel)BindingContext;
        await AnimateAddButton();
        vm.AddTaskCommand.Execute(null);
        TaskEntry.Unfocus();
    }

    private async Task AnimateAddButton()
    {
        await AddButton.ScaleToAsync(0.8, 75, Easing.CubicOut);
        await AddButton.ScaleToAsync(1, 75, Easing.CubicIn);
    }

    // клик по галочке
    private void OnCheckBoxTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is TodoItem task)
        {
            task.IsCompleted = !task.IsCompleted;
            var vm = (MainViewModel)BindingContext;
            vm.SortTask(task);
        }
    }

    //дабл клик по задаче
    private void OnTaskDoubleTapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid && grid.BindingContext is TodoItem task)
        {
            task.IsCompleted = !task.IsCompleted;
            var vm = (MainViewModel)BindingContext;
            vm.SortTask(task);
        }
    }

    // клик по крестику
    private async void OnDeleteTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is TodoItem task)
        {
            var viewModel = (MainViewModel)BindingContext;

            // находим родительский grid элемнета для анимации
            if (border.Parent is Grid taskGrid)
            {
                await Task.WhenAll(
                    taskGrid.TranslateToAsync(150, 0, 300, Easing.CubicIn),
                    taskGrid.FadeToAsync(0, 300, Easing.Linear)
                );
            }

            viewModel.DeleteTaskCommand.Execute(task);
        }
    }
}