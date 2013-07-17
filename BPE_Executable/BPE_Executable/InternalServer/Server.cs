using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BukkitPluginEditor.InternalServer
{
    public sealed class Server 
    {

        public static readonly string ServerJarFolder =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bukkit Plugin Editor\\Server\\";

        public static readonly string DefaultCraftBukkit = "craftbukkitlatest.jar";
        
        private string CraftBukkitFileName;

        public Server() : this(DefaultCraftBukkit)
        {
        }

        public Server(string fileName)
        {
            if (!fileName.EndsWith(".jar"))
            {
                CraftBukkitFileName = fileName + ".jar";
            }

            else
            {
                CraftBukkitFileName = fileName;
            }
        }

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
