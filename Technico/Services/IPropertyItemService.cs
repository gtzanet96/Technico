using Technico.Models;
using Technico.Responses;

namespace Technico.Services
{
    public interface IPropertyItemService
    {
        PropertyCustomResponse AddCoOwnerToPropertyItem(int propertyItemId, string ownerVAT);
        PropertyCustomResponse CreatePropertyItem(PropertyItem item, string ownerVat);
        PropertyCustomResponse DeletePropertyItem(int itemId, bool softDelete = true);
        ImmutablePropertyItem? GetPropertyItemDetails(int itemId);
        PropertyCustomResponse RemoveCoOwnerFromPropertyItem(int propertyItemId, string ownerVAT);
        PropertyCustomResponse UpdatePropertyItem(int itemId, PropertyItem newItem);
    }
}