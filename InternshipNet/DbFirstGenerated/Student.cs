using System;
using System.Collections.Generic;

namespace InternshipNet.DbFirstGenerated;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Resume? Resume { get; set; }

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
