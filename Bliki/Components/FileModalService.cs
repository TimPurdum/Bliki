using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bliki.Components
{
    public class FileModalService: ModalService
    {
        public string? FilePath { get; set; }
        public IFileListEntry? File { get; set; }
    }
}
