using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class PropertyItem
{
    public int Id { get; set; }  // PK
    public string PropertyIdentificationNumber { get; set; } = string.Empty; // MUST BE UNIQUE
    public string PropertyAddress { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public PropertyType PropertyType { get; set; } // default to Uncategorized
    public bool IsDeactivated { get; set; } = false; // default to active

    public List<PropertyOwner> PropertyOwners { get; set; } = []; //ένα property item μπορεί να έχει πολλούς property owners
    public List<Repair> Repairs { get; set; } = []; //ένα property item μπορεί να έχει πολλά repairs
}
