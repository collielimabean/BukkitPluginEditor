using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BPE_Executable
{
    public static class BPEJavaChecker
    {

        /// <summary>
        /// Determines whether the user has Java 6 or higher installed.
        /// </summary>
        /// <returns>True if Java SE 6 or higher is installed, false if Java is not installed or Java SE 6 or higher is not installed.</returns>
        public static bool JavaInstalled()
        {

            string javaInstall = GetJavaInstallationPath();
            string filePath = Path.Combine(javaInstall, "javaw.exe");

            if (File.Exists(filePath))
            {
                int version = GetJavaVersionNumber(javaInstall);

                if (version >= 6)
                {
                    return true;
                }

            }

            return false;

        }

        public static int GetJavaVersionNumber(string installPath)
        {

            string[] split = installPath.Split('\\');

            int index = -1;

            for (int i = 0; i < split.Length; i++)
            {
                if (split[i].Contains("jdk") || split[i].Contains("jre"))
                {
                    index = i;
                    break;
                }

            }

            if (index == -1)
            {
                throw new Exception("Java installation found, but could not find folder.");
            }

            else
            {
                string version = split[index].Substring(3,3);

                return (int) ((Double.Parse(version)  % 1) * 10);
            }


        }

        /// <summary>
        /// Gets the Java installation path, if it exists.
        /// </summary>
        /// <returns>JAVA_HOME or installation path</returns>
        public static string GetJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");

            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";

            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(javaKey))
            {
                string currentVersion = rk.GetValue("CurrentVersion").ToString();

                using (RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    return key.GetValue("JavaHome").ToString();
                }
            }

        }
        
    }
}
