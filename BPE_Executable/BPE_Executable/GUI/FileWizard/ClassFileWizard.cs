using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BukkitPluginEditor.GUI.FileWizard
{

    /// <summary>
    /// Provides the wizard to create a new Class.
    /// </summary>
    public class ClassFileWizard : Wizard
    {

        private string ProjectName;

        /// <summary>
        /// Constructs a new ClassFileWizard object.
        /// </summary>
        /// <param name="ActiveProjectName"></param>
        public ClassFileWizard(string ActiveProjectName) : base()
        {
            ProjectName = ActiveProjectName as string;

            if (ProjectName == null)
            {
                ProjectName = @"[Blank]";
            }

        }

        public override void CreatePanelDescription()
        {
            throw new NotImplementedException();
        }

        public override void CreatePanelProgression()
        {
            throw new NotImplementedException();
        }

    }
}
