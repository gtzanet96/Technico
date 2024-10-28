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
    /*GDPR requirements are met in the following ways:
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

    //1. The self-registration service
    public PropertyOwner CreatePropertyOwner(PropertyOwner owner)
    { 
        db.PropertyOwners.Add(owner);
        db.SaveChanges();
        return owner;
    }

    /*2. The display service allows displaying the following that concern the current user: (But not editable!! -GDPR-)
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
                item.PropertyType
            ))
            .ToList();
    }

    public List<ImmutableRepair> GetPropertyOwnerRepairs(int ownerId)
    {
        return db.Repairs
            .Where(repair => repair.PropertyOwnerId == ownerId)
            .Select(repair => new ImmutableRepair(
            repair.Id,
            repair.ScheduledDate,
            repair.Type,
            repair.Description,
            repair.Status,
            repair.Cost
        ))
        .ToList();
    }

    //3. The service also includes the following options: Update, Delete.
    public PropertyOwner? UpdatePropertyOwner(PropertyOwner owner) //can update any field if db owner id is specified
    {
        PropertyOwner? ownerdb = db.PropertyOwners.FirstOrDefault(c => c.Id == owner.Id);
        if (ownerdb != null)
        {
            ownerdb.Email = owner.Email;
            db.SaveChanges();
        }
        return ownerdb;
    }

    public bool DeletePropertyOwner(int id) //deletes an owner based on his id
    {
        PropertyOwner? ownerdb = db.PropertyOwners.FirstOrDefault(c => c.Id == id);
        if (ownerdb != null)
        {
            db.PropertyOwners.Remove(ownerdb);
            db.SaveChanges();
            return true;
        }
        return false;
    }
}
