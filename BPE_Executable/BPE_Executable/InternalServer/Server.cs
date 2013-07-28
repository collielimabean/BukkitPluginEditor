using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BukkitPluginEditor.InternalServer
{

    /// <summary>
    /// Defines the internal server implementation.
    /// </summary>
    public sealed class Server 
    {

        /// <summary>
        /// Location of all CraftBukkit jars.
        /// </summary>
        public static readonly string ServerJarFolder =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bukkit Plugin Editor\\Server\\";

        /// <summary>
        /// Default CraftBukkit jarfile.
        /// </summary>
        public static readonly string DefaultCraftBukkit = "craftbukkitlatest.jar";
        
        private string CraftBukkitFileName;

        /// <summary>
        /// Constructs a default Server object with the Default CraftBukkit file.
        /// </summary>
        public Server() : this(DefaultCraftBukkit)
        {
        }

        /// <summary>
        /// Constructs a server with the specified jarfile.
        /// </summary>
        /// <param name="fileName"></param>
        public Server(string fileName)
        {
            string[] split = fileName.Split('.');

            if (split.Length <= 1)
            {
                throw new ArgumentException("No extension on jarfile!");
            }

            else
            {
                if(split[1].Equals(".jar"))
                {
                    CraftBukkitFileName = fileName;
                }

                else
                {
                    throw new ArgumentException("Incorrect extension!");
                }
            }

        }

        /// <summary>
        /// Runs the server implementation on a different process.
        /// </summary>
        public void Run()
        {
            try
            {
                Process server = new Process();

                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "java";
                info.Arguments = "-Xmx1024M -jar " + ServerJarFolder + CraftBukkitFileName + " -o true";
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;

                server.StartInfo = info;
                server.Start();
            }

            catch (Exception)
            {
            }


        }

    }
}
