using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BukkitPluginEditor.Initializer;

namespace BukkitPluginEditor.GUI.FileWizard
{

    /// <summary>
    /// Defines the progression of Panels to make a Java Project.
    /// </summary>
    public class JavaProjectWizard : Wizard
    {

        /// <summary>
        /// Default dependency for any project - JRE/JDK
        /// </summary>
        public static readonly string[] DefaultDependency = 
        {
            "Java " + JavaChecker.GetJavaVersionNumber(JavaChecker.GetJavaInstallationPath()),
            JavaChecker.GetJavaInstallationPath() 
        };

        private string ProjectPath;

        /// <summary>
        /// Creates a JavaProjectPanel object.
        /// </summary>
        public JavaProjectWizard() : base()
        {
        }

        /// <summary>
        /// Creates the panel progression for the Java Project file wizard.
        /// </summary>
        public override void CreatePanelProgression()
        {
            CreateProject();
            CreateDependencyWindow();
        }

        /// <summary>
        /// Creates the Panel Descriptions.
        /// </summary>
        public override void CreatePanelDescription()
        {
            descriptions.Add("Create a Java Project");
            descriptions.Add("External Dependencies");
        }

        private void CreateProject()
        {
            TableLayoutPanel setup = new TableLayoutPanel();
            setup.Padding = new Padding(10);
            setup.AutoSize = true;
            setup.RowCount = 3;
            setup.ColumnCount = 2;

            Label name = new Label();
            name.TextAlign = ContentAlignment.MiddleCenter;
            name.Dock = DockStyle.Fill;
            name.Text = "Name:";
            name.Font = new Font("Segoe UI", 10.0f);
            setup.Controls.Add(name, 0, 0);

            TextBox box = new TextBox();
            box.Font = new Font("Segoe UI", 10.0f);
            box.Dock = DockStyle.Fill;
            box.AcceptsTab = false;
            box.Multiline = false;
            setup.Controls.Add(box, 1, 0);

            Label type = new Label();
            type.TextAlign = ContentAlignment.MiddleCenter;
            type.Dock = DockStyle.Fill;
            type.Text = "Type:";
            type.Font = new Font("Segoe UI", 10.0f);
            setup.Controls.Add(type, 0, 1);

            ComboBox projTypes = new ComboBox();
            projTypes.Dock = DockStyle.Fill;
            projTypes.Font = new Font("Segoe UI", 10.0f);
            projTypes.Items.Add("Eclipse Project");
            projTypes.Items.Add("IntelliJ Module");
            projTypes.Items.Add("Maven Project");
            setup.Controls.Add(projTypes, 1, 1);

            Label loc = new Label();
            loc.TextAlign = ContentAlignment.MiddleCenter;
            loc.Dock = DockStyle.Fill;
            loc.Font = new Font("Segoe UI", 10.0f);
            loc.Text = "Location:";
            setup.Controls.Add(loc, 0, 2);

            Button browse = new Button();
            browse.Text = "Browse...";
            browse.Click += new EventHandler(browse_Click);
            setup.Controls.Add(browse, 1, 2);

            //add controls
            TabPage page = new TabPage();
            page.Controls.Add(setup);

            cycle.Add(page);
        }

        private void CreateDependencyWindow()
        {
            TableLayoutPanel dependencies = new TableLayoutPanel();
            dependencies.AutoSize = true;
            dependencies.RowCount = 2;
            dependencies.ColumnCount = 3;

            //Data view for dependencies
            DataGridView data = new DataGridView();
            data.ReadOnly = true;
            data.Dock = DockStyle.Fill;
            data.Columns.Add("Name", "Name");
            data.Columns.Add("Location", "Path");
            data.Rows.Add(DefaultDependency);
            dependencies.Controls.Add(data, 0, 0);
            dependencies.SetColumnSpan(data, 3);

            //create buttons
            Button add = new Button();
            add.Dock = DockStyle.Fill;
            add.Text = "Add";
            add.Click += new EventHandler(AddDependency);
            dependencies.Controls.Add(add, 0, 1);

            Button remove = new Button();
            remove.Dock = DockStyle.Fill;
            remove.Text = "Remove";
            remove.Enabled = false;
            dependencies.Controls.Add(remove, 1, 1);

            Button edit = new Button();
            edit.Text = "Edit";
            edit.Dock = DockStyle.Fill;
            dependencies.Controls.Add(edit, 2, 1);
            
            //add page
            TabPage two = new TabPage();
            two.Controls.Add(dependencies);

            cycle.Add(two);

        }

        private void browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ProjectPath = dialog.SelectedPath;
            }
            
        }

        private void AddDependency(object sender, EventArgs e)
        {

            Form add = new Form();
            add.ShowIcon = false;
            add.ShowInTaskbar = false;
            add.Size = new Size(500, 300);
            add.Text = "Add Dependency";

            add.ShowDialog();
        }
        
        /// <summary>
        /// Add a dependency to the list.
        /// </summary>
        /// <param name="dependency"></param>
        protected void AddDependency(string[] dependency)
        {
            //get datagridview
            TableLayoutPanel dependencies = (TableLayoutPanel) cycle[1].Controls[0];
            DataGridView view = (DataGridView) dependencies.GetControlFromPosition(0, 0);

            view.Rows.Add(dependency[0], dependency[1]);

        }

        private void RemoveDependency(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Removesa dependency.
        /// </summary>
        /// <param name="dependency"></param>
        protected void RemoveDependency(string[] dependency)
        {

        }



    }
}
