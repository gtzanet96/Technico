using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.Responses;

public record ImmutablePropertyItem(
    int Id,
    long PropertyIdentificationNumber,
    string PropertyAddress,
    int YearOfConstruction,
    PropertyType PropertyType,
    List<ImmutablePropertyOwnerSummary> CoOwners
);
