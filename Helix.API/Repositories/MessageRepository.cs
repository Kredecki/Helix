using Helix.API.Models;

namespace Helix.API.Repositories;

/// <summary>
/// The MessageRepository class is a singleton that provides a centralized storage for managing Message objects.
/// It allows adding messages and retrieving messages based on a given timestamp.
/// </summary>
public class MessageRepository
{
    private static readonly MessageRepository _instance = new();
    private readonly List<Message> _messages = [];

    private MessageRepository() { }

    public static MessageRepository Instance => _instance;

    public void AddMessage(Message message)
    {
        _messages.Add(message);
    }

    public List<Message> GetMessagesSince(DateTime lastCheck)
    {
        return _messages.Where(m => m.Timestamp > lastCheck).ToList();
    }
}
