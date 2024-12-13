using System;
using System.Collections.Generic;

namespace DatabasUppgiftEntity.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? TeacherId { get; set; }

    public string GradeValue { get; set; } = null!;

    public DateOnly DateAssigned { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Employee? Teacher { get; set; }
}
