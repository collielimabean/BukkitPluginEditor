using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BukkitPluginEditor.GUI.FileWizard
{
    public class PackageFileWizard : Wizard
    {

        private string path;

        public PackageFileWizard(string ParentProjectPath) : base()
        {
            path = ParentProjectPath;
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
