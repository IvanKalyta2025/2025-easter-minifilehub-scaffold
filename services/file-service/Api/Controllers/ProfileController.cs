// using Microsoft.AspNetCore.Mvc;
// using Api.Models;
// using Api.Services;

// namespace Api.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]

//     public class ProfileController : ControllerBase
//     {
//         private readonly ProfileService _profileService;

//         public ProfileController(ProfileService profileService)
//         {
//             _profileService = profileService;
//         }
//         [HttpPost("{userId}/create")]
//         public async Task<IActionResult> CreateProfile(
//             [FromRoute] Guid userId,
//             [FromForm] string firstName,
//             [FromForm] string lastName,
//             [FromForm] IFormFile avatarUrl)
//         {
//         }
//     }
// }