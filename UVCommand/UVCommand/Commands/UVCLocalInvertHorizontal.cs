using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVCommand.Commands
{
    class UVCLocalInvertHorizontal
    {
        IEnumerable<IPXVertex> TargetVertices { get; }

        public string Name => "局所鏡像反転";

        public string Description => "選択頂点のUVを選択範囲の局所領域基準で鏡像反転する";

        public UVCLocalInvertHorizontal(IEnumerable<IPXVertex> targetVertices)
        {
            TargetVertices = targetVertices;
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

        public void UnDo() => Do();

    }
}
