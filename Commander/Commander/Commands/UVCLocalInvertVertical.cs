using PEPlugin.Pmx;
using PEPExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Commands
{
    class UVCLocalInvertVertical : IUVCommand
    {
        public int ID => 1;

        public string Name => "UV局所垂直反転";

        public string Description => "選択頂点のUVを選択範囲の局所領域基準で垂直反転する";

        IEnumerable<IPXVertex> TargetVertices { get; }

        public UVCLocalInvertVertical(IEnumerable<IPXVertex> targetVertices)
        {
            TargetVertices = targetVertices;
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

        public void UnDo() => Do();

        public override string ToString()
        {
            return Name;
        }
    }
}
