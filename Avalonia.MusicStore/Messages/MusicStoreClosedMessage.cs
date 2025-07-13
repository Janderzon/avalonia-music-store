using Avalonia.MusicStore.ViewModels;

namespace Avalonia.MusicStore.Messages;

public class MusicStoreClosedMessage(AlbumViewModel selectedAlbum)
{
    public AlbumViewModel SelectedAlbum { get; } = selectedAlbum;
}