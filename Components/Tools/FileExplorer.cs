using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Components.Tools
{
    class FileExplorer
    {
        private string explorerPath;
        private string basePath;

        public FileExplorer(string path)
        {
            this.explorerPath = path;
            this.basePath = path;
        }

        public FileExplorer()
        {
        }

        public void goToBase()
        {
            this.explorerPath = this.basePath;
        }

        public string GetExplorerPath()
        {
            return this.explorerPath;
        }
        
        public void UpdatePath(string path)
        {
            if (this.explorerPath == null)
            {
                this.explorerPath = path;
                return;
            }
            this.explorerPath = Path.Combine(this.explorerPath, path);
        }

        public List<string> GetDirNames()
        {
            List<string> dirList = new List<string>();

            string[] subdirectoryEntries = Directory.GetDirectories(explorerPath);
            foreach (string subdirectory in subdirectoryEntries)
            {
                dirList.Add(Path.GetFileName(subdirectory));
            }

            return dirList;
        }

        public List<string> getPngList()
        {
            List<string> pngList = new List<string>();
            foreach (string fileName in Directory.GetFiles(this.explorerPath))
            {
                if (Path.GetExtension(fileName) == ".png")
                {
                    pngList.Add(fileName);
                }
            }
            return pngList;
        }

        public int pngListCount()
        {
            int cnt = getPngList().Count();
            return cnt;
        }

        public bool IsDocumentDir()
        {
            foreach (string fileName in Directory.GetFiles(this.explorerPath))
            {
                if (Path.GetExtension(fileName) == ".png")
                {
                    return true;
                }
            }

            return false;
        }

        public string GetConfigFile()
        {
            foreach (string fileName in Directory.GetFiles(this.explorerPath))
            {
                if (Path.GetExtension(fileName) == ".png")
                {
                    return fileName;
                }
            }

            return "";
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }
    }
}
