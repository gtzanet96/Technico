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
    public PropertyType PropertyType { get; set; } // default το Uncategorized

    public int PropertyOwnerId { get; set; } // αντί για owner vat number
    public PropertyOwner PropertyOwner { get; set; } // navigation property

    public List<Repair> Repairs { get; set; } = [];
}
