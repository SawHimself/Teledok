using Entities;
using Persistence.Repository;

namespace Services.ValidationService.Concrete
{
    public static class ClientValidationService
    {
        public static bool IsValid(Client client)
        {
            if (client == null)
            {
                throw new ArgumentException(nameof(client), "Client cannot be null");
            }

            if (string.IsNullOrWhiteSpace(client.Name))
            {
                throw new ArgumentException("Client name cannot be empty or whitespace.");
            }
            if (client.Name.Length < 3 ||  client.Name.Length > 100)
            {
                throw new ArgumentException("Name must be between 3 and 100 characters.");
            }

            if (client.TIN.Length != 10 && client.TIN.Length != 12)
            {
                throw new ArgumentException("TIN must be either 10 or 12 characters long.");
            }
            else
            {
                if (!client.TIN.All(char.IsDigit))
                {
                    throw new ArgumentException("TIN must consist of numbers only");
                }
                if (client.Type == ClientType.LE && client.TIN.Length != 10)
                {
                    throw new ArgumentException("The TIN of a legal entity must consist of 10 Arabic digits.");
                }
                if (client.Type == ClientType.IE && client.TIN.Length != 12)
                {
                    throw new ArgumentException("The TIN of an individual must consist of 12 Arabic digits.");
                }
            }
            return true;
        }
    }
}
