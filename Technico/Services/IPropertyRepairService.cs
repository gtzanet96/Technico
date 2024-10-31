using Technico.Models;
using Technico.Responses;

namespace Technico.Services
{
    public interface IPropertyRepairService
    {
        PropertyCustomResponse CreateRepair(PropertyRepair repair, int propertyItemId);
        PropertyCustomResponse DeleteRepair(int repairId, bool softDelete = true);
        List<ImmutableRepair> SearchRepairsByMinCost(decimal minCost);
        List<ImmutableRepair> SearchRepairsByStatus(RepairStatus repairStatus);
        List<ImmutableRepair> SearchRepairsByType(RepairType repairType);
        PropertyCustomResponse UpdateRepair(int repairId, PropertyRepair updatedRepair);
    }
}