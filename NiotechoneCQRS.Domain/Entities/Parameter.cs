using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Domain.Entities;

public class Parameter
{
    public int ParameterId { get; set; }
    public string ParamName { get; set; }

    public virtual ICollection<ParameterValue> ParameterValues { get; set; }
}
