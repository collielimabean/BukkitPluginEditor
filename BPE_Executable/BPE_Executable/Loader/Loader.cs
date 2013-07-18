using BukkitPluginEditor.Initializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BukkitPluginEditor.IO
{
    /// <summary>
    /// This class is responsible for loading files to the UI
    /// </summary>
    public class Loader
    {

        /// <summary>
        /// Constructs a Loader object.
        /// </summary>
        public Loader()
        {
            JavaChecker.GetJavaInstallationPath();
        }

    }
}
