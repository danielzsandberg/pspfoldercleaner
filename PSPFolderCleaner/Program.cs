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
                GoAndDeleteNonCSFiles(new DirectoryInfo(folderBrowserDialog.SelectedPath));

                Console.WriteLine("Purge complete.");
            }
        }
 
        private static void GoAndDeleteNonCSFiles(DirectoryInfo directory)
        {
            IEnumerable<FileInfo> files = directory.EnumerateFiles();
            foreach(FileInfo file in files)
            {
                if(file.Extension != ".cs" || file.Extension != ".wxs")
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
