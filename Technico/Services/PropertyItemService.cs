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

public class PropertyItemService
{
    private TechnicoDbContext db;
    public PropertyItemService(TechnicoDbContext db) // dependency injection
    {
        this.db = db;
    }
    // 2. Create
    public PropertyCustomResponse CreatePropertyItem(PropertyItem item, string ownerVat)
    {
        // Check if the provided VAT is valid (exists in db owners)
        var owner = db.PropertyOwners.FirstOrDefault(o => o.VAT == ownerVat);
        if (owner == null)
            return new PropertyCustomResponse { Status = 1, Message = "Property item creation failed. No property owner found with the provided VAT." };
        
        // Add owner to co-owners list of the property item
        item.PropertyOwners.Add(owner);
        
        // Check for empty fields - we want everything filled
        if (!ValidationsHandler.isValidItem(item))
            return new PropertyCustomResponse { Status = 1, Message = "The property owner was not created. All fields must be filled to create a new property owner." };
        
        // Check for unique PropertyIdentificationNumber(E9)
        if (db.PropertyItems.Any(i => i.PropertyIdentificationNumber == item.PropertyIdentificationNumber))
            return new PropertyCustomResponse { Status = 1, Message = "The property item was not created. A property item with this PropertyIdentificationNumber already exists." };

        // If all validations pass, save item to db
        try
        {
            db.PropertyItems.Add(item);
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Property item with PropertyIdentificationNumber {item.PropertyIdentificationNumber} was created successfully." };
        }
        catch (Exception e)
        {
            return new PropertyCustomResponse { Status = 1, Message = $"Property item creation with with PropertyIdentificationNumber {item.PropertyIdentificationNumber} failed due to a database error: '{e.Message}'" };
        }
   }

    // 1. Display all the details of the property
    public ImmutablePropertyItem? GetPropertyItemDetails(int itemId)
    {
        var propertyItem = db.PropertyItems
            .Where(item => item.Id == itemId)
            .Select(item => new ImmutablePropertyItem(
                item.Id,
                item.PropertyIdentificationNumber,
                item.PropertyAddress,
                item.YearOfConstruction,
                item.PropertyType,
                item.PropertyOwners.Select(owner => new ImmutablePropertyOwnerSummary(
                    owner.Id,
                    owner.VAT,
                    owner.FirstName,
                    owner.LastName
                )).ToList() // λίστα των βασικών στοιχείων των co-owners
            ))
            .FirstOrDefault();
        return propertyItem;
    }

    // 3. Update (nice to have): All the details of the property can be edited
    public PropertyCustomResponse UpdatePropertyItem(int itemId, PropertyItem newItem)
    {
        var itemdb = db.PropertyItems.FirstOrDefault(i => i.Id == itemId);
        if (itemdb == null)
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = "Update failed. Property item not found."
            };
        }

        // Check for unique PropertyIdentificationNumber, if changed at all
        if (newItem.PropertyIdentificationNumber != itemdb.PropertyIdentificationNumber &&
            db.PropertyItems.Any(i => i.PropertyIdentificationNumber == newItem.PropertyIdentificationNumber))
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = "Update failed. A property with this identification number already exists."
            };
        }

        // Update fields if validations pass - only update field when it has different value from db entry and not null
        itemdb.PropertyIdentificationNumber = (itemdb.PropertyIdentificationNumber != newItem.PropertyIdentificationNumber && newItem.PropertyIdentificationNumber > 0) ? newItem.PropertyIdentificationNumber : itemdb.PropertyIdentificationNumber;
        itemdb.PropertyAddress = (itemdb.PropertyAddress != newItem.PropertyAddress && !string.IsNullOrWhiteSpace(newItem.PropertyAddress)) ? newItem.PropertyAddress : itemdb.PropertyAddress;
        itemdb.YearOfConstruction = (itemdb.YearOfConstruction != newItem.YearOfConstruction && newItem.YearOfConstruction > 0) ? newItem.YearOfConstruction : itemdb.YearOfConstruction;
        itemdb.PropertyType = newItem.PropertyType; //enum

        db.SaveChanges();

        return new PropertyCustomResponse
        {
            Status = 0,
            Message = $"Property item with PropertyIdentificationNumber {itemdb.PropertyIdentificationNumber} updated successfully."
        };
    }

    public PropertyCustomResponse AddCoOwnerToPropertyItem(int propertyItemId, string ownerVAT)
    {
        var item = db.PropertyItems.Include(pi => pi.PropertyOwners).FirstOrDefault(pi => pi.Id == propertyItemId);
        var owner = db.PropertyOwners.FirstOrDefault(o => o.VAT == ownerVAT);

        if (item == null || owner == null)
            return new PropertyCustomResponse { Status = 1, Message = "Property item or owner not found." };

        if (item.PropertyOwners.Any(o => o.Id == owner.Id))
            return new PropertyCustomResponse { Status = 1, Message = $"Owner {owner.FirstName} {owner.LastName} is already a co-owner of the property." };

        item.PropertyOwners.Add(owner);
        db.SaveChanges();

        return new PropertyCustomResponse { Status = 0, Message = $"Owner {owner.FirstName} {owner.LastName} was added as a co-owner." };
    }

    public PropertyCustomResponse RemoveCoOwnerFromPropertyItem(int propertyItemId, string ownerVAT)
    {
        var item = db.PropertyItems.Include(pi => pi.PropertyOwners).FirstOrDefault(pi => pi.Id == propertyItemId);
        var owner = db.PropertyOwners.FirstOrDefault(o => o.VAT == ownerVAT);

        if (item == null || owner == null)
            return new PropertyCustomResponse
            { Status = 1, Message = "Property item or owner not found." };

        if (!item.PropertyOwners.Any(o => o.Id == owner.Id))
            return new PropertyCustomResponse { Status = 1, Message = $"This owner is not a co-owner of the property with number {item.PropertyIdentificationNumber}." };

        item.PropertyOwners.Remove(owner);
        db.SaveChanges();

        return new PropertyCustomResponse { Status = 0,  Message = $"Owner {owner.FirstName} {owner.LastName} was removed from co-owners of the property with number {item.PropertyIdentificationNumber}." };
    }

    public PropertyCustomResponse DeletePropertyItem(int itemId, bool softDelete = true) // Set softDelete to false for Hard delete
    {
        // Get property item and related PropertyOwners and Repairs
        var propertyItem = db.PropertyItems
            .Include(pi => pi.PropertyOwners)
            .Include(pi => pi.Repairs)
            .FirstOrDefault(pi => pi.Id == itemId);

        if (propertyItem == null)
            return new PropertyCustomResponse { Status = 1, Message = "Property item was not found." };

        if (softDelete)
        {
            propertyItem.IsDeactivated = true;
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Property item with Property Identification Number {propertyItem.PropertyIdentificationNumber} was deactivated." };
        }
        else
        {
            // First delete associations with PropertyOwners in the join table
            propertyItem.PropertyOwners.Clear();

            // Then we do NOT delete repairs related to the property item, we only remove the item's relation to them and we keep them for history purposes
            foreach (var repair in propertyItem.Repairs.ToList())
            {
                repair.PropertyItemId = null;
            }

            // Finally, hard delete the property item itself with no further implications
            db.PropertyItems.Remove(propertyItem);
            db.SaveChanges();

            return new PropertyCustomResponse { Status = 0, Message = $"Property item with ID {propertyItem.PropertyIdentificationNumber} was permanently deleted." };
        }
    }
}
