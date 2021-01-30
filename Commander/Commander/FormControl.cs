using PEPCommander.Commands;
using PEPCommander.Requires;
using PEPExtensions;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PEPCommander
{
    public partial class FormControl : Form
    {
        IPERunArgs Args { get; }

        public FormControl(IPERunArgs args)
        {
            Args = args;

            InitializeComponent();
            Reload();

            listBoxCommands.Items.AddRange(CommandManager.List.ToArray());
        }

        internal void Reload()
        {
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
            if (listBoxMacro.Items.Count < 1)
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
            try
            {
                var launch = new CommandLauncher(Args);
                launch.Launch(listBoxMacro.Items.Cast<ICommand>());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
