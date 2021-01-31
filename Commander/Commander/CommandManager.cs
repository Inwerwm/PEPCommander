using PEPCommander.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander
{
    public static class CommandManager
    {
        public static IReadOnlyList<ICommand> List => new ICommand[]
        {
            new UVCLocalInvertVertical(),
            new UVCLocalInvertHorizontal(),
            new CreateWrapperBone(),
        };
    }
}
