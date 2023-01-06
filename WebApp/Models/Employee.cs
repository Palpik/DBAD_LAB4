using System;
using System.Collections.Generic;

namespace WebApp;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set;} = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
