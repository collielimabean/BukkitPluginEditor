using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BPE_Executable
{
    public sealed class BPEInitializer
    {

        public static readonly string CRAFTBUKKIT_XML_URL = "http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/artifacts/";
        public static readonly string BUKKIT_XML_URL = "http://dl.bukkit.org/api/1.0/downloads/projects/bukkit/artifacts/";
        public static readonly string BUKKITURL = "http://dl.bukkit.org/";
        public static readonly string BPEURL = "http://sourceforge.net/projects/bukkitpe/files/Alpha%20launcher/BukkitPluginEditor.jar/download";
        public static readonly string BPE_FILE_NAME = "BukkitPluginEditor.jar";

        private BukkitSplashScreen screen;

        private string fullPath;
        public string FolderPath
        {
            get
            {
                return fullPath;
            }
        }

        private XDocument bukkit;
        private XDocument craftbukkit;

        private BackgroundWorker bukkitXML;
        private BackgroundWorker craftbukkitXML;
        private BackgroundWorker bukkitjar;
        private BackgroundWorker craftbukkitjar;
        private BackgroundWorker bpejar;

        private int latestBukkitBuild;
        private int latestCraftBukkitBuild;
        private int machineBukkitBuild;
        private int machineCraftBukkitBuild;

        delegate void SetTextCallBack(string text);
        delegate void AddValueCallBack(int value);

        public BPEInitializer(BukkitSplashScreen screen)
        {
            this.screen = screen;
            screen.Show();

            CreateFileSystem();

            StartXMLBackgroundWorkers();

            while (bukkitXML.IsBusy || craftbukkitXML.IsBusy)
            {
                Application.DoEvents();
            }

            StartJarBackgroundWorkers();
            
            while (bukkitjar.IsBusy || craftbukkitjar.IsBusy)
            {
                Application.DoEvents();
            }

            StartBPEBackgroundWorker();

            while (bpejar.IsBusy)
            {
                Application.DoEvents();
            }

            StartBukkitEditor();

        }

        /// <summary>
        /// Starts the process for executing the Bukkit Plugin Editor and also exits program on successful handoff to JVM.
        /// </summary>>
        private void StartBukkitEditor()
        {
            if (BPEJavaChecker.JavaInstalled())
            {

                //save location
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string editorPath = "\"" + appData +  "\\Bukkit Plugin Editor\\" + BPE_FILE_NAME + "\"";

                Process javaProcess = new Process();

                //proof of concept
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "javaw";
                startInfo.Arguments = @"-jar " + editorPath;

                javaProcess.StartInfo = startInfo;
                javaProcess.Start();

                screen.ProgressBarDescriptor.Text = "Starting...";
                screen.ProgessBar.Value = screen.ProgessBar.Maximum;
                screen.Close();

            }

            else
            {
                //show dialog to install JVM
                MessageBox.Show("Please install Java SE 6 or higher to use this program.");

            }

        }

        /// <summary>
        /// Creates the Application storage folder and all subfolders required for the Bukkit Plugin Editor.
        /// </summary>
        private void CreateFileSystem()
        {
            screen.ProgressBarDescriptor.Text = "Checking File System...";
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            fullPath = appdata + "\\Bukkit Plugin Editor\\";

            //create BPE housing folder
            Directory.CreateDirectory(fullPath);

            //Create Server folder to house the Craftbukkit jars and server world
            Directory.CreateDirectory(fullPath + "\\Server\\");

            //Create Libraries folder to house Bukkit jars
            Directory.CreateDirectory(fullPath + "\\Libraries\\");

            screen.ProgessBar.Value += 5;
        }

        /// <summary>
        /// Starts the XML checking processes.
        /// </summary>
        private void StartXMLBackgroundWorkers()
        {

            screen.ProgressBarDescriptor.Text = "Getting artifacts...";
            
            bukkitXML = new BackgroundWorker();
            bukkitXML.DoWork += new DoWorkEventHandler(bukkitXML_doWork);
            bukkitXML.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bukkitXML_runWorkCompleted);
            bukkitXML.WorkerSupportsCancellation = true;
            bukkitXML.WorkerReportsProgress = true;
            bukkitXML.RunWorkerAsync();

            craftbukkitXML = new BackgroundWorker();
            craftbukkitXML.DoWork += new DoWorkEventHandler(craftbukkitXML_doWork);
            craftbukkitXML.RunWorkerCompleted += new RunWorkerCompletedEventHandler(craftbukkitXML_runWorkCompleted);
            craftbukkitXML.WorkerSupportsCancellation = true;
            craftbukkitXML.WorkerReportsProgress = true;
            craftbukkitXML.RunWorkerAsync();
        }

        /// <summary>
        /// Starts the downloading/checking jar process.
        /// </summary>
        private void StartJarBackgroundWorkers()
        {

            screen.ProgressBarDescriptor.Text = "Checking Bukkit version...";

            bukkitjar = new BackgroundWorker();
            bukkitjar.DoWork += new DoWorkEventHandler(bukkitjar_doWork);
            bukkitjar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bukkitjar_runWorkCompleted);
            bukkitjar.WorkerSupportsCancellation = true;
            bukkitjar.WorkerReportsProgress = true;
            bukkitjar.RunWorkerAsync();

            craftbukkitjar = new BackgroundWorker();
            craftbukkitjar.DoWork += new DoWorkEventHandler(craftbukkitjar_doWork);
            craftbukkitjar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(craftbukkitjar_runWorkCompleted);
            craftbukkitjar.WorkerSupportsCancellation = true;
            craftbukkitjar.WorkerReportsProgress = true;
        }

        private void StartBPEBackgroundWorker()
        {
            bpejar = new BackgroundWorker();
            bpejar.DoWork += new DoWorkEventHandler(bpejar_doWork);
            bpejar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bpejar_runWorkCompleted);
            bpejar.WorkerSupportsCancellation = true;
            bpejar.WorkerReportsProgress = true;
            bpejar.RunWorkerAsync();
        }

        private void bukkitXML_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            if (bw.CancellationPending == true)
            {
                return;
            }

            WebClient client = new WebClient();
            client.Headers.Add("accept", "application/xml");
            client.DownloadFile(new Uri(BUKKIT_XML_URL), fullPath + "\\Bukkit.xml");
        }

        private void craftbukkitXML_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            if (bw.CancellationPending == true)
            {
                return;
            }

            WebClient client = new WebClient();
            client.Headers.Add("accept", "application/xml");
            client.DownloadFile(new Uri(CRAFTBUKKIT_XML_URL), fullPath + "\\CraftBukkit.xml");

        }

        private void bukkitXML_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            SetText("Checking for latest Bukkit build...");
            
            bukkit = XDocument.Load(fullPath + "\\Bukkit.xml");

            var buildlist = bukkit.Descendants("build_number");

            latestBukkitBuild = Convert.ToInt32(buildlist.ElementAt(0).Value);

            AddValue(5);

        }

        private void craftbukkitXML_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            SetText("Checking for latest CraftBukkit build...");

            craftbukkit = XDocument.Load(fullPath + "\\CraftBukkit.xml");

            var buildlist = craftbukkit.Descendants("build_number");

            latestCraftBukkitBuild = Convert.ToInt32(buildlist.ElementAt(0).Value);

            AddValue(5);
        }

        private void bukkitjar_doWork(object sender, DoWorkEventArgs e)
        {

            SetText("Checking Bukkit version...");

            LoadMachineVersions();
            
            if ((latestBukkitBuild != machineBukkitBuild) || (!File.Exists(fullPath + "\\Libraries\\bukkitlatest.jar")))
            {

                SetText("Downloading latest Bukkit version...");

                //determine URL
                var list = bukkit.Root.Elements("results").
                                       Elements("list-item").
                                       Elements("file").
                                       Elements("url");

                string RelativeUrl = list.ElementAt(0).Value;
                string fullURL = BUKKITURL + RelativeUrl;

                WebClient client = new WebClient();
                client.DownloadFile(new Uri(fullURL), fullPath + "\\Libraries\\bukkitlatest.jar");

            }

            else if(latestBukkitBuild == machineBukkitBuild)
            {
                return;
            }
        }

        private void bukkitjar_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddValue(30);
            craftbukkitjar.RunWorkerAsync();
        }

        private void craftbukkitjar_doWork(object sender, DoWorkEventArgs e)
        {
            
            SetText("Checking CraftBukkit version...");

            if ((machineCraftBukkitBuild != latestCraftBukkitBuild) || (!File.Exists(fullPath + "\\Server\\craftbukkitlatest.jar")))
            {

                SetText("Downloading latest CraftBukkit version...");

                //get url
                var list = craftbukkit.Root.Elements("results").
                       Elements("list-item").
                       Elements("file").
                       Elements("url");

                string RelativeUrl = list.ElementAt(0).Value;
                string fullURL = BUKKITURL + RelativeUrl;

                WebClient client = new WebClient();
                client.DownloadFile(new Uri(fullURL), fullPath + "\\Server\\craftbukkitlatest.jar");

                //update localversions.txt
                UpdateMachineVersionsFile();
            }

            else
            {
                return;
            }

        }

        private void LoadMachineVersions()
        {
            if (File.Exists(fullPath + "\\localversions.txt"))
            {

                string[] versions;

                try
                {

                    using (StreamReader sr = new StreamReader(fullPath + "\\localversions.txt"))
                    {
                        versions = sr.ReadLine().Split(',');
                    }

                    machineBukkitBuild = Convert.ToInt32(versions.ElementAt(1));
                    machineCraftBukkitBuild = Convert.ToInt32(versions.ElementAt(3));

                }

                catch (Exception)
                {
                    machineBukkitBuild = 0;
                    machineCraftBukkitBuild = 0;

                    //rewrite the file
                    File.WriteAllText(fullPath + "\\localversions.txt",
                                            "Bukkit," + latestBukkitBuild + "," +
                                            "CraftBukkit," + latestCraftBukkitBuild);

                }
            }

            else
            {
                machineBukkitBuild = 0;
                machineCraftBukkitBuild = 0;

                File.WriteAllText(fullPath + "\\localversions.txt",
                        "Bukkit," + latestBukkitBuild + "," +
                        "CraftBukkit," + latestCraftBukkitBuild);
            }

        }

        private void UpdateMachineVersionsFile()
        {
            File.WriteAllText(fullPath + "\\localversions.txt",
                                 "Bukkit," + latestBukkitBuild + "," +
                                 "CraftBukkit," + latestCraftBukkitBuild);
        }

        private void craftbukkitjar_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddValue(30);
            SetText("Checking if BPE is up to date...");
        }

        private void bpejar_doWork(object sender, DoWorkEventArgs e)
        {
            if(!File.Exists(fullPath + "\\BukkitPluginEditor.jar"))
            {
                SetText("Downloading Bukkit Plugin Editor...");
                
                WebClient client = new WebClient();
                client.DownloadFile(new Uri(BPEURL), fullPath + "\\BukkitPluginEditor.jar");
            }
        }

        private void bpejar_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddValue(15);
            SetText("Starting...");
        }

        private void SetText(string text)
        {

            if (screen.ProgressBarDescriptor.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(SetText);
                screen.ProgressBarDescriptor.Invoke(d, new object[] { text });
            }

            else
            {
                screen.ProgressBarDescriptor.Text = text;
            }
        }

        private void AddValue(int value)
        {
            if (screen.ProgessBar.InvokeRequired)
            {
                AddValueCallBack d = new AddValueCallBack(AddValue);
                screen.ProgressBarDescriptor.Invoke(d, new object[] { value });
            }

            else
            {
                screen.ProgessBar.Value += value;
            }

        }

    }
}
