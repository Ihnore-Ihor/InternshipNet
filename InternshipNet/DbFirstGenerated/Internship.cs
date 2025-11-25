using System;
using System.Collections.Generic;

namespace InternshipNet.DbFirstGenerated;

public partial class Internship
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CompanyId { get; set; }

    public bool IsRemote { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
