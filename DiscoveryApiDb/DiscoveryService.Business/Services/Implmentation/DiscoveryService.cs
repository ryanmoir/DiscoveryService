namespace DiscoveryService.Business.Services.Implmentation
{
    using global::DiscoveryService.Business.Mappers.Discovery;
    using global::DiscoveryService.Business.Services.Interface;
    using global::DiscoveryService.EntityAcceess.Repository.Interface;
    using global::DiscoveryService.EntityAcceess.UnitOfWork.Interface;
    using System.Threading.Tasks;

    public class DiscoveryService : IDiscoveryService
    {
        public IDiscoveryRespository discoveryRepository { get; internal set; }

        public IUnitOfWork unitOfWork { get; internal set; }

        public DiscoveryService(IDiscoveryRespository discoveryRespository, IUnitOfWork unitOfWork)
        {
            this.discoveryRepository = discoveryRespository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<long> Add(Models.Dto.Controllers.Discovery.DiscoveryAddDto addDto)
        {
            var entity = new DiscoveryAddDtoMapper().ToEntity(addDto);
            await discoveryRepository.Insert(entity);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(long id)
        {
            await discoveryRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<Models.Dto.Controllers.Discovery.DiscoveryGetDto?> Get(long id)
        {
            var entity = await discoveryRepository.GetById(id);
            if (entity != null)
            {
                return new DiscoveryGetDtoMapper().ToDto(entity);
            }
            return null;
        }

        public async Task Update(Models.Dto.Controllers.Discovery.DiscoveryUpdateDto updateDto)
        {
            var entity = new DiscoveryUpdateDtoMapper().ToEntity(updateDto);
            discoveryRepository.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}