using Technico.Models;
using Technico.Responses;

namespace Technico.Services
{
    public interface IPropertyOwnerService
    {
        PropertyCustomResponse CreatePropertyOwner(PropertyOwner owner);
        PropertyCustomResponse DeletePropertyOwner(int id);
        ImmutablePropertyOwner? GetPropertyOwnerDetails(int ownerId);
        List<ImmutablePropertyItem> GetPropertyOwnerItems(int ownerId);
        List<ImmutableRepair> GetPropertyOwnerRepairs(int ownerId);
        PropertyCustomResponse UpdatePropertyOwner(int ownerId, PropertyOwner updatedOwner);
    }
}