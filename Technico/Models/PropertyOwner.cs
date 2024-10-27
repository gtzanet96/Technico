using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class PropertyOwner
{
    public int Id { get; set; }
    public string VAT { get; set; } = string.Empty; // Must be unique
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // User-specific fields
    public string Email { get; set; } = string.Empty; // Used as Username
    public string Password { get; set; } = string.Empty;
    public string UserType { get; set; } = "Primary Owner"; // Default user type

    public List<PropertyItem> Properties { get; set; } = [];
    public List<Repair> Repairs { get; set; } = [];

}