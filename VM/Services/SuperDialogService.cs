using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VM.Shard.Services;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace VM.Start.Services
{
    public class SuperDialogService : ISuperDialogService
    {
        public string GetFilePath(string filter = "所有文件|*.*", string title = "选择文件")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.Title = title; // 补上了你遗漏的标题赋值

            return openFileDialog.ShowDialog() != true ? string.Empty : openFileDialog.FileName;
        }

        public string GetFolderPath(string filter =" ", string title = "选择文件夹")
        {

            var dialog = new VistaFolderBrowserDialog
            {
                Description = "请选择包含测试图像的本地目录",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };
         
            return dialog.ShowDialog() != true ? string.Empty : dialog.SelectedPath;
        }

        public string SaveFilePath(string filter = "所有文件|*.*", string title = "保存文件")
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = title;

    
            saveFileDialog.OverwritePrompt = true;

            saveFileDialog.AddExtension = true;

            return saveFileDialog.ShowDialog() != true ? string.Empty : saveFileDialog.FileName;
        }

        public string SaveFolderPath(string filter =" ", string title = "选择保存文件夹")
        {
            return GetFolderPath(filter, title);
        }
    }
}
