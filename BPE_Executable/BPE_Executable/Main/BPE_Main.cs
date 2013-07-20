using BukkitPluginEditor.Initializer;
using BukkitPluginEditor.GUI;
using System;
using System.IO;
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

            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bukkit Plugin Editor";

            if (IsConnectedToInternet() || (File.Exists(folderpath + "\\Server\\craftbukkitlatest.jar") &&
                     File.Exists(folderpath + "\\Libraries\\bukkitlatest.jar")))
            {
                BPEInitializer init = new BPEInitializer(new BukkitSplashScreen());
                init.Dispose();
                Application.Run(new BPEForm());
            }

            else
            {
                MessageBox.Show("Unable to retrieve core parts of the application. \nPlease connect to the internet to use this program.", "Error");
            }



        }

        /// <summary>
        /// Checks for internet connection at startup.
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedToInternet()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
