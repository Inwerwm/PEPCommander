using PEPlugin.Pmx;
using PEPlugin.SDX;
using PEPExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVCommand
{
    /// <summary>
    /// 自身を内包している面の情報を持った頂点
    /// </summary>
    class VertexWithFace : IPXVertex
    {
        IPXVertex Vertex { get; }
        /// <summary>
        /// 内包されている面
        /// </summary>
        public IPXFace ParentFace { get; }

        public VertexWithFace(IPXVertex vertex, IPXFace face)
        {
            Vertex = vertex;
            ParentFace = face;

            if (!ParentFace.ToVertices().Contains(Vertex))
                throw new ArgumentException("指定頂点を内包していない面が渡されました。");
        }

        // IPXVertex
        public V3 Position { get => Vertex.Position; set => Vertex.Position = value; }
        public V3 Normal { get => Vertex.Normal; set => Vertex.Normal = value; }
        public V2 UV { get => Vertex.UV; set => Vertex.UV = value; }
        public V4 UVA1 { get => Vertex.UVA1; set => Vertex.UVA1 = value; }
        public V4 UVA2 { get => Vertex.UVA2; set => Vertex.UVA2 = value; }
        public V4 UVA3 { get => Vertex.UVA3; set => Vertex.UVA3 = value; }
        public V4 UVA4 { get => Vertex.UVA4; set => Vertex.UVA4 = value; }
        public IPXBone Bone1 { get => Vertex.Bone1; set => Vertex.Bone1 = value; }
        public IPXBone Bone2 { get => Vertex.Bone2; set => Vertex.Bone2 = value; }
        public IPXBone Bone3 { get => Vertex.Bone3; set => Vertex.Bone3 = value; }
        public IPXBone Bone4 { get => Vertex.Bone4; set => Vertex.Bone4 = value; }
        public float Weight1 { get => Vertex.Weight1; set => Vertex.Weight1 = value; }
        public float Weight2 { get => Vertex.Weight2; set => Vertex.Weight2 = value; }
        public float Weight3 { get => Vertex.Weight3; set => Vertex.Weight3 = value; }
        public float Weight4 { get => Vertex.Weight4; set => Vertex.Weight4 = value; }
        public bool QDEF { get => Vertex.QDEF; set => Vertex.QDEF = value; }
        public bool SDEF { get => Vertex.SDEF; set => Vertex.SDEF = value; }
        public V3 SDEF_C { get => Vertex.SDEF_C; set => Vertex.SDEF_C = value; }
        public V3 SDEF_R0 { get => Vertex.SDEF_R0; set => Vertex.SDEF_R0 = value; }
        public V3 SDEF_R1 { get => Vertex.SDEF_R1; set => Vertex.SDEF_R1 = value; }
        public float EdgeScale { get => Vertex.EdgeScale; set => Vertex.EdgeScale = value; }

        public object Clone()
        {
            return Vertex.Clone();
        }
    }
}
