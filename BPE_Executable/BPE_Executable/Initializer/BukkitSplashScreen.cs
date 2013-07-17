using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BukkitPluginEditor.Initializer
{
    public class BukkitSplashScreen : Form
    {

        public BukkitSplashScreen()
        {

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(500, 300);

            this.Icon = new Icon("BukkitEditorLogo.ico");

            this.BackgroundImageLayout = ImageLayout.Center;
            this.BackgroundImage = Image.FromFile("BukkitEditorLogo.png");

            Label title = InitializeTitle();
            load = InitializeProgressBar();
            descriptor = InitializeProgressBarDescription();

            this.Controls.Add(title);
            this.Controls.Add(load);
            this.Controls.Add(descriptor);

        }


        private ProgressBar load;

        public ProgressBar ProgessBar
        {
            get
            {
                return load;
            }

            set
            {
                load = value;
            }
        }

        /// <summary>
        /// Initializes the progress bar to load components.
        /// </summary>
        /// <returns>
        /// The control Progress Bar docked at the bottom of the screen.
        /// </returns>
        private ProgressBar InitializeProgressBar()
        {
            //create directory in appdata
            //download latest bukkit and craftbukkit version

            ProgressBar bar = new ProgressBar();
            bar.Dock = DockStyle.Bottom;

            bar.Minimum = 0;
            bar.Maximum = 100;
            bar.Value = 0;

            return bar;

        }

        /// <summary>
        /// Initializes the title label.
        /// </summary>
        /// <returns>
        /// The title label "Bukkit Plugin Editor" docked at the top of the Form, with height of 300 and width 500.
        /// Font used is Segoe UI, 32.0f
        /// </returns>
        private Label InitializeTitle()
        {
            Label title = new Label();
            title.Dock = DockStyle.Top;
            title.Size = new Size(500, 75);
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Text = "Bukkit Plugin Editor";
            title.Font = new Font("Segoe UI", 32.0f);

            return title;
        }

        private Label descriptor;

        public Label ProgressBarDescriptor
        {
            get
            {
                return descriptor;
            }

            set
            {
                descriptor = value;
            }
        }

        /// <summary>
        /// Initializes the Progress Bar descriptor Label control.
        /// </summary>
        /// <returns>
        /// The default descriptor Label with text "Initializing...";
        /// </returns>
        private Label InitializeProgressBarDescription()
        {
            Label desc = new Label();
            desc.Size = new Size(500, 50);
            desc.TextAlign = ContentAlignment.MiddleCenter;
            desc.Text = "Initializing...";
            desc.Location = new Point(0, 225);
            desc.Font = new Font("Segoe UI", 12.0f);

            return desc;
        }

    }
}
