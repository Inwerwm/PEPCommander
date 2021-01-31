using PEPCommander.Requires;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Commands
{
    class UVCLocalInvertHorizontal : ICommand, IRequireVertices
    {
        public int ID => 2;

        public string Name => "UV局所鏡像反転";

        public string Description => "選択頂点のUVを選択範囲の局所領域基準で鏡像反転する";

        public IEnumerable<IPXVertex> TargetVertices { get; set; }

        public IEnumerable<CommandResource> RequireResources => new CommandResource[] { CommandResource.Vertex };

        public void Supply(IEnumerable<IPXVertex> vertices)
        {
            TargetVertices = vertices;
        }

        public void Do()
        {
            float xAverage = TargetVertices.Average(vtx => vtx.UV.X);
            IEnumerable<(IPXVertex Vertex, float Difference)> diffs = TargetVertices.Select(vtx => (vtx, vtx.UV.X - xAverage));

            foreach (var item in diffs)
            {
                item.Vertex.UV.X -= 2 * item.Difference;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            return new UVCLocalInvertHorizontal() { TargetVertices = this.TargetVertices };
        }
    }
}
