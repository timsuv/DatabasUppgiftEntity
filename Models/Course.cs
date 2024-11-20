using System;
using System.Collections.Generic;

namespace DatabasUppgiftEntity.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Field { get; set; } = null!;
}
