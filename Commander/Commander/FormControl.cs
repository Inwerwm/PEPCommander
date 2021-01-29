using PEPCommander.Commands;
using PEPExtensions;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEPCommander
{
    public partial class FormControl : Form
    {
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }

        public FormControl(IPERunArgs args)
        {
            Args = args;
            listBoxCommands.Items.AddRange(CommandManager.List.ToArray());

            InitializeComponent();
            Reload();
        }

        internal void Reload()
        {
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();
        }

        private void WriteCommandDescription(object sender, EventArgs e)
        {
            textBoxDescription.Text = ((sender as ListBox).SelectedItem as ICommand).Description;
        }

        private void WriteResourceDescription(object sender, EventArgs e)
        {
            textBoxDescription.Text = CommandResourceInfo.Descriptions[(CommandResource)(sender as ListBox).SelectedItem];
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxCommands.SelectedItem as ICommand;
            if (selectedItem is null)
                return;

            listBoxMacro.Items.Add(selectedItem);
            foreach (var resource in selectedItem.RequireResources.Where(resource => !listBoxRequires.Items.Contains(resource)))
            {
                listBoxRequires.Items.Add(resource);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var selectedItem = (ICommand)listBoxMacro.SelectedItem;
            if (selectedItem != null)
                listBoxMacro.Items.Remove(selectedItem);

            // コマンドが空になったら要求要素リストを全削除する
            if(listBoxMacro.Items.Count < 1)
            {
                listBoxRequires.Items.Clear();
                return;
            }

            // 削除したコマンドの要求要素を残ったコマンドが要求していないなら要求要素リストから削除する
            foreach (var resource in selectedItem.RequireResources.Where(resource => !listBoxMacro.Items.Cast<ICommand>().Any(cmd => cmd.RequireResources.Contains(resource))))
            {
                listBoxRequires.Items.Remove(resource);
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            
        }
    }
}
