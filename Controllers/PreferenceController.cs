using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Models.PreferenceModel;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace Owhytee_Phones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly ICartService _preferencesService;

        public PreferenceController(ICartService preferencesService)
        {
            _preferencesService = preferencesService;
        }


        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<PreferenceRequest>> GetSessionPreferences(string sessionId)
        {
            try
            {
                var preferences = await _preferencesService.GetPreferencesAsync(sessionId);
                return Ok(preferences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving session preferences");
            }
        }

        [HttpPut("session")]
        public async Task<ActionResult<PreferenceRequest>> UpdateSessionPreferences(string sessionId,bool MagicMode)
        {
            try
            {
                var preferences = await _preferencesService.UpdatePreferencesAsync(sessionId,MagicMode);
                return Ok(preferences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating session preferences");
            }
        }
    }
}
