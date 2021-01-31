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
                    case CommandResource.Pmx:
                        (command as IRequirePmx).Supply(Pmx);
                        break;
                    case CommandResource.Vertex:
                        var selectedVertex = Args.Host.Connector.View.PmxView.GetSelectedVertexIndices().Select(i => Pmx.Vertex[i]).ToList();
                        if (!selectedVertex.Any())
                            throw new InvalidOperationException("ビュー画面で頂点が選択されている必要があります。");

                        (command as IRequireVertices).Supply(selectedVertex);
                        break;
                    case CommandResource.Bone:
                        var selectedBones = Args.Host.Connector.View.PmxView.GetSelectedBoneIndices().Select(i => Pmx.Bone[i]).ToList();
                        if(!selectedBones.Any())
                            throw new InvalidOperationException("ビュー画面でボーンが選択されている必要があります。");

                        (command as IRequireBones).Supply(selectedBones);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
