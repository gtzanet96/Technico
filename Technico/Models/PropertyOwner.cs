using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class PropertyOwner
{
    public int Id { get; set; }
    public string VAT { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int UserId { get; set; } // Foreign key to User table
    public User? User { get; set; } // Navigation property for accessing User info

    public List<PropertyItem> Properties { get; set; } = [];
    public List<Repair> Repairs { get; set; } = [];

}
