using Microsoft.Win32;
using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test.MyApplication
{
    public class ClientFunctions
    {
        /// <summary>
        /// Открывает файл и кодирует его в кодировке Base64
        /// </summary>
        /// <returns>base64 encoding file</returns>
        public void OpenFile(out string base64, out string extension)
        {
            string base64Data = string.Empty;
            string fileExtension = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                fileExtension = Path.GetExtension(filePath);
                try { base64Data = Convert.ToBase64String(File.ReadAllBytes(filePath)); }
                catch { }

                extension = fileExtension;
                base64 = base64Data;
            }
            base64 = base64Data;
            extension = fileExtension;
        }
    }
}
