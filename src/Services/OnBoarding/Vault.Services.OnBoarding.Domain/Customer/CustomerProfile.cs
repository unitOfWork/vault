using Vault.Services.OnBoarding.Domain.Common;
using Vault.Services.OnBoarding.Domain.Customer.Enums;

namespace Vault.Services.OnBoarding.Domain.Customer
{
    /// <summary>Customer profile : Register(), MarkPending(), MarkVerified(), MarkRejected()</summary>
    public sealed class CustomerProfile : Entity
    {
        private CustomerProfile() { }

        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public KycState KycStatus { get; private set; }
        public DateTimeOffset? VerifiedAt { get; private set; }

        /// <summary>Creates a new, unverified customer profile.</summary>
        public static CustomerProfile Create(Guid userId, string firstName, string lastName, DateOnly dateOfBirth)
        {
            return new CustomerProfile
            {
                UserId = userId,
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                DateOfBirth = dateOfBirth,
                KycStatus = KycState.Unverified
            };
        }

        /// <summary>Submits the profile for KYC review.</summary>
        public void MarkPending()
        {
            KycStatus = KycState.Pending;
            VerifiedAt = null;
        }

        /// <summary>Records a successful KYC verification.</summary>
        public void MarkVerified(DateTimeOffset verifiedAt)
        {
            KycStatus = KycState.Verified;
            VerifiedAt = verifiedAt;
        }

        /// <summary>Records a failed KYC verification.</summary>
        public void MarkRejected()
        {
            KycStatus = KycState.Rejected;
            VerifiedAt = null;
        }
    }
}
