using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.IO;
using FileManager;
using System.Threading;

namespace ConfigManager
{
    public partial class Service1 : ServiceBase
    {
        Logger logger;
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }

    class Logger
    {
        private ConfigManager configManager;
        private List<Functional> Options = new List<Functional>();
        private string targDirPath;
        private string scrDirPath;
        private bool enabled = true;

        private FileSystemWatcher watcher;

        object obj = new object();

        public Logger()
        {
            configManager = new ConfigManager();
            Options = configManager.GetOptions();

            if (Options.Count >= 0)
            {
                scrDirPath = Options[0].Source;
                targDirPath = Options[0].Target;
                watcher = new FileSystemWatcher(scrDirPath);
                watcher.Deleted += Watcher_Deleted;
                watcher.Created += Watcher_Created;
                watcher.Changed += Watcher_Changed;
                watcher.Renamed += Watcher_Renamed;
            }
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "создан";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter("C:\\2\\takts.txt", true))
                {
                    writer.WriteLine(String.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
                if (fileEvent == "изменен" && filePath.EndsWith(".txt"))
                {
                    string source = "C:\\2\\sdir\\";
                    string target = "C:\\2\\tdir\\";
                    string name = filePath.Split((char)92)[filePath.Split((char)92).Length - 1];
                    General exp = new General(source, target, name);
                    exp.Changes();
                }
            }
        }
    }
}