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
            ICommand selectedCommand = ((sender as ListBox).SelectedItem as ICommand);
            if (selectedCommand != null)
                textBoxDescription.Text = selectedCommand.Description;
        }

        private void WriteResourceDescription(object sender, EventArgs e)
        {
            object selectedItem = (sender as ListBox).SelectedItem;
            if (selectedItem != null)
                textBoxDescription.Text = CommandResourceInfo.Descriptions[(CommandResource)selectedItem];
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
            var selectedCommand = (ICommand)listBoxMacro.SelectedItem;
            if (selectedCommand == null)
                return;

            listBoxMacro.Items.Remove(selectedCommand);

            // コマンドが空になったら要求要素リストを全削除する
            if (listBoxMacro.Items.Count < 1)
            {
                listBoxRequires.Items.Clear();
                return;
            }

            // 削除したコマンドの要求要素を残ったコマンドが要求していないなら要求要素リストから削除する
            foreach (var resource in selectedCommand.RequireResources.Where(resource => !listBoxMacro.Items.Cast<ICommand>().Any(cmd => cmd.RequireResources.Contains(resource))))
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
            catch (Exception ex)
            {
                Utility.ShowExceptionMessage(ex);
            }
        }
    }
}
