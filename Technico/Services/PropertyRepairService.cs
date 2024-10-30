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

public class PropertyRepairService
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
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = $"Repair creation failed. Property item with id {propertyItemId} was not found."
            };
        }

        // Validate required fields
        if (repair.ScheduledDate == default ||
            string.IsNullOrWhiteSpace(repair.Type) ||
            string.IsNullOrWhiteSpace(repair.RepairDescription) ||
            string.IsNullOrWhiteSpace(repair.Status) ||
            repair.Cost <= 0)
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = $"Creation of repair for property with id {propertyItemId} failed. All Repair fields must be filled."
            };
        }

        // Set propertyitemid field for the repair entry
        repair.PropertyItemId = propertyItemId;

        db.PropertyRepairs.Add(repair);
        db.SaveChanges();

        return new PropertyCustomResponse
        {
            Status = 0,
            Message = $"Repair for property with id {propertyItemId} was created successfully."
        };
    }

    public PropertyCustomResponse UpdateRepair(PropertyRepair updatedRepair)
    {
        // Finding requested repair and then checking if it exists
        var repairDb = db.PropertyRepairs.FirstOrDefault(r => r.Id == updatedRepair.Id);

        if (repairDb == null)
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = $"Repair with id {updatedRepair.Id} was not found."
            };
        }

        // Updating fields if validations pass - only update field when it has different value from db entry and not null
        repairDb.ScheduledDate = updatedRepair.ScheduledDate != default ? updatedRepair.ScheduledDate : repairDb.ScheduledDate;
        repairDb.Type = !string.IsNullOrWhiteSpace(updatedRepair.Type) ? updatedRepair.Type : repairDb.Type;
        repairDb.RepairDescription = !string.IsNullOrWhiteSpace(updatedRepair.RepairDescription) ? updatedRepair.RepairDescription : repairDb.RepairDescription;
        repairDb.Status = !string.IsNullOrWhiteSpace(updatedRepair.Status) ? updatedRepair.Status : repairDb.Status;
        repairDb.Cost = updatedRepair.Cost > 0 ? updatedRepair.Cost : repairDb.Cost;

        db.SaveChanges();

        return new PropertyCustomResponse
        {
            Status = 0,
            Message = $"Repair with id {repairDb.Id} was updated successfully."
        };
    }
    public PropertyCustomResponse DeleteRepair(int repairId, bool softDelete = true)
    {
        var repair = db.PropertyRepairs.FirstOrDefault(r => r.Id == repairId);

        if (repair == null)
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = $"Repair with id {repairId} was not found."
            };
        }

        if (softDelete)
        {
            repair.IsDeactivated = true;
            db.SaveChanges();
            return new PropertyCustomResponse
            {
                Status = 0,
                Message = $"Repair with id {repair.Id} was deactivated successfully."
            };
        }
        else
        {
            db.PropertyRepairs.Remove(repair);
            db.SaveChanges();
            return new PropertyCustomResponse
            {
                Status = 0,
                Message = $"Repair with id {repair.Id} was deleted successfully."
            };
        }
    }
}
