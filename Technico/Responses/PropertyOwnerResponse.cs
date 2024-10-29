using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.Responses;

public class PropertyOwnerResponse
{
    public int Status { get; set; } // 0 επιτυχία, 1 αποτυχία - αν και δεν το χρησιμοποιώ προστοπαρόν, το αφήνω για future expansion
    public string Message { get; set; } = string.Empty;
}
