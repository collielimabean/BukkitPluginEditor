using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BukkitPluginEditor.GUI
{
    public partial class BPEForm : Form
    {

        private MenuStrip MainMenu;

        public BPEForm()
        {
            this.Text = "Bukkit Plugin Editor";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new Icon("BukkitEditorLogo.ico");
            this.BackColor = Color.LightGray;

            MainMenu = new MenuStrip();

            InitializeFileMenu();
            InitializeEditMenu();
            InitializeProjectMenu();
            InitializeBuildMenu();
            InitializeRunMenu();

            this.Controls.Add(MainMenu);
            this.MainMenuStrip = MainMenu;

        }

        private void InitializeFileMenu()
        {
            ToolStripMenuItem file = new ToolStripMenuItem("File");

            ToolStripMenuItem newFile = new ToolStripMenuItem("New");
            newFile.DropDownDirection = ToolStripDropDownDirection.Right;

            ToolStripMenuItem[] newfileItems = 
            {
                new ToolStripMenuItem("Project/Module"),
                new ToolStripMenuItem("Class"),
                new ToolStripMenuItem("Package"),
                new ToolStripMenuItem("Interface"),
                new ToolStripMenuItem("File"),
            };

            //Adjust internal buttons and add them to newFile
            foreach (ToolStripMenuItem button in newfileItems)
            {
                button.Width = 75;

                if (!button.Text.Equals("Project/Module") && !button.Text.Equals("File"))
                {
                    button.Enabled = false;
                }

                newFile.DropDownItems.Add(button);
            }

            ToolStripMenuItem open = new ToolStripMenuItem("Open");
            ToolStripMenuItem save = new ToolStripMenuItem("Save");
            ToolStripMenuItem saveas = new ToolStripMenuItem("Save As");
            ToolStripMenuItem exit = new ToolStripMenuItem("Exit");

            file.DropDownItems.Add(newFile);
            file.DropDownItems.Add(open);
            file.DropDownItems.Add(new ToolStripSeparator());
            file.DropDownItems.Add(save);
            file.DropDownItems.Add(saveas);
            file.DropDownItems.Add(new ToolStripSeparator());
            file.DropDownItems.Add(exit);

            MainMenu.Items.Add(file);
            
        }

        private void InitializeEditMenu()
        {
            ToolStripMenuItem edit = new ToolStripMenuItem("Edit");

            MainMenu.Items.Add(edit);
        }

        private void InitializeProjectMenu()
        {
            ToolStripMenuItem project = new ToolStripMenuItem("Project");

            MainMenu.Items.Add(project);
        }

        private void InitializeBuildMenu()
        {
            ToolStripMenuItem build = new ToolStripMenuItem("Build");
            MainMenu.Items.Add(build);
        }

        private void InitializeRunMenu()
        {
            ToolStripMenuItem run = new ToolStripMenuItem("Run");
            MainMenu.Items.Add(run);
        }

        

    }
}
