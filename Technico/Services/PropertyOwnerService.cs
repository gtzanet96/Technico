using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Repositories;
using Technico.Responses;

namespace Technico.Services;

public class PropertyOwnerService
{
    /* GDPR requirements are met in the following ways:
     - Data Minimization: Συλλέγουμε και επεξεργαζόμαστε μόνο τα απαραίτητα για την υπηρεσία δεδομένων.
       (π.χ δεν ρωτάμε αν ο χρήστης έχει και άλλα ακίνητα εκτός από τα καταχωρημένα, αν έχει παιδιά κτλ)
     - Right to Access: Οι χρήστες έχουν πρόσβαση ανά πάσα στιγμή στα αποθηκευμένα δεδομένα τους (display services)
     - Right to Rectification: Επιτρέπουμε στους χρήστες να διορθώσουν και να ανανεώσουν ανακριβή δεδομένα (update service)
     - Right to Erasure: Παρέχουμε επιλογή για μόνιμη διαγραφή των δεδομένων ενός χρήστη μετά από σχετική του αίτηση (delete service).
     - Data security: Χρησιμοποιώ immutable objects (records) στα display services και δεν κάνω καθόλου display το password στο display service 
        του owner details.
     */

    private TechnicoDbContext db;
    public PropertyOwnerService(TechnicoDbContext db) // dependency injection
    {
        this.db = db;
    }

    // 1. The property owner self-registration service
    public PropertyCustomResponse CreatePropertyOwner(PropertyOwner owner)
    {
        // Check if all required fields are filled
        if (!ValidationsHandler.IsValidOwner(owner))
        {
            return new PropertyCustomResponse
            {
                Status = 1, //Status is not used in current implementation, it is here just for good practice
                Message = "The property owner was not created. All fields must be filled to create a new property owner."
            };
        }
        // Check for unique VAT
        if (db.PropertyOwners.Any(o => o.VAT == owner.VAT))
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = "The property owner was not created. A property owner with this VAT already exists."
            };
        }
        // If validations pass, save the owner
        db.PropertyOwners.Add(owner);
        db.SaveChanges();

        return new PropertyCustomResponse
        {
            Status = 0,
            Message = $"Property owner with name {owner.FirstName} {owner.LastName} created."
        };
    }

    /* 2. The display service allows displaying the following that concern the current user: (But not editable!! -GDPR-)
    • Property Owner details (But not the password!! -GDPR-)
    • Property items details
    • Repairs details */
    public ImmutablePropertyOwner? GetPropertyOwnerDetails(int id)
    {
        return db.PropertyOwners
            .Where(owner => owner.Id == id)
            .Select(owner => new ImmutablePropertyOwner(
                owner.Id,
                owner.VAT,
                owner.FirstName,
                owner.LastName,
                owner.Address,
                owner.PhoneNumber,
                owner.Email,
                owner.UserType
            ))
            .FirstOrDefault();
    }

    public List<ImmutablePropertyItem> GetPropertyOwnerItems(int ownerId)
    {
        return db.PropertyItems
            .Where(item => item.PropertyOwners.Any(owner => owner.Id == ownerId))
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
                )).ToList() //showing co-owners too
            ))
            .ToList();
    }

    public List<ImmutableRepair> GetPropertyOwnerRepairs(int ownerId)
    {
        return db.PropertyItems
        .Where(item => item.PropertyOwners.Any(owner => owner.Id == ownerId))
        .SelectMany(item => item.Repairs)
        .Select(repair => new ImmutableRepair(
            repair.Id,
            repair.ScheduledDate,
            repair.Type,
            repair.RepairDescription,
            repair.Status,
            repair.Cost
        ))
        .ToList();
    }

    // 3. The service also includes the following options: Update, Delete.
    // Can update any field if owner id is specified
    public PropertyCustomResponse UpdatePropertyOwner(PropertyOwner owner) 
    {
        PropertyOwner? ownerdb = db.PropertyOwners.FirstOrDefault(c => c.Id == owner.Id);
        if (ownerdb == null)
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = "Property owner not found."
            };
        }

        // Checking for unique VAT if it is changed
        if (owner.VAT != ownerdb.VAT && db.PropertyOwners.Any(o => o.VAT == owner.VAT))
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = "Update failed. A property owner with this VAT already exists."
            };
        }

        //only update field when it has different value from db entry and not null
        ownerdb.VAT = (ownerdb.VAT != owner.VAT && !string.IsNullOrWhiteSpace(owner.VAT)) ? owner.VAT : ownerdb.VAT;
        ownerdb.FirstName = (ownerdb.FirstName != owner.FirstName && !string.IsNullOrWhiteSpace(owner.FirstName)) ? owner.FirstName : ownerdb.FirstName;
        ownerdb.LastName = (ownerdb.LastName != owner.LastName && !string.IsNullOrWhiteSpace(owner.LastName)) ? owner.LastName : ownerdb.LastName;
        ownerdb.Email = (ownerdb.Email != owner.Email && !string.IsNullOrWhiteSpace(owner.Email)) ? owner.Email : ownerdb.Email;
        ownerdb.Address = (ownerdb.Address != owner.Address && !string.IsNullOrWhiteSpace(owner.Address)) ? owner.Address : ownerdb.Address;
        ownerdb.PhoneNumber = (ownerdb.PhoneNumber != owner.PhoneNumber && !string.IsNullOrWhiteSpace(owner.PhoneNumber)) ? owner.PhoneNumber : ownerdb.PhoneNumber;

        db.SaveChanges();

        return new PropertyCustomResponse
        {
            Status = 0,
            Message = $"Property owner with name: {ownerdb.FirstName} {ownerdb.LastName} updated successfully."
        };
    }

    // Deletes an owner based on his id - Εφόσον ο Property Owner αναφέρει τα GDPR, χρησιμοποιώ μόνο hard delete
    public PropertyCustomResponse DeletePropertyOwner(int id)
    {
        PropertyOwner? ownerdb = db.PropertyOwners
                    .Include(o => o.PropertyItems)
                    .ThenInclude(pi => pi.Repairs)
                    .FirstOrDefault(o => o.Id == id);

        if (ownerdb == null)
        {
            return new PropertyCustomResponse
            {
                Status = 1,
                Message = "Property owner was not found."
            };
        }

        // Soft Delete στα repairs του owner για να μην έχουμε απώλειες στο repairs history όταν διαγράφεται ο χρήστης
        foreach (var propertyItem in ownerdb.PropertyItems)
        {
            foreach (var repair in propertyItem.Repairs)
            {
                repair.IsDeactivated = true;
            }
        }

        // Hard delete στα PropertyItems του Owner
        foreach (var propertyItem in ownerdb.PropertyItems)
        {
            propertyItem.PropertyOwners.Remove(ownerdb); // Διαγραφή του owner από τη λίστα των co-owners
            db.PropertyItems.Remove(propertyItem); // Hard delete του ίδιου του Property Item απ τον αντίστοιχο πίνακα
        }

        db.PropertyOwners.Remove(ownerdb); // Hard delete τον PropertyOwner
        db.SaveChanges();

        return new PropertyCustomResponse { 
            Status = 0,
            Message = $"Property owner {ownerdb.FirstName} {ownerdb.LastName} and his associated property items were deleted successfully."
        };
    }
}
