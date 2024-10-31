using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public static bool IsValidEmail(string email)
    {
        // Regular expression for validating email format
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    public static bool isValidItem(PropertyItem item)
    {
        if (item.PropertyIdentificationNumber <= 0 ||
            string.IsNullOrWhiteSpace(item.PropertyAddress) ||
            item.YearOfConstruction <= 0)
            return false;
        return true;
    }

    public static bool isValidRepair(PropertyRepair repair)
    {
        if (repair.ScheduledDate == default ||
            repair.RepairType == RepairType.Uncategorized ||
            string.IsNullOrWhiteSpace(repair.RepairDescription) ||
            repair.Cost <= 0)
            return false;
        return true;
    }

}


