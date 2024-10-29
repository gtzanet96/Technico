using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.Services;

public static class ValidationsHandler
{
    public static bool IsValidOwner(PropertyOwner owner)
    {
        if(string.IsNullOrWhiteSpace(owner.VAT) ||
            string.IsNullOrWhiteSpace(owner.FirstName) ||
            string.IsNullOrWhiteSpace(owner.LastName) ||
            string.IsNullOrWhiteSpace(owner.Address) ||
            string.IsNullOrWhiteSpace(owner.PhoneNumber) ||
            string.IsNullOrWhiteSpace(owner.Email) ||
            string.IsNullOrWhiteSpace(owner.Password))
            return false;
        return true;
    }
}
