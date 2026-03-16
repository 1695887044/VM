using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Services
{
    /// <summary>
    /// 负责调用文件对话框
    /// </summary>
    public interface ISuperDialogService
    {
        public string GetFilePath(string filter = "所有文件|*.*", string title = "选择文件");
        public string GetFolderPath(string filter=" ", string title = "选择文件夹");

        public string SaveFilePath(string filter = "所有文件|*.*", string title = "选择文件");
        public string SaveFolderPath(string filter=" ", string title = "选择文件");
    }
}
