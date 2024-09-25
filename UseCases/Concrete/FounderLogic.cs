using Entities;
using Persistence.Repository;
using Services.LoggerService;
using Services.ValidationService.Concrete;

namespace UseCases.Concrete
{
    public class FounderLogic : IFounderLogic
    {
        private readonly IFounderRepository _repository;
        private readonly ILoggerManager _logger;

        public FounderLogic(IFounderRepository repository, ILoggerManager logger) 
        { 
            _repository = repository;
            _logger = logger;
        }

        public async Task<Founder?> UpdateFullName(string TIN, string fullName)
        {
            Founder? founder = await _repository.GetByTINAsync(TIN);
            if (founder == null) 
            { 
                return null;
            }
            else
            {
                founder.FullName = fullName;
                if(FounderValidationService.IsValid(founder)) 
                {
                    founder.UpdateDate = DateTime.Now;
                    await _repository.UpdateAsync(founder);
                    _logger.LogInfo($"DataBase changed: founder entity update {founder.Id} {founder.FullName}");
                    return founder;
                }
                else
                {
                    throw new ArgumentException("Incorrect fullname");
                }
            }
        }

        public async Task<Founder?> UpdateTIN(string TIN, string tin)
        {
            Founder? founder = await _repository.GetByTINAsync(TIN);
            if (founder == null)
            {
                return null;
            }
            else
            {
                founder.TIN = TIN;
                if (FounderValidationService.IsValid(founder))
                {
                    founder.UpdateDate = DateTime.Now;
                    await _repository.UpdateAsync(founder);
                    _logger.LogInfo($"DataBase changed: founder entity update {founder.Id} {founder.FullName}");
                    return founder;
                }
                else
                {
                    throw new ArgumentException("Incorrect TIN");
                }
            }
        }
    }
}
