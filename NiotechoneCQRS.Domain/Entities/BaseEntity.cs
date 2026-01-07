using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Domain.Entities;

public class BaseEntity
{
    public Nullable<int> CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<int> ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
}
