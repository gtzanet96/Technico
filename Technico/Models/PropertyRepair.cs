﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class PropertyRepair
{
    public int Id { get; set; } // PK
    public DateTime ScheduledDate { get; set; }
    public string Type { get; set; } = string.Empty; // type of repair: (Painting, Insulation, Frames, plumbing, electrical work)
    public string RepairDescription { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; // status: (Pending, In Progress, Complete)
    [Precision(8, 2)] public decimal Cost { get; set; }
    public bool IsDeactivated { get; set; } = false; // default to active

    public int? PropertyItemId { get; set; } // αντί για το property address που ανέφερε το business στο repair entity, για λόγους ίδιους με τους παραπάνω
    public PropertyItem? PropertyItem { get; set; } // navigation property, δεν θα εμφανιστεί σαν ξεχωριστή στήλη
}