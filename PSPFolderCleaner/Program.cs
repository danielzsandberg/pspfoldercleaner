using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSPFolderCleaner
{
    /// <summary>
    /// The main entry point for this program.
    /// </summary>
    class Program
    {
        #region

        static List<string> _extensionsToNotDelete =
            new List<string>()
            {
                ".cs",
                ".wxs",
                ".html",
                ".xaml"
            };
        
        #endregion

        #region Methods

        /// <summary>
        /// The main entry method for this program
        /// </summary>
        /// <param name="args">User inputted arguments</param>
        [STAThread]
        static void Main(string[] args)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Console.Write("Are you sure you want to delete all the files in {0}? (yes, no):", folderBrowserDialog.SelectedPath);
                string areYouSure = Console.ReadLine();
                if(areYouSure.Equals("yes"))
                {
                    GoAndDeleteNonCSFiles(new DirectoryInfo(folderBrowserDialog.SelectedPath));

                    Console.WriteLine("Purge complete. Press any key to exit...");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("Didn't do anything. Press any key to exit...");
                    Console.Read();
                }
            }
        }
 
        private static void GoAndDeleteNonCSFiles(DirectoryInfo directory)
        {
            IEnumerable<FileInfo> files = directory.EnumerateFiles();
            foreach(FileInfo file in files)
            {
                if(!_extensionsToNotDelete.Contains(file.Extension))
                {
                    Console.WriteLine("{0} deleted!", file.FullName);
                    file.Delete();
                }
            }

            IEnumerable<DirectoryInfo> directories = directory.EnumerateDirectories();
            foreach(DirectoryInfo currentDirectory in directories)
            {
                GoAndDeleteNonCSFiles(currentDirectory);
            }
        }

        #endregion
    }
}
