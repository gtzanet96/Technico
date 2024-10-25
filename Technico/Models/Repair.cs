using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class Repair
{
    public int Id { get; set; } // PK
    public DateTime ScheduledDate { get; set; }
    public string Type { get; set; } = string.Empty; // type of repair: (Painting, Insulation, Frames, plumbing, electrical work)
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty; // repair address
    public string Status { get; set; } = "Pending"; // status: (Pending, In Progress, Complete)
    [Precision(8, 2)] public decimal Cost { get; set; }

    public int PropertyOwnerId { get; set; } // αντί για το owner vat ώστε σε πιθανή μελλοντική αλλαγή να μην αλλάζει σε δύο μέρη
    public PropertyOwner? PropertyOwner { get; set; } // navigation property, δεν θα εμφανιστεί σαν ξεχωριστή στήλη

    public int PropertyItemId { get; set; } // αντί για το property address που ανέφερε το business στο repair entity, για λόγους ίδιους με τους παραπάνω
    public PropertyItem? PropertyItem { get; set; } // navigation property, δεν θα εμφανιστεί σαν ξεχωριστή στήλη
}
