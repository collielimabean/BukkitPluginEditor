using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BukkitPluginEditor.GUI
{

    /// <summary>
    /// Defines a BukkitTabPage.
    /// </summary>
    public class BukkitTabPage : TabPage
    {

        /// <summary>
        /// Constructs a BukkitTabPage.
        /// </summary>
        /// <param name="title"></param>
        public BukkitTabPage(string title) : base(appendClosingTabSymbol(title))
        {
        }

        /// <summary>
        /// Adds an 'X' symbol at at the end of a tab title to indicate a closing symbol.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        protected static string appendClosingTabSymbol(string original)
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
