using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BukkitPluginEditor.GUI.FileWizard
{
    public class InterfaceFileWizard : Wizard
    {
        private string ProjectName;

        /// <summary>
        /// Constructs a new IntefaceFileWizard object.
        /// </summary>
        /// <param name="ActiveProjectName"></param>
        public InterfaceFileWizard(string ActiveProjectName) : base()
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
