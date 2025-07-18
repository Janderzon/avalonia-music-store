using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.MusicStore.Messages;
using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.ViewModels;

public partial class MusicStoreViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;

    [ObservableProperty] public partial string? SearchText { get; set; }

    [ObservableProperty] public partial bool IsBusy { get; private set; }

    [ObservableProperty] public partial AlbumViewModel? SelectedAlbum { get; set; }

    public ObservableCollection<AlbumViewModel> SearchResults { get; } = [];

    partial void OnSearchTextChanged(string? value)
    {
        _ = DoSearch(SearchText);
    }

    private async Task DoSearch(string? term)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;

        IsBusy = true;
        SearchResults.Clear();

        var albums = await Album.SearchAsync(term);

        foreach (var album in albums)
        {
            var vm = new AlbumViewModel(album);
            SearchResults.Add(vm);
        }

        if (!cancellationToken.IsCancellationRequested)
        {
            LoadCovers(cancellationToken);
        }

        IsBusy = false;
    }

    private async void LoadCovers(CancellationToken cancellationToken)
    {
        foreach (var album in SearchResults.ToList())
        {
            await album.LoadCover();
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }
    }

    [RelayCommand]
    private void BuyMusic()
    {
        if (SelectedAlbum != null)
        {
            WeakReferenceMessenger.Default.Send(new MusicStoreClosedMessage(SelectedAlbum));
        }
    }
}