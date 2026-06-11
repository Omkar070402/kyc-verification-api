using KYC.Data;
using KYC.Interface;
using KYC.Models.RequestModels;
using KYC.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace KYC.Service
{
    public class KycService : IKycService
    {

        private readonly AppDbContext _context;

        public KycService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<KycVerificationResposne> VerifyKycAsync(KYCVerficationRequest request, string uid)
        {
            var idFields = SimulateOcrExtraction (request.IdDocumentType);

            var addressFields = SimulateOcrExtraction(request.AddressDocumentType);

            var idPassed = ValidateIdDocument(idFields);

            var addressPassed = ValidateAddressDocument(addressFields);

            var nameMatched = idFields["name"] == addressFields["name"];

            // Step 6 - Determine overall status
            var overallStatus = idPassed && addressPassed && nameMatched ? "PASS" : "FAIL";


            var result = new KycVerificationResposne
            {
                Uid = int.Parse(uid),
                IdDocumentType = request.IdDocumentType,
                AddressDocumentType = request.AddressDocumentType,
                IdVerificationPass = idPassed,
                AddressVerificationPass = addressPassed,
                NameMatch = nameMatched,
                OverallStatus = overallStatus,
                FailureReason = overallStatus == "FAIL" ? GetFailureReason(idPassed, addressPassed, nameMatched) : null,
                CreatedAt = DateTime.UtcNow
            };

            // Step 8 - Save to DB
            await _context.KycVerification.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;

        }

        public Dictionary<string , string> SimulateOcrExtraction(string documentType)
        {

            return new Dictionary<string, string>
            {

                { "name", "Omkar Seshadri" },
                { "documentNumber", "ABC1234567" },
                { "dateOfBirth", "1999-04-07" },
                { "expiryDate", "2029-04-07" },
                { "address", "123 Main Street" },
                { "city", "Pune" },
                { "pincode", "411001" }

            };

        }

        private bool ValidateIdDocument(Dictionary<string, string> fields)
        {
            return !string.IsNullOrEmpty(fields["name"]) &&
                   !string.IsNullOrEmpty(fields["documentNumber"]) &&
                   DateTime.Parse(fields["expiryDate"]) > DateTime.UtcNow;
        }

        private bool ValidateAddressDocument(Dictionary<string, string> fields)
        {
            return !string.IsNullOrEmpty(fields["name"]) &&
                   !string.IsNullOrEmpty(fields["address"]) &&
                   !string.IsNullOrEmpty(fields["pincode"]);
        }


        private string GetFailureReason(bool idPassed, bool addressPassed, bool nameMatched)
        {
            if (!idPassed) return "ID document validation failed";
            if (!addressPassed) return "Address document validation failed";
            if (!nameMatched) return "Name mismatch between documents";
            return "Unknown failure";
        }


        public async Task<KycVerificationResposne> GetVerificationStatusAsync(int id)
        {
            var result = await _context.KycVerification
                .FirstOrDefaultAsync(r => r.Id == id);

            if (result == null)
                throw new Exception("Verification not found");

            return result;
        }

        public async Task<List<KycVerificationResposne>> GetVerificationHistoryAsync(string userId)
        {
            return await _context.KycVerification
                .Where(r => r.Uid == int.Parse(userId))
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }




    }
    }



