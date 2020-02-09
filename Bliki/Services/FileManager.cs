using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bliki.Services
{
    public class FileManager
    {
        public async Task Save(IFileListEntry uploadFile, string savePath)
        {
            using var ms = await uploadFile.ReadAllAsync();
            using var saveFile = File.OpenWrite(Path.Combine(_uploadsDirectory, savePath));
            saveFile.Write(ms.ToArray());
            saveFile.Close();
        }

        private string _uploadsDirectory = Path.Combine("WikiPages", "uploads");
    }
}
