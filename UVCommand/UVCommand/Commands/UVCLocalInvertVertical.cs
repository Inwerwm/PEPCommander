using PEPlugin.Pmx;
using PEPExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVCommand.Commands
{
    class UVCLocalInvertVertical : IUVCommand
    {
        IEnumerable<IPXFace> TargetFaces { get; }
        IEnumerable<VertexWithFace> TargetVertices { get; }

        public UVCLocalInvertVertical(IEnumerable<IPXFace> targetFaces)
        {
            TargetFaces = targetFaces;

            TargetVertices = TargetFaces.SelectMany(face => face.ToVertices().Select(vtx => new VertexWithFace(vtx, face)));
        }

        public void Do()
        {

        }
    }
}
