using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Responses;

public record ImmutableRepair(
    int Id,
    DateTime ScheduledDate,
    string Type,
    string Description,
    string Status,
    decimal Cost
);
