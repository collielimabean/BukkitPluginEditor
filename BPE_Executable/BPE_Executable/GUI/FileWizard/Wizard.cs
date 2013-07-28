using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BukkitPluginEditor.GUI.FileWizard
{

    /// <summary>
    /// Abstract base class to create a file wizard.
    /// </summary>
    public abstract class Wizard
    {

        /// <summary>
        /// Stores the current index of the progression.
        /// </summary>
        protected int index = 0;

        /// <summary>
        /// Details the cycle progression for the file wizard.
        /// </summary>
        protected List<TabPage> cycle;

        /// <summary>
        /// Details the descriptions to describe the TabPage cycle.
        /// </summary>
        protected List<string> descriptions;

        /// <summary>
        /// Initializes fields.
        /// </summary>
        public Wizard()
        {
            cycle = new List<TabPage>();
            descriptions = new List<string>();
        }

        /// <summary>
        /// Creates the panel progression.
        /// </summary>
        public abstract void CreatePanelProgression();

        /// <summary>
        /// Creates the panel description.
        /// </summary>
        public abstract void CreatePanelDescription();

        /// <summary>
        /// Gets the current TabPage.
        /// </summary>
        /// <returns>The current TabPage according to the field 'index'.</returns>
        public TabPage Current()
        {
            return cycle[index];
        }

        /// <summary>
        /// Returns the next TabPage in the cycle. If none, this method returns null.
        /// </summary>
        /// <returns>Next TabPage, null if last element.</returns>
        public TabPage Next()
        {
            if (index < (cycle.Count - 1))
            {
                index++;
                return cycle[index];
            }

            else return null;
        }

        /// <summary>
        /// Returns the last TabPage.
        /// </summary>
        /// <returns>The last TabPage, null if current index is the first element.</returns>
        public TabPage Back()
        {
            if (index >= 1)
            {
                index--;
                return cycle[index];
            }

            else return null;
        }

        /// <summary>
        /// Gets the current description.
        /// </summary>
        /// <returns></returns>
        public string CurrentDescription()
        {
            return descriptions[index];
        }

        /// <summary>
        /// Gets the next description, if it exists.
        /// </summary>
        /// <returns>A string containing the next description, but returns null if index is at the end of the list.</returns>
        public string NextDescription()
        {
            if (index < (descriptions.Count - 1))
            {
                index++;
                return descriptions[index];
            }

            else return null;
        }

        /// <summary>
        /// Gets the previous description, if it exists.
        /// </summary>
        /// <returns>A string containing the previous description, null if index is first element.</returns>
        public string PreviousDescription()
        {
            if (index >= 1)
            {
                index--;
                return descriptions[index];
            }

            else return null;
        }

    }
}
