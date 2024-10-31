using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.Responses;

public record ImmutableRepair(
    int Id,
    DateTime ScheduledDate,
    RepairType RepairType,
    string RepairDescription,
    RepairStatus RepairStatus,
    decimal Cost
);
