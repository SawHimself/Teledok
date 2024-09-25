using Entities;
using Persistence.Repository;

namespace Services.ValidationService.Concrete
{
    public static class FounderValidationService
    {
        public static bool IsValid(Founder founder)
        {
            if (founder == null)
            {
                throw new ArgumentException(nameof(founder), "Founder cannot be null");
            }

            if (string.IsNullOrWhiteSpace(founder.FullName))
            {
                throw new ArgumentException("Client name cannot be empty or whitespace.");
            }

            if (founder.TIN.Length != 12)
            {
                throw new ArgumentException("For the founder - an individual, a TIN of 12 characters is required.");
            }
            return true;
        }
    }
}
