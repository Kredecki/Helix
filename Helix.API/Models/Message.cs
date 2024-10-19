namespace Helix.API.Models;

/// <summary>
/// Represents a message with an identifier, content, sender, and timestamp. 
/// </summary>
public class Message
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Content { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
}