using PEPCommander.Commands;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Requires
{
    interface IRequireBones
    {
        void Supply(IEnumerable<IPXBone> Bones);
    }
}
