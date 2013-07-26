using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace BukkitPluginEditor.GUI
{
    /// <summary>
    /// This class lays out the main UI for Bukkit Plugin development.
    /// </summary>
    public sealed partial class BPEForm : Form
    {

        #region Members

        private MenuStrip MainMenu;
        private TreeView explorer;
        private SplitContainer container;
        private TabControl EditorTabs;
        private TabControl ConsoleTabs;

        /// <summary>
        /// Gets or sets the tab pane for the editor.
        /// </summary>
        public TabControl TabEditor
        {
            get
            {
                return EditorTabs;
            }

            set
            {
                EditorTabs = value;
            }
        }

        /// <summary>
        /// Gets or sets the TabControl for the console.
        /// </summary>
        public TabControl TabConsole
        {
            get
            {
                return ConsoleTabs;
            }

            set
            {
                ConsoleTabs = value;
            }
        }

        /// <summary>
        /// Gets or sets the Project Explorer in the Bukkit Plugin Editor.
        /// </summary>
        public TreeView ProjectExplorer
        {
            get
            {
                return explorer;
            }

            set
            {
                explorer = value;
            }
        }

        #endregion

        /// <summary>
        /// Constructs a new Bukkit Plugin Editor Window.
        /// </summary>
        public BPEForm()
        {

            InitializeForm();

            //initialize controls here
            MainMenu = new MenuStrip();
            this.MainMenuStrip = MainMenu;

            InitializeMenus();
            InitializePackageExplorer();
            InitializeEditorAndConsole();

            this.Controls.Add(MainMenu);
            this.Controls.Add(explorer);
            this.Controls.Add(container);

        }

        private void InitializeForm()
        {
            //initialize form
            this.Text = "Bukkit Plugin Editor (Alpha)";
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;
            this.Icon = new Icon("BukkitEditorLogo.ico");
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            //dynamically create screen such that is 3/5s of screen width and 4/5s of screen height.
            Screen scrn = Screen.FromControl(this);
            int width = scrn.WorkingArea.Width;
            int height = scrn.WorkingArea.Height;

            this.Size = new Size((int)(0.6 * width), (int)(0.8 * height));
            this.SizeChanged += new EventHandler(Form_Resize);
        }

        #region Menustrip initialization

        private void InitializeMenus()
        {
            InitializeFileMenu();
            InitializeEditMenu();
            InitializeProjectMenu();
            InitializeRunMenu();
            InitializeHelpMenu();
        }

        private void InitializeFileMenu()
        {
            ToolStripMenuItem file = new ToolStripMenuItem("File");

            ToolStripMenuItem newFile = new ToolStripMenuItem("New...");
            newFile.ShortcutKeys = Keys.Control | Keys.N;
            newFile.Click += delegate { NewFileWizard w = new NewFileWizard(); w.ShowDialog(); };
            
            ToolStripMenuItem open = new ToolStripMenuItem("Open...");
            open.ShortcutKeys = Keys.Control | Keys.O;

            ToolStripMenuItem save = new ToolStripMenuItem("Save");
            save.ShortcutKeys = Keys.Control | Keys.S;

            ToolStripMenuItem saveas = new ToolStripMenuItem("Save As");
            saveas.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;

            ToolStripMenuItem export = new ToolStripMenuItem("Export");
            export.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;

            ToolStripMenuItem exit = new ToolStripMenuItem("Exit");
            exit.ShortcutKeys = Keys.Control | Keys.Q;
            exit.Click += delegate {    this.Dispose();    };

            file.DropDownItems.Add(newFile);
            file.DropDownItems.Add(open);
            file.DropDownItems.Add(new ToolStripSeparator());
            file.DropDownItems.Add(save);
            file.DropDownItems.Add(saveas);
            file.DropDownItems.Add(new ToolStripSeparator());
            file.DropDownItems.Add(export);
            file.DropDownItems.Add(new ToolStripSeparator());
            file.DropDownItems.Add(exit);

            MainMenu.Items.Add(file);
            
        }

        private void InitializeEditMenu()
        {
            ToolStripMenuItem edit = new ToolStripMenuItem("Edit");

            ToolStripMenuItem copy = new ToolStripMenuItem("Copy");
            copy.ShortcutKeys = Keys.Control | Keys.C;
            copy.Click += new EventHandler(Copy_Clicked);

            ToolStripMenuItem cut = new ToolStripMenuItem("Cut");
            cut.ShortcutKeys = Keys.Control | Keys.X;
            cut.Click += new EventHandler(Cut_Clicked);

            ToolStripMenuItem paste = new ToolStripMenuItem("Paste");
            paste.ShortcutKeys = Keys.Control | Keys.V;
            paste.Click += new EventHandler(Paste_Clicked);

            ToolStripMenuItem closeTab = new ToolStripMenuItem("Close Tab");
            closeTab.ShortcutKeys = Keys.Control | Keys.W;
            closeTab.Click += new EventHandler(OptionCloseTab);

            ToolStripMenuItem preferences = new ToolStripMenuItem("Preferences");

            edit.DropDownItems.Add(copy);
            edit.DropDownItems.Add(cut);
            edit.DropDownItems.Add(paste);
            edit.DropDownItems.Add(new ToolStripSeparator());
            edit.DropDownItems.Add(closeTab);
            edit.DropDownItems.Add(new ToolStripSeparator());
            edit.DropDownItems.Add(preferences);

            MainMenu.Items.Add(edit);
        }

        private void InitializeProjectMenu()
        {
            ToolStripMenuItem project = new ToolStripMenuItem("Project");

            MainMenu.Items.Add(project);
        }

        private void InitializeRunMenu()
        {
            ToolStripSplitButton run = new ToolStripSplitButton("Run");
            run.Enabled = false;

            ToolStripMenuItem runItem = new ToolStripMenuItem("Run");
            runItem.ShortcutKeys = Keys.F7;

            ToolStripMenuItem runconfig = new ToolStripMenuItem("Run Configurations");

            run.DropDownItems.Add(runconfig);

            MainMenu.Items.Add(run);
        }

        private void InitializeHelpMenu()
        {
            ToolStripMenuItem help = new ToolStripMenuItem("Help");

            ToolStripMenuItem about = new ToolStripMenuItem("About Bukkit Plugin Editor");
            about.Click += delegate { AboutBox box = new AboutBox(); box.ShowDialog(); };

            help.DropDownItems.Add(about);

            MainMenu.Items.Add(help);
        }

        #endregion

        #region Initialize Controls

        private void InitializePackageExplorer()
        {
            explorer = new TreeView();
            explorer.Location = new Point(0, MainMenu.Height);
            explorer.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            explorer.Size = new Size((int)(0.2 * this.ClientSize.Width), this.ClientSize.Height);
            explorer.Scrollable = true;

            TreeNode node = new TreeNode("Workspace");

            node.Expand();

            explorer.Nodes.Add(node);
        }

        private void InitializeEditorAndConsole()
        {

            //initialize container
            container = new SplitContainer();
            container.Location = new Point(explorer.Size.Width, MainMenu.Height);
            container.Size = new Size((int)(0.8 * this.ClientSize.Width), this.ClientSize.Height);
            container.SplitterDistance = (int) (0.6 * container.Size.Height);
            container.SizeChanged += new EventHandler(Container_Resize);
            container.Orientation = Orientation.Horizontal;

            //initialize panels
            SplitterPanel top = container.Panel1;
            SplitterPanel bottom = container.Panel2;

            //Editor tab control
            EditorTabs = new TabControl();
            EditorTabs.Dock = DockStyle.Fill;
            EditorTabs.Size = top.ClientSize;
            EditorTabs.MouseDown += new MouseEventHandler(MouseCloseTab);

            BukkitEditorTabPage trollfaic = new BukkitEditorTabPage("testing 2", top);

            EditorTabs.Controls.Add(trollfaic);

            //Console tab control
            ConsoleTabs = new TabControl();
            ConsoleTabs.Dock = DockStyle.Fill;
            ConsoleTabs.Size = top.ClientSize;

            TabPage errors = new TabPage("Error List");
            TabPage console = new TabPage("Console");

            ConsoleTabs.Controls.Add(errors);
            ConsoleTabs.Controls.Add(console);

            //Add tabcontrols to containers.
            top.Controls.Add(EditorTabs);
            bottom.Controls.Add(ConsoleTabs);

        }

        #endregion
    }
}
