using Entities;
using Persistence.Repository;
using Services.LoggerService;
using Services.ValidationService.Concrete;
using System.Diagnostics.Metrics;
using System.Net;

namespace UseCases.Concrete
{
    public class ClientLogic : IClientLogic
    {
        private readonly IClientRepository _repository;
        private readonly ILoggerManager _logger;
        public ClientLogic(IClientRepository repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public async Task<Client?> Create(Client newclient)
        {
            Client? saved = await _repository.GetByTINAsync(newclient.TIN);
            if (saved != null)
            {
                throw new ArgumentException("Client with the same TIN already exists");
            }
            if (ClientValidationService.IsValid(newclient))
            {
                if (saved != null)
                {
                    saved.CreationDate = DateTime.Now;
                }
                newclient.CreationDate = DateTime.Now;

                newclient.UpdateDate = DateTime.Now;
                saved = await _repository.CreateAsync(newclient);
            }
            _logger.LogInfo($"DataBase changed: Add new client - {newclient.Id} {newclient.Name}");
            return saved;
        }

        public async Task<Client?> Remove(string TIN)
        {
            Client? client = await _repository.GetByTINAsync(TIN);
            if (client == null)
            {
                throw new ArgumentException("Сlient not found");
            }
            else
            {
                await _repository.RemoveAsync(client.Id);
                _logger.LogWarn($"DataBase changed: client entity deleted {client.Id} {client.Name}");
                return client;
            }
        }

        public async Task<Client?> UpdateName(string TIN, string name)
        {
            Client? client = await _repository.GetByTINAsync(TIN);
            if (client == null)
            {
                throw new ArgumentException("Сlient not found");
            }
            else
            {
                client.Name = name;
                if (ClientValidationService.IsValid(client))
                {
                    client.UpdateDate = DateTime.Now;
                    await _repository.UpdateAsync(client);
                    _logger.LogInfo($"DataBase changed: client entity update {client.Id} {client.Name}");
                    return client;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Client?> UpdateTIN(string OldTIN, string NewTIN, ClientType NewType)
        {
            Client? client = await _repository.GetByTINAsync(OldTIN);
            if (client == null)
            {
                throw new ArgumentException("Сlient not found");
            }
            else
            {
                client.TIN = NewTIN;
                client.Type = NewType;
                if (ClientValidationService.IsValid(client))
                {
                    client.UpdateDate = DateTime.Now;
                    await _repository.UpdateAsync(client);
                    _logger.LogInfo($"DataBase changed: client entity update {client.Id} {client.Name}");
                    return client;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Client?> AddFounder(string TIN, Founder founder)
        {
            Client? client = await _repository.GetByTINAsync(TIN);
            if (client == null)
            {
                throw new ArgumentException("Сlient not found");
            }
            else
            {
                if (FounderValidationService.IsValid(founder))
                {
                    if (client.Type == ClientType.IE)
                    {
                        throw new ArgumentException("Only a legal entity can have founders");
                    }
                    founder.CreationDate = DateTime.Now;
                    founder.UpdateDate = DateTime.Now;

                    if (client.Founders == null)
                    {
                        client.Founders = new List<Founder>();
                    }

                    client.Founders.Add(founder);
                    await _repository.UpdateAsync(client);
                    _logger.LogInfo($"DataBase changed: client entity update {client.Id} {client.Name}");
                    _logger.LogInfo($"DataBase changed: Add new founder - {founder.Id} {founder.FullName}");
                    return client;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<Client?> DeleteFounder(string clientTIN, string founderTIN)
        {
            Client? client = await _repository.GetByTINAsync(clientTIN);
            if (client == null)
            {
                return null;
            }
            else
            {
                if (client.Founders == null)
                {
                    throw new ArgumentException("Fouder not found");
                }

                Founder? founder = client.Founders.FirstOrDefault(f => f.TIN == founderTIN);
                if (founder == null)
                {
                    throw new ArgumentException("Founder not found");
                }

                client.Founders.Remove(founder);
                _logger.LogInfo($"DataBase changed: client entity update {client.Id} {client.Name}");
                _logger.LogWarn($"DataBase changed: founder entity deleted {founder.Id} {founder.FullName}");
                await _repository.UpdateAsync(client);
                return client;
            }
        }

        public IQueryable<Client> GetClients(int position, int pageSize, string? name)
        {
            if (position < 0 || pageSize < 0)
            {
                throw new ArgumentException("Incorrect page position or size");
            }
            else
            {
                return _repository.SoftFilterPageAsync(position, pageSize, name);
            }
        }
    }
}
