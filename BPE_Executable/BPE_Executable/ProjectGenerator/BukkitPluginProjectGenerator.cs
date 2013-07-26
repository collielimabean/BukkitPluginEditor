using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BukkitPluginEditor.GUI.FileWizardPanels;

namespace BukkitPluginEditor.ProjectGenerator
{

    /// <summary>
    /// Does all necessary File I/O to automatically generate a basic plugins.
    /// </summary>
    public class BukkitPluginProjectGenerator : ProjectGenerator
    {

        /// <summary>
        /// Constructs a BukkitPluginProjectGenerator object.
        /// </summary>
        /// <param name="PluginName"></param>
        /// <param name="PluginLocation"></param>
        /// <param name="Dependencies"></param>
        /// <param name="ProjectType"></param>
        public BukkitPluginProjectGenerator(string PluginName, string PluginLocation, List<string> Dependencies, ProjectTypes ProjectType)
            : base(PluginName, PluginLocation, Dependencies, ProjectType)
        {
        }

        /// <summary>
        /// Creates the file system for a new Bukkit Plugin Project, as well as generate files.
        /// </summary>
        /// <returns></returns>
        public bool CreatePlugin()
        {
            bool createSuccess = base.CreateProject();

            //create files here


            return true;
        }

    }
}
