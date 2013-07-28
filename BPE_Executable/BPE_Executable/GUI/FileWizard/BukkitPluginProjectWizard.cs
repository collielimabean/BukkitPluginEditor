using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BukkitPluginEditor.GUI.FileWizard
{

    /// <summary>
    /// Creates the Panel progression for a Bukkit Plugin Project.
    /// </summary>
    public class BukkitPluginProjectWizard : JavaProjectWizard
    {

        /// <summary>
        /// Defines the standard Bukkit jar used for BukkitPluginProject.
        /// </summary>
        public static readonly string[] BukkitDependency = 
        {
            "Bukkit", (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bukkit Plugin Editor\\Libraries\\bukkitlatest.jar")
        };

        /// <summary>
        /// Constructs a BukkitPluginProjectPanel object.
        /// </summary>
        public BukkitPluginProjectWizard() : base()
        {
        }

        /// <summary>
        /// Defines the progression of panels to create the Bukkit Project Panel wizard.
        /// </summary>
        public override void CreatePanelProgression()
        {
            base.CreatePanelProgression();
            base.AddDependency(BukkitDependency);
        }

    }
}
