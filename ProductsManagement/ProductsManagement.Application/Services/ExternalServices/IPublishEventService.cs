namespace ProductsManagement.Application.Services.ExternalServices
{
    public interface IPublishEventService
    {
        void PostEventProductCreated(int productId, string name, string description);
    }
}
