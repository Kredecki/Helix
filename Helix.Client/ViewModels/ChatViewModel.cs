using GalaSoft.MvvmLight.Command;
using Helix.Client.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Net.Http.Json;

namespace Helix.Client.ViewModels;

/// <summary>
/// Represents the view model for a chat application screen, implementing the INotifyPropertyChanged interface
/// to support property change notifications, which is typically used for data binding in UI frameworks.
/// </summary>
public class ChatViewModel : INotifyPropertyChanged
{
    /// <summary>
    ///
    /// Gets or sets the collection of messages. This collection is observable, meaning any modifications to it 
    /// will automatically update any UI elements bound to it, such as displaying real-time message updates in a chat application.
    ///
    /// </summary>
    public ObservableCollection<Message> Messages { get; set; }
    /// <summary>
    ///
    /// Gets or sets the new message as a string, with an initial default value of an empty string.
    /// 
    /// </summary>
    public string NewMessage { get; set; } = string.Empty;
    /// <summary>
    ﻿/// Gets or sets the name of the client. The default value is an empty string.
    ﻿/// </summary>
    public string ClientName { get; set; } = string.Empty;
    private readonly DispatcherTimer _timer = new();

    /// <summary>
    /// A command property used to encapsulate the logic for sending a message. 
    /// It can be bound to a user interface element, allowing it to execute the
    /// associated method or function when triggered. 
    /// Typically, this would involve sending data from the client to a server 
    /// or another part of the application.
    /// </summary>
    public ICommand SendMessageCommand { get; set; }

    /// <summary>
    /// Specifies the interval in seconds to refresh or update a messages in chat.
    /// </summary>
    private readonly int SecondsToRefresh = 5;

    /// <summary>
    /// Initializes a new instance of the ChatViewModel class.
    /// Sets up the collection for messages and initializes the command for sending messages.
    /// Also sets up a dispatcher timer to periodically retrieve messages asynchronously every 5 seconds.
    /// </summary>
    public ChatViewModel()
    {
        Messages = new ObservableCollection<Message>();
        SendMessageCommand = new RelayCommand(SendMessage);

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(SecondsToRefresh);
        _timer.Tick += async (s, e) => await GetMessagesAsync();
        _timer.Start();
    }

    /// <summary>
    /// Sends a new message to a specified endpoint if the client name and message content are not empty.
    /// Uses an HTTP POST request with the message serialized as JSON.
    /// If the message is sent successfully, it clears the message input and retrieves the latest messages.
    /// Displays a message box with an error message if the message cannot be sent or if an exception occurs.
    /// </summary>
    private async void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(ClientName))
        {
            MessageBox.Show($"Podaj nazwę klienta!");
            return;
        }

        if (string.IsNullOrWhiteSpace(NewMessage))
        {
            MessageBox.Show($"Wiadomość nie może być pusta!");
            return;
        }

        var message = new Message
        {
            Sender = ClientName,
            Content = NewMessage
        };

        var json = JsonSerializer.Serialize(message);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            using HttpClient client = new();
            var response = await client.PostAsync("https://localhost:7000/api/chat/SendMessage", content);

            if (response.IsSuccessStatusCode)
            {
                NewMessage = string.Empty;
                await GetMessagesAsync();
            }
            else
            {
                MessageBox.Show("Nie udało się wysłać wiadomości.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd: {ex.Message}");
        }
    }

    /// <summary>
    /// Asynchronously retrieves messages from a specified API endpoint and adds them to the Messages collection if they do not already exist.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation to get and process messages.
    /// </returns>
    public async Task GetMessagesAsync()
    {
        try
        {
            using HttpClient client = new();
            var response = await client.GetFromJsonAsync<List<Message>>("https://localhost:7000/api/chat/GetMessages");

            if (response is null) return;

            foreach (var msg in response)
            {
                if (!Messages.Contains(msg))
                {
                    Messages.Add(msg);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas pobierania wiadomości: {ex.Message}");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    /// <summary>
    /// Raises the PropertyChanged event when a property value changes.
    /// </summary>
    /// <param name="name">The name of the property that changed. If not provided, it uses the caller member name.</param>
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}