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

public class PropertyOwnerService : IPropertyOwnerService
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
            return new PropertyCustomResponse { Status = 1, Message = "Owner creation failed. All fields must be filled to create a new property owner." };
        // Check for unique VAT
        if (db.PropertyOwners.Any(o => o.VAT == owner.VAT))
            return new PropertyCustomResponse { Status = 1, Message = "Owner creation failed. A property owner with this VAT already exists." };
        // Check for unique email
        if (db.PropertyOwners.Any(o => o.Email == owner.Email))
            return new PropertyCustomResponse { Status = 1, Message = "Owner creation failed. A property owner with this Email address already exists." };
        // Check for valid email format
        if (!ValidationsHandler.IsValidEmail(owner.Email))
            return new PropertyCustomResponse { Status = 1, Message = "Owner creation failed. Email format is invalid." };
        // If validations pass, save the owner
        try
        {
            db.PropertyOwners.Add(owner);
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Property owner with name {owner.FirstName} {owner.LastName} was created." };
        }
        catch (Exception e)
        {
            return new PropertyCustomResponse { Status = 1, Message = $"Owner creation failed due to a database error: '{e.Message}'" };
        }
    }

    /* 2. The display service allows displaying the following that concern the current user: (But not editable!! -GDPR-)
    • Property Owner details (But not the password!! -GDPR-)
    • Property items details
    • Repairs details */
    public ImmutablePropertyOwner? GetPropertyOwnerDetails(int ownerId)
    {
        return db.PropertyOwners
            .Where(owner => owner.Id == ownerId)
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
            repair.RepairType,
            repair.RepairDescription,
            repair.RepairStatus,
            repair.Cost
        ))
        .ToList();
    }

    // 3. The service also includes the following options: Update, Delete.
    public PropertyCustomResponse UpdatePropertyOwner(int ownerId, PropertyOwner updatedOwner)
    {
        PropertyOwner? ownerdb = db.PropertyOwners.FirstOrDefault(o => o.Id == ownerId);
        if (ownerdb == null)
            return new PropertyCustomResponse { Status = 1, Message = "Property owner not found." };
        // Check for unique VAT if VAT is to be updated
        if (updatedOwner.VAT != ownerdb.VAT && db.PropertyOwners.Any(o => o.VAT == updatedOwner.VAT))
            return new PropertyCustomResponse { Status = 1, Message = "Update failed. A property owner with this VAT already exists." };
        // Check for unique email
        if (db.PropertyOwners.Any(o => o.Email == updatedOwner.Email))
            return new PropertyCustomResponse { Status = 1, Message = "Owner creation failed. A property owner with this Email address already exists." };
        // Check for valid email format
        if (!ValidationsHandler.IsValidEmail(updatedOwner.Email))
            return new PropertyCustomResponse { Status = 1, Message = "Owner creation failed. Email format is invalid." };

        // Only update a field when it has different value from db entry and is not null
        ownerdb.VAT = (ownerdb.VAT != updatedOwner.VAT && !string.IsNullOrWhiteSpace(updatedOwner.VAT)) ? updatedOwner.VAT : ownerdb.VAT;
        ownerdb.FirstName = (ownerdb.FirstName != updatedOwner.FirstName && !string.IsNullOrWhiteSpace(updatedOwner.FirstName)) ? updatedOwner.FirstName : ownerdb.FirstName;
        ownerdb.LastName = (ownerdb.LastName != updatedOwner.LastName && !string.IsNullOrWhiteSpace(updatedOwner.LastName)) ? updatedOwner.LastName : ownerdb.LastName;
        ownerdb.Email = (ownerdb.Email != updatedOwner.Email && !string.IsNullOrWhiteSpace(updatedOwner.Email)) ? updatedOwner.Email : ownerdb.Email;
        ownerdb.Address = (ownerdb.Address != updatedOwner.Address && !string.IsNullOrWhiteSpace(updatedOwner.Address)) ? updatedOwner.Address : ownerdb.Address;
        ownerdb.PhoneNumber = (ownerdb.PhoneNumber != updatedOwner.PhoneNumber && !string.IsNullOrWhiteSpace(updatedOwner.PhoneNumber)) ? updatedOwner.PhoneNumber : ownerdb.PhoneNumber;

        try
        {
            db.SaveChanges();
            return new PropertyCustomResponse { Status = 0, Message = $"Property owner with name: {ownerdb.FirstName} {ownerdb.LastName} updated successfully." };
        }
        catch (Exception e)
        {
            return new PropertyCustomResponse { Status = 1, Message = $"Owner update failed due to a database error: '{e.Message}'" };
        }
    }

    // Deletes an owner based on his id - Εφόσον στο Property Owner Entity αναφέρονται τα GDPR, χρησιμοποιώ μόνο hard delete
    public PropertyCustomResponse DeletePropertyOwner(int id)
    {
        PropertyOwner? ownerdb = db.PropertyOwners
                    .Include(o => o.PropertyItems)
                    .ThenInclude(pi => pi.Repairs)
                    .FirstOrDefault(o => o.Id == id);

        if (ownerdb == null) return new PropertyCustomResponse { Status = 1, Message = "Property owner was not found." };

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

        return new PropertyCustomResponse { Status = 0, Message = $"Property owner {ownerdb.FirstName} {ownerdb.LastName} and his associated property items were deleted successfully." };
    }
}
