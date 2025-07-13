using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.MusicStore.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.Views;

public partial class MusicStoreWindow : Window
{
    public MusicStoreWindow()
    {
        InitializeComponent();

        // Register a handler to listen for the message sent by the view model.
        WeakReferenceMessenger.Default.Register<MusicStoreWindow, MusicStoreClosedMessage>(this,
            static (window, message) =>
            {
                // Close the dialog and return the selected album.
                window.Close(message.SelectedAlbum);
            });
    }
}