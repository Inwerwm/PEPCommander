using PEPCommander.Requires;
using PEPlugin.Pmx;
using PEPExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Commands
{
    class UVCLocalInvertVertical : ICommand, IRequireVertices
    {
        public int ID => 1;

        public string Name => "UV局所垂直反転";

        public string Description => "選択頂点のUVを選択範囲の局所領域基準で垂直反転する";

        IEnumerable<IPXVertex> TargetVertices { get; set; }

        public IEnumerable<CommandResource> RequireResources => new CommandResource[] { CommandResource.Vertex };

        public void Supply(IEnumerable<IPXVertex> vertices)
        {
            TargetVertices = vertices;
        }

        public void Do()
        {
            float yAverage = TargetVertices.Average(vtx => vtx.UV.Y);
            IEnumerable<(IPXVertex Vertex, float Difference)> diffs = TargetVertices.Select(vtx => (vtx, vtx.UV.Y - yAverage));

            foreach (var item in diffs)
            {
                item.Vertex.UV.Y -= 2 * item.Difference;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public object Clone() => new UVCLocalInvertVertical() { TargetVertices = this.TargetVertices };
    }
}
