using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Repositories;
using Technico.Responses;

namespace Technico.Services;

public class PropertyRepairService : IPropertyRepairService
{
    private readonly TechnicoDbContext db;

    public PropertyRepairService(TechnicoDbContext db) // Dependency Injection
    {
        this.db = db;
    }

    // Create
    public PropertyCustomResponse CreateRepair(PropertyRepair repair, int propertyItemId)
    {
        // check if the property item exists
        var propertyItem = db.PropertyItems.Include(pi => pi.PropertyOwners).FirstOrDefault(pi => pi.Id == propertyItemId);
        if (propertyItem == null)
            return new PropertyCustomResponse { Status = 1, Message = $"Repair creation failed. Property item with id {propertyItemId} was not found." };

        // Validate required fields
        if (!ValidationsHandler.isValidRepair(repair))
            return new PropertyCustomResponse { Status = 1, Message = $"Creation of repair for property with id {propertyItemId} failed. All Repair fields must be filled." };

        // Set propertyitemid field for the repair entry
        repair.PropertyItemId = propertyItemId;

        try
        {
            db.PropertyRepairs.Add(repair);
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Repair for property with id {propertyItemId} was created successfully." };
        }
        catch (Exception e)
        {
            return new PropertyCustomResponse { Status = 1, Message = $"Repair creation for property with id {propertyItemId} failed due to a database error: '{e.Message}'" };
        }
    }
    // Update
    public PropertyCustomResponse UpdateRepair(int repairId, PropertyRepair updatedRepair)
    {
        // Finding requested repair and then checking if it exists
        var repairDb = db.PropertyRepairs.FirstOrDefault(r => r.Id == repairId);

        if (repairDb == null)
            return new PropertyCustomResponse { Status = 1, Message = $"Repair with id {repairId} was not found." };

        // Updating fields if validations pass - only update field when it has different value from db entry and not null
        repairDb.ScheduledDate = updatedRepair.ScheduledDate != default ? updatedRepair.ScheduledDate : repairDb.ScheduledDate;
        repairDb.RepairType = (updatedRepair.RepairType != RepairType.Uncategorized && updatedRepair.RepairType != repairDb.RepairType) ? updatedRepair.RepairType : repairDb.RepairType;
        repairDb.RepairDescription = !string.IsNullOrWhiteSpace(updatedRepair.RepairDescription) ? updatedRepair.RepairDescription : repairDb.RepairDescription;
        repairDb.RepairStatus = updatedRepair.RepairStatus != repairDb.RepairStatus ? updatedRepair.RepairStatus : repairDb.RepairStatus;
        repairDb.Cost = updatedRepair.Cost > 0 ? updatedRepair.Cost : repairDb.Cost;

        db.SaveChanges();

        return new PropertyCustomResponse { Status = 0, Message = $"Repair with id {repairDb.Id} was updated successfully." };
    }
    // Delete
    public PropertyCustomResponse DeleteRepair(int repairId, bool softDelete = true)
    {
        var repair = db.PropertyRepairs.FirstOrDefault(r => r.Id == repairId);

        if (repair == null)
            return new PropertyCustomResponse { Status = 1, Message = $"Repair with id {repairId} was not found." };

        if (softDelete)
        {
            repair.IsDeactivated = true;
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Repair with id {repair.Id} was deactivated successfully." };
        }
        else
        {
            db.PropertyRepairs.Remove(repair);
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Repair with id {repair.Id} was deleted successfully." };
        }
    }
    // Τρεις Search μέθοδοι με διαφορετικά κριτήρια
    public List<ImmutableRepair> SearchRepairsByType(RepairType repairType)
    {
        return db.PropertyRepairs
            .Where(r => r.RepairType == repairType)
            .Select(r => new ImmutableRepair(
                r.Id,
                r.ScheduledDate,
                r.RepairType,
                r.RepairDescription,
                r.RepairStatus,
                r.Cost
            ))
            .ToList();
    }
    public List<ImmutableRepair> SearchRepairsByStatus(RepairStatus repairStatus)
    {
        return db.PropertyRepairs
            .Where(r => r.RepairStatus == repairStatus)
            .Select(r => new ImmutableRepair(
                r.Id,
                r.ScheduledDate,
                r.RepairType,
                r.RepairDescription,
                r.RepairStatus,
                r.Cost
            ))
            .ToList();
    }
    public List<ImmutableRepair> SearchRepairsByMinCost(decimal minCost)
    {
        return db.PropertyRepairs
            .Where(r => r.Cost >= minCost)
            .Select(r => new ImmutableRepair(
                r.Id,
                r.ScheduledDate,
                r.RepairType,
                r.RepairDescription,
                r.RepairStatus,
                r.Cost
            ))
            .ToList();
    }

}
