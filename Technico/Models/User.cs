﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty; // used as username
    public string Password { get; set; } = string.Empty;
    public string UserType { get; set; } = "Property Owner";
}
