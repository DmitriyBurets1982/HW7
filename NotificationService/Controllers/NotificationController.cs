using Contracts.Notifications.Orders;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("notificationservice/[controller]")]
    public class NotificationController(INotificationService notificationService, ILogger<NotificationController> logger) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetLastByAccountId(int id)
        {
            logger.LogInformation("'{MethodName}' with parameter '{Id}' was called", nameof(GetLastByAccountId), id);

            var notification = notificationService.GetLastByAccountId(id);
            if (notification == null)
            {
                return NoContent();
            }

            return Ok(notification);
        }

        [HttpGet("all/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IList<Notification>), StatusCodes.Status200OK)]
        public ActionResult<IList<Notification>> GetAllByAccountBy(int id)
        {
            logger.LogInformation("'{MethodName}' with parameter '{Id}' was called", nameof(GetAllByAccountBy), id);

            var notifications = notificationService.GetAllByAccountId(id);
            if (!notifications.Any())
            {
                return NoContent();
            }

            return Ok(notifications);
        }
    }
}
