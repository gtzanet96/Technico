using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Responses;

public record ImmutablePropertyOwnerSummary(
    int Id,
    string VAT,
    string FirstName,
    string LastName
);
