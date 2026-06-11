using KYC.Interface;
using KYC.Models.RequestModels;
using KYC.Models.ResponseModels;
using KYC.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYC.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KycController : ControllerBase
    {
        private readonly IKycService _kycservice;

        public KycController(IKycService kycservcie)
        {
            _kycservice = kycservcie;
        }

        [HttpPost("verify")]

        public async Task<IActionResult> VerifyDocuments([FromBody] KYCVerficationRequest request)
        {
            var userId = "1"; // hardcoded for now, JWT will replace this later
            var result = await _kycservice.VerifyKycAsync(request, userId);
            return Ok(result);
        }


        [HttpGet("status/{id}")]
        public async Task<IActionResult> GetStatus(int uid)
        {
            try
            {
                var result = await _kycservice.GetVerificationStatusAsync(uid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetHistory(string userId)
        {
            var result = await _kycservice.GetVerificationHistoryAsync(userId);
            return Ok(result);
        }



    }
}
