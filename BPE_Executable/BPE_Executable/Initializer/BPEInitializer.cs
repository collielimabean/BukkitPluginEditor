using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BukkitPluginEditor.Initializer
{
    /// <summary>
    /// Initializes all resources and files needed to run the main Bukkit Editor program.
    /// </summary>
    public sealed class BPEInitializer : IDisposable
    {

        #region Initializer members
        /// <summary>
        /// Specifies the URL from which to get the CraftBukkit XML artifact.
        /// </summary>
        public static readonly string CRAFTBUKKIT_XML_URL = "http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/artifacts/";

        /// <summary>
        /// Specifies the URL from which to get the Bukkit XML artifact.
        /// </summary>
        public static readonly string BUKKIT_XML_URL = "http://dl.bukkit.org/api/1.0/downloads/projects/bukkit/artifacts/";

        /// <summary>
        /// Specifies the domain of the download site for Bukkit/CraftBukkit files.
        /// </summary>
        public static readonly string BUKKITURL = "http://dl.bukkit.org";

        private BukkitSplashScreen screen;

        private string fullPath;

        /// <summary>
        /// Gets the Folder where all Bukkit Editor Application data is stored.
        /// </summary>
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

        private int? latestBukkitBuild;
        private int? latestCraftBukkitBuild;
        private int machineBukkitBuild;
        private int machineCraftBukkitBuild;

        delegate void SetTextCallBack(string text);
        delegate void AddValueCallBack(int value);

        #endregion

        /// <summary>
        /// Constructs a BPEInitializer object.
        /// </summary>
        /// <param name="screen">A splash screen from which to display progress</param>
        public BPEInitializer(BukkitSplashScreen screen)
        {
            this.screen = screen;
            screen.Show();

            if (JavaChecker.JavaInstalled())
            {
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

                SignalCompletion();
            }

            else
            {
                SetText("A Java SE 6 or higher installation was not found.");
                screen.ProgessBar.ForeColor = Color.Red;

                MessageBox.Show("Java SE 6 or higher is required to run this program.");

            }
    
        }

        /// <summary>
        /// Disposes of the BPEInitializer object.
        /// </summary>
        public void Dispose()
        {
            bukkitXML.Dispose();
            craftbukkitXML.Dispose();
            bukkitjar.Dispose();
            craftbukkitjar.Dispose();
            screen.Dispose();
        }

        /// <summary>
        /// Sends message to BukkitSplashScreen that all initial processes have completed.
        /// </summary>>
        private void SignalCompletion()
        {
            SetText("Starting...");
            screen.ProgessBar.Value = screen.ProgessBar.Maximum;
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

        #region Backgroundworkers

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
        /// Starts the downloading/checking Bukkit/CraftBukkit jar process.
        /// </summary>
        private void StartJarBackgroundWorkers()
        {

            screen.ProgressBarDescriptor.Text = "Checking Bukkit version...";

            bukkitjar = new BackgroundWorker();
            bukkitjar.DoWork += new DoWorkEventHandler(bukkitjar_doWork);
            bukkitjar.WorkerSupportsCancellation = true;
            bukkitjar.WorkerReportsProgress = true;
            bukkitjar.RunWorkerAsync();

            craftbukkitjar = new BackgroundWorker();
            craftbukkitjar.DoWork += new DoWorkEventHandler(craftbukkitjar_doWork);
            craftbukkitjar.WorkerSupportsCancellation = true;
            craftbukkitjar.WorkerReportsProgress = true;
        }

        /// <summary>
        /// Downloads the XML artifact from Bukkit.org
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bukkitXML_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            if (bw.CancellationPending == true)
            {
                return;
            }

            WebClient client = new WebClient();
            client.Headers.Add("accept", "application/xml");

            try
            {
                client.DownloadFile(new Uri(BUKKIT_XML_URL), fullPath + "\\Bukkit.xml");
            }

            catch (WebException)
            {
                return;
            }
        }

        /// <summary>
        /// Downloads the XML artifact for CraftBukkit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void craftbukkitXML_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            if (bw.CancellationPending == true)
            {
                return;
            }

            WebClient client = new WebClient();
            client.Headers.Add("accept", "application/xml");

            try
            {
                client.DownloadFile(new Uri(CRAFTBUKKIT_XML_URL), fullPath + "\\CraftBukkit.xml");
            }

            catch (WebException)
            {
                return;
            }

        }

        /// <summary>
        /// Loads the downloaded XML artifact into memory (Bukkit.xml)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bukkitXML_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            SetText("Checking for latest Bukkit build...");

            try
            {
                bukkit = XDocument.Load(fullPath + "\\Bukkit.xml");
            }

            catch (FileNotFoundException)
            {
                return;
            }

            var buildlist = bukkit.Descendants("build_number");

            latestBukkitBuild = Convert.ToInt32(buildlist.ElementAt(0).Value);

            AddValue(5);

        }

        /// <summary>
        /// Loads the downloaded XML artifact into memory (CraftBukkit.xml)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void craftbukkitXML_runWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            SetText("Checking for latest CraftBukkit build...");

            try
            {
                craftbukkit = XDocument.Load(fullPath + "\\CraftBukkit.xml");
            }

            catch (FileNotFoundException)
            {
                return;
            }

            var buildlist = craftbukkit.Descendants("build_number");

            latestCraftBukkitBuild = Convert.ToInt32(buildlist.ElementAt(0).Value);

            AddValue(5);
        }

        /// <summary>
        /// Checks the Bukkit API jar (if any), downloads the latest API build, and updates localversions.txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bukkitjar_doWork(object sender, DoWorkEventArgs e)
        {

            SetText("Checking Bukkit version...");

            LoadMachineVersions();

            if (latestBukkitBuild != null && (latestBukkitBuild != machineBukkitBuild) || (!File.Exists(fullPath + "\\Libraries\\bukkitlatest.jar")))
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

                UpdateMachineVersionsFile();
            }

            else if (latestBukkitBuild == machineBukkitBuild)
            {
                craftbukkitjar.RunWorkerAsync();
                return;
            }

            AddValue(30);

        }

        /// <summary>
        /// Checks for a downloaded version of CraftBukkit, downloads the latest if required, and updates localversions.txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void craftbukkitjar_doWork(object sender, DoWorkEventArgs e)
        {

            SetText("Checking CraftBukkit version...");

            if (latestCraftBukkitBuild != null && (machineCraftBukkitBuild != latestCraftBukkitBuild) || (!File.Exists(fullPath + "\\Server\\craftbukkitlatest.jar")))
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

            else if (latestCraftBukkitBuild == machineCraftBukkitBuild)
            {
                return;
            }

            AddValue(30);

        }

        #endregion

        /// <summary>
        /// Loads the local machine versions of Bukkit and CraftBukkit from the \\Bukkit Plugin Editor directory
        /// </summary>
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

                    UpdateMachineVersionsFile();

                }
            }

            else
            {
                machineBukkitBuild = 0;
                machineCraftBukkitBuild = 0;
                UpdateMachineVersionsFile();
            }

        }

        /// <summary>
        /// Writes to the machine versions file, replacing the old versions with the latest builds as assigned from the downloaded bukkit.xml and craftbukkit.xml.
        /// </summary>
        private void UpdateMachineVersionsFile()
        {
            File.WriteAllText(fullPath + "\\localversions.txt",
                                 "Bukkit," + latestBukkitBuild + "," +
                                 "CraftBukkit," + latestCraftBukkitBuild);
        }     

        /// <summary>
        /// Allows thread-safe calls to the BukkitSplashScreen and change the descriptor label's text.
        /// </summary>
        /// <param name="text"></param>
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

        /// <summary>
        /// Thread-safe change to the progress bar's value.
        /// </summary>
        /// <param name="value"></param>
        private void AddValue(int value)
        {

            if (screen.IsHandleCreated)
            {
                if (screen.ProgessBar.InvokeRequired)
                {
                    AddValueCallBack d = new AddValueCallBack(AddValue);

                    screen.ProgessBar.Invoke(d, new object[] { value });

                }

                else
                {
                    screen.ProgessBar.Value += value;
                }

            }


        }

    }
}
