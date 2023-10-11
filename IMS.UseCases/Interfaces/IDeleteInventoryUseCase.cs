using IMS.CoreBusiness;

namespace IMS.UseCases.Interfaces
{
    public interface IDeleteInventoryUseCase
    {
        Task ExecuteAsync(Inventory inventory);
    }
}