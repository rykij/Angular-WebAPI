using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public interface IResult
    {
        int Trial { get; set; }
        int Timestep { get; set; }
    }
}
