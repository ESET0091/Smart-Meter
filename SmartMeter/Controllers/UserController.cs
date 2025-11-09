using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeter.Models.DTOs;
using SmartMeter.Services;
using SmartMeter.Services.UserServices;
using System.Security.Claims;

namespace SmartMeter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all endpoints
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public readonly IUserServices _userServices;

        public UserController(IAuthService authService, IUserServices userServices)
        {
            _authService = authService;
            _userServices = userServices;
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto request)
        {
            // Get user ID from JWT token
            Console.WriteLine(request.CurrentPassword);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
            {
                return Unauthorized("Invalid token");
            }

            // Validate request
            if (string.IsNullOrEmpty(request.CurrentPassword) ||
                string.IsNullOrEmpty(request.NewPassword) ||
                string.IsNullOrEmpty(request.ConfirmNewPassword))
            {
                return BadRequest("All fields are required");
            }

            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return BadRequest("New password and confirmation do not match");
            }

            // Attempt to change password
            var result = await _authService.ChangePasswordAsync(userId, request);

            if (!result)
            {
                return BadRequest("Failed to change password. Please check your current password.");
            }

            return Ok("Password changed successfully");
        }

        [HttpGet("consumption-history")]

        public async Task<IActionResult> GetTotalEnergyConsumed([FromQuery] int orgUnitId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {

            startDate = startDate.ToUniversalTime();
            endDate = endDate.ToUniversalTime();
            // Validate the date range
            if (startDate > endDate)
                return BadRequest("Start date cannot be greater than end date.");

            // Get the historical consumption data from the service
            var historicalData = await _userServices.GetHistoricalConsumptionAsync(orgUnitId, startDate, endDate);

            // Check if no data is found for the provided OrgUnit and date range
            if (historicalData == null)
                return NotFound("No data found for the given OrgUnit and date range.");

            // Return the result
            return Ok(historicalData);
        }


    }
}