using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BukkitPluginEditor.Initializer;

namespace BukkitPluginEditor.GUI.FileWizardPanels
{

    /// <summary>
    /// Defines the progression of Panels to make a Java Project.
    /// </summary>
    public class JavaProjectWizard
    {

        /// <summary>
        /// Default dependency for any project - JRE/JDK
        /// </summary>
        public static readonly string[] DefaultDependency = 
        {
            "Java " + JavaChecker.GetJavaVersionNumber(JavaChecker.GetJavaInstallationPath()),
            JavaChecker.GetJavaInstallationPath() 
        };

        /// <summary>
        /// Stores the current index of the progression.
        /// </summary>
        protected int index = 0;

        protected List<TabPage> cycle;
        
        private string ProjectPath;

        /// <summary>
        /// Creates a JavaProjectPanel object.
        /// </summary>
        public JavaProjectWizard()
        {
            cycle = new List<TabPage>();
        }

        /// <summary>
        /// Creates the panel progression to make a Java project.
        /// </summary>
        public virtual void CreatePanelProgression()
        {
            CreateProject();
            CreateDependencyWindow();
        }

        private void CreateProject()
        {
            TableLayoutPanel setup = new TableLayoutPanel();
            setup.AutoSize = true;
            setup.RowCount = 3;
            setup.ColumnCount = 2;

            Label name = new Label();
            name.Text = "Project Name:";
            name.Font = new Font("Segoe UI", 10.0f);
            name.TextAlign = ContentAlignment.TopCenter;
            setup.Controls.Add(name, 0, 0);

            RichTextBox box = new RichTextBox();
            box.Font = new Font("Segoe UI", 10.0f);
            box.AcceptsTab = false;
            box.Multiline = false;
            setup.Controls.Add(box, 1, 0);

            Label type = new Label();
            type.Dock = DockStyle.Fill;
            type.TextAlign = ContentAlignment.TopCenter;
            type.Text = "Project Type:";
            type.Font = new Font("Segoe UI", 10.0f);
            setup.Controls.Add(type, 0, 1);

            ComboBox projTypes = new ComboBox();
            projTypes.Font = new Font("Segoe UI", 10.0f);
            projTypes.Items.Add("Eclipse Project");
            projTypes.Items.Add("IntelliJ Module");
            projTypes.Items.Add("Maven Project");
            setup.Controls.Add(projTypes, 1, 1);

            Label loc = new Label();
            loc.TextAlign = ContentAlignment.TopCenter;
            loc.Font = new Font("Segoe UI", 10.0f);
            loc.Text = "Project Location:";
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
        }
        
        /// <summary>
        /// Add a dependency to the list.
        /// </summary>
        /// <param name="dependency"></param>
        protected void AddDependency(string[] dependency)
        {
            //get datagridview
        }

        private void RemoveDependency(object sender, EventArgs e)
        {
        }

        public TabPage Current()
        {
            return cycle[index];
        }

        public TabPage Next()
        {
            if (index < (cycle.Count - 1))
            {
                index++;
                return cycle[index];
            }

            else return null;
        }

        public TabPage Back()
        {
            if (index >= 1)
            {
                index--;
                return cycle[index];
            }

            else return null;
        }

    }
}
