using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TotalCommander.Model
{
    static class Manipulation
    {
        public static void CopyFile(String sourceFile, String destinationFile)
        {
            File.Copy(sourceFile, destinationFile);
        }

        public static void CopyDir(String sourceDir, String destinationDir)
        {

            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDir(subDir.FullName, newDestinationDir);
            }
        }
    }
}
