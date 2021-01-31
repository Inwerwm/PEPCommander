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
            foreach (var bone in Bones)
            {
                IPXBone createdBone = bone.Clone() as IPXBone;

                bone.Name += "R";
                bone.NameE += "R";

                bone.AppendParent = createdBone;
                bone.AppendRatio = 1;
                bone.IsAppendLocal = false;
                bone.IsAppendRotation = true;
                bone.IsAppendTranslation = true;

                Pmx.Bone.Insert(Pmx.Bone.IndexOf(bone), createdBone);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
