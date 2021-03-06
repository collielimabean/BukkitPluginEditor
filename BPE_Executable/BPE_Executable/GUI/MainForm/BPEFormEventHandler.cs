﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using BukkitPluginEditor.InternalServer;

namespace BukkitPluginEditor.GUI
{
    public sealed partial class BPEForm
    {

        /// <summary>
        /// Eventhandler for calling the options menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Options_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

        }

        #region Form and Child Controls Resizing

        /// <summary>
        /// Resizes the controls when the parent form is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Form_Resize(object sender, EventArgs e)
        {
            explorer.Size = new Size((int)(0.2 * this.ClientSize.Width), this.ClientSize.Height);

            container.Location = new Point(explorer.Size.Width, MainMenu.Height);
            container.Size = new Size((int)(0.8 * this.ClientSize.Width), this.ClientSize.Height);
        }

        /// <summary>
        /// Resizes the SplitContainer's controls when the SplitContainer is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Container_Resize(object sender, EventArgs e)
        {
            SplitterPanel top = container.Panel1;
            SplitterPanel bottom = container.Panel2;

            top.Size = container.Panel1.ClientSize;
            bottom.Size = container.Panel2.ClientSize;
        }

        #endregion

        #region Tab handlers

        /// <summary>
        /// Close tab actions when menu item invoked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OptionCloseTab(object sender, EventArgs e)
        {
            try
            {
                string name = this.EditorTabs.SelectedTab.Name;

                for (int i = 0; i < EditorTabs.TabPages.Count; i++)
                {
                    if (EditorTabs.TabPages[i].Name.Equals(name))
                    {
                        EditorTabs.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
                
            catch (NullReferenceException)
            {
                Console.WriteLine("Selected tab is null, NullReferenceException caught and handled.");
                return;
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

            TabControl ctrl = (TabControl) sender;

            //Looping through the controls.
            for (int i = 0; i < ctrl.TabPages.Count; i++)
            {
                Rectangle r = ctrl.GetTabRect(i);

                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);

                if (closeButton.Contains(e.Location))
                {
                    if (MessageBox.Show("Close this tab without saving?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ctrl.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #endregion

        #region 'File' menu event handlers

        public void Open_Clicked(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;

            DialogResult user = open.ShowDialog();

            if (user == DialogResult.OK)
            {
                string name = open.FileName;
            }
        }

        #endregion

        #region 'Edit' menu event handlers

        /// <summary>
        /// Defines actions when Copy button or Ctrl+C invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Copy_Clicked(object sender, EventArgs e)
        {
            FastColoredTextBox box = (FastColoredTextBox)EditorTabs.SelectedTab.Controls[0];
            box.Copy();
        }

        /// <summary>
        /// Defines actions when 'Cut' button or Ctrl+X is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Cut_Clicked(object sender, EventArgs e)
        {
            FastColoredTextBox box = (FastColoredTextBox) EditorTabs.SelectedTab.Controls[0];
            box.Cut();
        }

        /// <summary>
        /// Defines action when paste button or shortkey Ctrl+V is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Paste_Clicked(object sender, EventArgs e)
        {
            FastColoredTextBox box = (FastColoredTextBox) EditorTabs.SelectedTab.Controls[0];
            box.Paste();
        }

        #endregion

        #region 'Run' button event handlers

        public void Run_Clicked(object sender, EventArgs e)
        {
            //compile
            //call server
        }

        #endregion


    }
}
