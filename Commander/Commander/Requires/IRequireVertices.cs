using PEPCommander.Commands;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Requires
{
    interface IRequireVertices : ICommand
    {
        void Supply(IEnumerable<IPXVertex> vertices);
    }
}
