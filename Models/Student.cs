using System;
using System.Collections.Generic;

namespace DatabasUppgiftEntity.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PersonalNumber { get; set; } = null!;

    public string? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
