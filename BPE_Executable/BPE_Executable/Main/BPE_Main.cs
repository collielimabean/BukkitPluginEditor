using BukkitPluginEditor.Initializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BukkitPluginEditor.Main
{
    /// <summary>
    /// Entry class for the application.
    /// </summary>
    public class BPE_Main
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BPEInitializer init = new BPEInitializer(new BukkitSplashScreen());

            while (!init.Success)
            {
                Application.DoEvents();
            }

            init.Dispose();

            Application.Run(new BukkitPluginEditor.GUI.BPEForm());

        }
    }
}
