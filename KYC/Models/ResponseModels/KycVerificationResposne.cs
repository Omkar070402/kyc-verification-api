namespace KYC.Models.ResponseModels
{
    public class KycVerificationResposne
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string ? IdDocumentType { get; set; }
        public string ? AddressDocumentType { get; set; }
        public bool IdVerificationPass { get; set; }
        public bool AddressVerificationPass { get; set; }
        public bool NameMatch { get; set; }
        public string ? OverallStatus { get; set; }
        public string ? FailureReason { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
