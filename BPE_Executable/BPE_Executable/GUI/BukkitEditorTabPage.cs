using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace BukkitPluginEditor.GUI
{
    /// <summary>
    /// Extends functionality of System.Windows.Forms.TabPage to include a FastColoredTextBox
    /// </summary>
    public class BukkitEditorTabPage : BukkitTabPage
    {

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
        /// Gets or sets the editor for the BukkitTabPage
        /// </summary>
        public FastColoredTextBox Editor
        {
            get
            {
                return box;
            }

            set
            {
                box = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'modified' state of the editor in the TabPage.
        /// </summary>
        public bool Modified
        {
            get
            {
                return changed;
            }

            set
            {
                changed = value;
            }
        }

        private FastColoredTextBox box;
        private bool changed;

        /// <summary>
        /// Constructs a BukkitTabPage with a FastColoredTextBox as an editor.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="parent"></param>
        public BukkitEditorTabPage(string title, Panel parent) : base(title)
        {
            InitializeFastColoredTextBox(parent);

            this.SizeChanged += new EventHandler(Tab_Resized);

            changed = false;
            this.Controls.Add(box);
        }

        private void InitializeFastColoredTextBox(Panel parent)
        {
            box = new FastColoredTextBox();
            box.Font = new Font("Courier New", 10f);
            box.Size = parent.ClientSize;
            box.AcceptsTab = true;
            box.AcceptsReturn = true;
            box.TextChanged += new EventHandler<TextChangedEventArgs>(BoxText_Changed);
        }

        /// <summary>
        /// Defines actions when tab is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tab_Resized(object sender, EventArgs e)
        {
            box.Size = this.ClientSize;
        }

        /// <summary>
        /// Gives actions changes in text in the TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BoxText_Changed(object sender, TextChangedEventArgs e)
        {
            //Display that the tab has been modified
            if (!changed)
            {
                changed = true;
                Text = "*" + Text;
            }

            //set keywords to standard style
            foreach (string keyword in JavaKeywords)
            {
                e.ChangedRange.SetStyle(KeywordStyle, keyword);
            }

            //set commenting style
            e.ChangedRange.SetStyle(SingleLineCommentStyle, @"//.*$", RegexOptions.Singleline);

            //code folding settings
            e.ChangedRange.SetFoldingMarkers("{", "}");

        }

    }
}
