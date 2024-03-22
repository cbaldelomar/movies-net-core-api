using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Utilities;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class AuditableEntityAttribute : Attribute
{

}
