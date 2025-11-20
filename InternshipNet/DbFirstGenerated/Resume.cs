using System;
using System.Collections.Generic;

namespace InternshipNet.DbFirstGenerated;

public partial class Resume
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;
}
