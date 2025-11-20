using System;
using System.Collections.Generic;

namespace InternshipNet.DbFirstGenerated;

public partial class StudentApplication
{
    public int StudentId { get; set; }

    public int InternshipId { get; set; }

    public int Status { get; set; }

    public DateTime AppliedDate { get; set; }

    public virtual Internship Internship { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
