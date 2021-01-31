using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Requires
{
    public enum CommandResource
    {
        Pmx,
        Vertex,
        Bone
    }

    public static class CommandResourceInfo
    {
        public static Dictionary<CommandResource, string> Descriptions => new Dictionary<CommandResource, string>()
        {
            { CommandResource.Pmx, "モデルデータへの変更を伴います。" },
            { CommandResource.Vertex, "PMXビューから頂点を選択する必要があります。"},
            { CommandResource.Bone, "フォームからボーンを選択する必要があります。" },
        };
    }
}
