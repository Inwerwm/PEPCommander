using PEPCommander.Requires;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPCommander.Commands
{
    class CreateWrapperBone : ICommand, IRequireBones, IRequirePmx
    {
        public int ID => 3;

        public string Name => "ラッパーボーンを作成";

        public string Description => $"選択ボーンに同一位置の付与親を作成する。{Environment.NewLine}もとのボーンは名前の後ろにRが付加される";

        public IEnumerable<CommandResource> RequireResources => new CommandResource[] { CommandResource.Bone, CommandResource.Pmx };

        IEnumerable<IPXBone> Bones { get; set; }
        IPXPmx Pmx { get; set; }

        public void Supply(IEnumerable<IPXBone> bones)
        {
            Bones = bones;
        }

        public void Supply(IPXPmx pmx)
        {
            Pmx = pmx;
        }

        public object Clone() => new CreateWrapperBone() { Bones = Bones, Pmx = Pmx };

        public void Do()
        {
            foreach (var item in Bones.Select((bone, i) => (bone, i)))
            {
                item.bone.Name += "R";
                item.bone.NameE += "R";

                IPXBone createdBone = item.bone.Clone() as IPXBone;

                item.bone.AppendParent = createdBone;
                item.bone.AppendRatio = 1;
                item.bone.IsAppendLocal = false;
                item.bone.IsAppendRotation = true;
                item.bone.IsAppendTranslation = true;

                Pmx.Bone.Insert(item.i, createdBone);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
