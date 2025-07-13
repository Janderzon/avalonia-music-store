using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        // ViewModel initialization logic.
    }

    [RelayCommand]
    private async Task AddAlbumAsync()
    {
        // Code here will be executed when the button is clicked.
    }
}