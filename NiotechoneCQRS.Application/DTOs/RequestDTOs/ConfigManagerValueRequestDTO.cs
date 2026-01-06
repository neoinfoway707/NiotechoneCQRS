using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.DTOs.RequestDTOs;

public class ConfigManagerValueRequestDTO
{
    public int ConfigManagerValueId { get; set; }
    public int ConfigManagerKeyId { get; set; }
    public long CompanyId { get; set; }
    public string Value { get; set; }
    public string Remarks { get; set; }
}
