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
    public partial class BPEForm : Form
    {

        private MenuStrip MainMenu;
        private TreeView explorer;
        private SplitContainer container;
        private TabControl tabs;

        /// <summary>
        /// Gets or sets the tab pane for the editor.
        /// </summary>
        public TabControl TabEditor
        {
            get
            {
                return tabs;
            }

            set
            {
                tabs = value;
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

        /// <summary>
        /// Constructs a new Bukkit Plugin Editor Window.
        /// </summary>
        public BPEForm()
        {
            this.Text = "Bukkit Plugin Editor";
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;
            this.Icon = new Icon("BukkitEditorLogo.ico");
            this.BackColor = Color.LightGray;

            Screen scrn = Screen.FromControl(this);
            int width = scrn.WorkingArea.Width;
            int height = scrn.WorkingArea.Height;

            this.Size = new Size((int) (0.6 * width), (int) (0.8 * height));
        
            MainMenu = new MenuStrip();

            explorer = new TreeView();
            explorer.Location = new Point(0, MainMenu.Size.Height);
            explorer.Size = new Size(explorer.Size.Width, (this.Size.Height - MainMenu.Size.Height));

            container = new SplitContainer();
            container.Orientation = Orientation.Horizontal;
            container.Location = new Point(explorer.Size.Width + 1, MainMenu.Size.Height + 2); //additions are offsets
            container.Size = new Size(this.Size.Width - explorer.Size.Width, this.Size.Height - MainMenu.Size.Height);

            InitializeMenus();
            InitializePackageExplorer();
            InitializeEditorAndConsole();

            this.Controls.Add(MainMenu);
            this.Controls.Add(explorer);
            this.Controls.Add(container);
            this.MainMenuStrip = MainMenu;

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

            ToolStripMenuItem newFile = new ToolStripMenuItem("New");
            newFile.ShortcutKeys = Keys.Control | Keys.N;
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

        private void InitializePackageExplorer()
        {
            TreeNode node = new TreeNode("Workspace");
            node.Nodes.Add(new TreeNode("Child"));

            node.Expand();

            explorer.Nodes.Add(node);

        }

        private void InitializeEditorAndConsole()
        {

            SplitterPanel top = container.Panel1;
            SplitterPanel bottom = container.Panel2;

            tabs = new TabControl();
            tabs.Size = top.ClientSize;
            tabs.MouseDown += new MouseEventHandler(MouseCloseTab);

            FastColoredTextBox box = new FastColoredTextBox();
            box.Size = top.ClientSize;
            box.AcceptsTab = true;
            box.AcceptsReturn = true;
            box.TextChanged += new EventHandler<TextChangedEventArgs>(FastColorTextBox_TextChanged);
            
            TabPage test = new TabPage(appendClosingTabSymbol("Testing..."));
            test.Controls.Add(box);

            tabs.Controls.Add(test);

            top.Controls.Add(tabs);
            bottom.BackColor = Color.LightCoral;

        }

        private string appendClosingTabSymbol(string original)
        {

            int spaces = 4;

            for (int i = 0; i < spaces; i++)
            {
                original += " ";
            }

            original += "X";

            return original;
        }

    }
}
