using KYC.Models.RequestModels;
using KYC.Models.ResponseModels;

namespace KYC.Interface
{
    public interface IKycService
    {
        Task<KycVerificationResposne> VerifyKycAsync(KYCVerficationRequest request, string uid);
        Task<KycVerificationResposne> GetVerificationStatusAsync(int id);
        Task<List<KycVerificationResposne>> GetVerificationHistoryAsync(string uid);
    }
}
