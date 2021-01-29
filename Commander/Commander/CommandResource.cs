using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander
{
    public enum CommandResource
    {
        Vertex
    }

    public static class CommandResourceInfo
    {
        public static Dictionary<CommandResource, string> Descriptions => new Dictionary<CommandResource, string>()
        {
            { CommandResource.Vertex, "PMXビューから頂点を選択する必要があります。"}
        };
    }
}
