using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BukkitPluginEditor.GUI.FileWizardPanels;

namespace BukkitPluginEditor.GUI
{

    /// <summary>
    /// Lays out a form with options to create a new file.
    /// </summary>
    public class NewFileWizard : Form
    {

        #region Members

        private Panel DefaultFileMenu;
        private Label Descriptor;
        private Button next;
        private Button back;
        private Button cancel;

        private dynamic wizard;

        private readonly string DefaultMessage = "What would you like to make?";

        /// <summary>
        /// Gets the button 'Next'.
        /// </summary>
        public Button Next
        {
            get
            {
                return next;
            }
        }

        /// <summary>
        /// Gets the button 'Back'.
        /// </summary>
        public Button Back
        {
            get
            {
                return back;
            }
        }

        private int index = -1;

        #endregion

        /// <summary>
        /// Constructs a NewFileWizard object.
        /// </summary>
        public NewFileWizard()
        {
            InitializeForm();
            InitializeButtons();
            InitializeLabel();
            InitializeDefaultPanel();
        }

        #region Initialization

        private void InitializeForm()
        {
            this.Text = "New File Wizard";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
        }

        private void InitializeButtons()
        {
            next = new Button();
            next.Text = "Next";
            next.Size = new Size(100, 50);
            next.Location = new Point(260, 400);
            next.Anchor = AnchorStyles.Right;
            next.Click += new EventHandler(NextButton_Click);
            
            back = new Button();
            back.Text = "Back";
            back.Size = next.Size;
            back.Location = new Point(150, 400);
            back.Anchor = AnchorStyles.Right;
            back.Enabled = false;

            cancel = new Button();
            cancel.Text = "Cancel";
            cancel.Size = back.Size;
            cancel.Location = new Point(40, 400);
            cancel.Anchor = AnchorStyles.Right;
            cancel.Click += delegate { this.Close(); };

            this.Controls.Add(next);
            this.Controls.Add(back);
            this.Controls.Add(cancel);
           
        }

        private void InitializeLabel()
        {
            Descriptor = new Label();
            Descriptor.Text = DefaultMessage;
            Descriptor.TextAlign = ContentAlignment.MiddleCenter;
            Descriptor.Font = new Font("Segoe UI", 14.0f);
            Descriptor.Dock = DockStyle.Top;

            this.Controls.Add(Descriptor);
        }

        private void InitializeDefaultPanel()
        {
            DefaultFileMenu = new Panel();
            DefaultFileMenu.Size = new Size(this.Width, (int)(0.75 * this.Height));
            DefaultFileMenu.Location = new Point(0, Descriptor.Size.Height + 5);

            ListBox box = new ListBox();
            box.Font = new Font("Segoe UI", 12.0f);

            List<string> choices = new List<string>();
            choices.Add("Bukkit Plugin Project");
            choices.Add("Java Project");
            choices.Add("Java Package/Folder");
            choices.Add("Java Class");
            choices.Add("Java Interface");
            choices.Add("YML File");
            choices.Add("Other (Text-Based) File");

            box.Dock = DockStyle.Fill;
            box.DataSource = choices;

            DefaultFileMenu.Controls.Add(box);

            this.Controls.Add(DefaultFileMenu);

        }

        #endregion

        #region Event handlers and helper methods

        /// <summary>
        /// Defines actions for the 'Next' button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextButton_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel) this.Controls[4];

            TabControl control = new TabControl();
            control.Appearance = TabAppearance.Buttons;
            control.ItemSize = new Size(0,1);
            control.SizeMode = TabSizeMode.Fixed;
            control.Size = panel.ClientSize;

            //default
            if (index == -1)
            {
                ListBox box = (ListBox) panel.Controls[0];

                TabPage initial = DetermineTree((string) box.SelectedItem);

                foreach (Control c in panel.Controls)
                {
                    panel.Controls.Remove(c);
                }

                control.Controls.Add(initial);
                panel.Controls.Add(control);

                this.Refresh();
                

                index++;
            }

            else if (index > -1)
            {
                if (wizard != null)
                {
                    //cast variable to proper wizardtype
                    TabPage page = wizard.Next();

                    if (page == null)
                    {
                        //todo run project generator background here. Remember the while(<thread>.isBusy()!)
                        this.Dispose();
                        return;
                    }

                    foreach (Control c in panel.Controls)
                    {
                        panel.Controls.Remove(c);
                    }

                    panel.Size = new Size(this.Width, (int)(0.75 * this.Height));

                    control.Controls.Add(page);
                    panel.Controls.Add(control);

                    this.Refresh();
                    index++;
                }

                else MessageBox.Show("Fatal error - could not get wizard type!");
            }
           
        }

        private TabPage DetermineTree(string input)
        {
            if (input.Equals("Bukkit Plugin Project"))
            {
                BukkitPluginProjectWizard wiz = new BukkitPluginProjectWizard();
                wiz.CreatePanelProgression();

                wizard = wiz;

                return wiz.Current();
            }

            else if (input.Equals("Java Project"))
            {
                JavaProjectWizard wiz = new JavaProjectWizard();
                wiz.CreatePanelProgression();

                wizard = wiz;
                return wiz.Current();
            }

            else
            {
                return new TabPage();
            }

        }

        #endregion

    }
}
