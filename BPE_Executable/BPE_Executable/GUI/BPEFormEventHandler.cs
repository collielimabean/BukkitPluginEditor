using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace BukkitPluginEditor.GUI
{
    public partial class BPEForm
    {

        /// <summary>
        /// Determines how to form resizes when user changes size of window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Form_Resize(object sender, EventArgs e)
        {
            Form form = (Form) sender;

            explorer.Location = new Point(0, MainMenu.Size.Height);
            explorer.Size = new Size(explorer.Size.Width, (this.Size.Height - MainMenu.Size.Height));
            container.Location = new Point(explorer.Size.Width, MainMenu.Size.Height + 2);
            container.Size = new Size(this.Size.Width - explorer.Size.Width, this.Size.Height - MainMenu.Size.Height);

        }

        /// <summary>
        /// Close tab actions when menu item invoked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OptionCloseTab(object sender, EventArgs e)
        {
            string name = this.tabs.SelectedTab.Name;

            for (int i = 0; i < tabs.TabPages.Count; i++)
            {
                if (tabs.TabPages[i].Name.Equals(name))
                {
                    tabs.TabPages.RemoveAt(i);
                    break;
                }
            }

        }

        /// <summary>
        /// Defines the actions for closing a tab when 'X' is clicked.
        /// (Author from CodeProject)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseCloseTab(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < tabs.TabPages.Count; i++)
            {
                Rectangle r = tabs.GetTabRect(i);

                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);

                if (closeButton.Contains(e.Location))
                {
                    if (MessageBox.Show("Close this tab without saving?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        tabs.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Eventhandler for calling the options menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Options_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;

        }

        #region 'Edit' menu event handlers

        /// <summary>
        /// Defines actions when Copy button or Ctrl+C invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Copy_Clicked(object sender, EventArgs e)
        {
            FastColoredTextBox box = (FastColoredTextBox)tabs.SelectedTab.Controls[0];
            box.Copy();
        }

        /// <summary>
        /// Defines actions when 'Cut' button or Ctrl+X is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Cut_Clicked(object sender, EventArgs e)
        {
            FastColoredTextBox box = (FastColoredTextBox) tabs.SelectedTab.Controls[0];
            box.Cut();
        }

        /// <summary>
        /// Defines action when paste button or shortkey Ctrl+V is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Paste_Clicked(object sender, EventArgs e)
        {
            FastColoredTextBox box = (FastColoredTextBox) tabs.SelectedTab.Controls[0];
            box.Paste();
        }

        #endregion

        #region Editor highlighting and autocomplete

        /// <summary>
        /// Standard style for keywords in Eclipse. (purple and bolded)
        /// </summary>
        public static readonly Style KeywordStyle = new TextStyle(Brushes.Purple, null, FontStyle.Bold);

        /// <summary>
        /// Standard tyle for single line comments in Eclipse. (green)
        /// </summary>
        public static readonly Style SingleLineCommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);

        /// <summary>
        /// Standard style for multiline comments in Eclipse. (blue)
        /// </summary>
        public static readonly Style MultiLineCommentStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);

        /// <summary>
        /// Defines all Java keywords
        /// </summary>
        public static readonly string[] JavaKeywords =
        { 
            "abstract", "assert", "boolean", "break", "byte", "case",
            "catch", "char", "class", "const", "continue", "default",
            "do", "double", "else", "enum", "extends", "final", "finally",
            "float", "for", "goto", "if", "implements", "import", "instanceof",
            "int", "interface", "long", "native", "new", "package", "private",
            "protected", "public", "return", "short", "static", "strictfp", "super",
            "switch", "synchronized", "this", "throw", "throws", "transient", "try",
            "void", "violatile", "while", "false", "true", "null"
        };

        /// <summary>
        /// Defines the actions of the syntax text box.
        /// Original author: Pavel Torgashov <see cref=">https://github.com/PavelTorgashov/"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FastColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //set keywords to standard style
            foreach(string keyword in JavaKeywords)
            {
                e.ChangedRange.SetStyle(KeywordStyle, keyword);
            }

            //set commenting style
            e.ChangedRange.SetStyle(SingleLineCommentStyle, @"//.*$", RegexOptions.Singleline);

            //code folding settings
            e.ChangedRange.SetFoldingMarkers("{", "}");

        }

        #endregion

    }
}
