namespace Helix.Client.Models;

public class Message
{
    /// <summary>
    /// Gets or sets the identifier represented as a GUID. 
    /// The default value is Guid.Empty.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    /// <summary>
     /// Gets or sets the content as a string. The default value is an empty string.
     /// </summary>
    public string Content { get; set; } = string.Empty;
    /// <summary>
    ///
    /// Gets or sets the sender's name or identifier. The default value is an empty string.
    /// 
    /// </summary>
    public string Sender { get; set; } = string.Empty;
    /// <summary>
    ///
    /// Gets or sets the timestamp value. Initializes to the current date and time when an instance is created.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// Checks if the current Message object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current Message object.</param>
    /// <returns>
    /// True if the specified object is a Message and has the same Id as the current Message; otherwise, false.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is Message otherMessage)
        {
            return this.Id == otherMessage.Id;
        }

        return false;
    }

    /// <summary>
    /// Generates a hash code for the current object using the hash code of the Id property.
    /// </summary>
    /// <returns>
    /// An integer hash code derived from the Id property of the object.
    /// </returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}