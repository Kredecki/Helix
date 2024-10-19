using Helix.API.Models;
using Helix.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Helix.API.Controllers;

/// <summary>
/// Indicates that an attribute is used to configure a controller to handle HTTP API requests. 
/// This is typically used in ASP.NET Core applications to define a RESTful web service or a web API.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    /// <summary>
    /// Handles the HTTP POST request to send a new message. This method generates a unique identifier and timestamp for the message
    /// before saving it to the message repository.
    /// </summary>
    /// <param name="message">The message object containing the content to be sent.</param>
    /// <returns>
    /// An IActionResult indicating the outcome of the send operation, typically returning an HTTP 200 OK status.
    /// </returns>
    [HttpPost("SendMessage")]
    public IActionResult SendMessage([FromBody] Message message)
    {
        message.Id = Guid.NewGuid();
        message.Timestamp = DateTime.UtcNow;
        MessageRepository.Instance.AddMessage(message);
        return Ok();
    }

    /// <summary>
    /// Retrieves new messages that have been added since the specified datetime.
    /// </summary>
    /// <param name="lastCheck">The DateTime representing the last time messages were checked.</param>
    /// <returns>
    /// An IActionResult containing a collection of messages retrieved since the specified datetime.
    /// </returns>
    [HttpGet("GetMessages")]
    public IActionResult GetMessages(DateTime lastCheck)
    {
        var messages = MessageRepository.Instance.GetMessagesSince(lastCheck);
        return Ok(messages);
    }
}