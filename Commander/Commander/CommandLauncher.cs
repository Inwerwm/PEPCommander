using PEPCommander.Commands;
using PEPCommander.Requires;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PEPCommander
{
    class CommandLauncher
    {
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; }

        public CommandLauncher(IPERunArgs args)
        {
            Args = args;
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();
        }

        public void Launch(IEnumerable<ICommand> commands)
        {
            try
            {
                var requires = commands.SelectMany(cmd => cmd.RequireResources).Distinct();

                foreach (var cmd in commands)
                {
                    SatisfyRequest(cmd);
                    cmd.Do();
                }

                PEPExtensions.Utility.Update(Args.Host.Connector, Pmx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SatisfyRequest(ICommand command)
        {
            foreach (var rsc in command.RequireResources)
            {
                switch (rsc)
                {
                    case CommandResource.Vertex:
                        int[] selectedVertexIndices = Args.Host.Connector.View.PmxView.GetSelectedVertexIndices();
                        if (selectedVertexIndices.Length < 1)
                            throw new InvalidOperationException("頂点が選択されている必要があります。");

                        var cmdV = command as IRequireVertices;
                        cmdV.Supply(selectedVertexIndices.Select(i => Pmx.Vertex[i]));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
