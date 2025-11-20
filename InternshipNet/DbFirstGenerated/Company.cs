using System;
using System.Collections.Generic;

namespace InternshipNet.DbFirstGenerated;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Industry { get; set; } = null!;

    public virtual ICollection<Internship> Internships { get; set; } = new List<Internship>();
}
