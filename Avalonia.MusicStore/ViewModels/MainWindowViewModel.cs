using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Avalonia.MusicStore.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<AlbumViewModel> Albums { get; } = [];

    public MainWindowViewModel()
    {
        // ViewModel initialization logic.
    }

    [RelayCommand]
    private async Task AddAlbumAsync()
    {
        // Send the message to the previously registered handler and await the selected album
        var album = await WeakReferenceMessenger.Default.Send(new PurchaseAlbumMessage());
        if (album is not null)
        {
            Albums.Add(album);
            await album.SaveToDiskAsync();
        }
    }
}