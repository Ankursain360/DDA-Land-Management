using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Helper
{
    public class FileHelper
    {
        #region Document Download added By Praeen 08 Oct 2020
        public string SaveFile(string path, IFormFile file)
        {
            string FileName = "";
            string filePath = "";
            if (!Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
            }
            FileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            filePath = Path.Combine(path, FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filePath;
        }
        public string SaveFile1(string path, IFormFile file)
        {
            string FileName = "";
            string filePath = "";
            if (!Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
            }
            FileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            filePath = Path.Combine(path, FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return FileName;
        }
        public MemoryStream GetMemory(string path)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return memory;
        }
        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public string SaveFile(string sn7FilePath, List<IFormFile> sn7Filep)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
