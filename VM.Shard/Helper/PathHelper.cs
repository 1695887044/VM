using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Helper
{
    public static class PathHelper
    {
        /// <summary>
        /// 获取解决方案路径
        /// </summary>
        /// <returns></returns>
        public static string GetSolutionPath()
        {
            try
            {
                // 获取当前程序集的路径
                string assemblyPath = Assembly.GetExecutingAssembly().Location;
                string currentDirectory = Path.GetDirectoryName(assemblyPath);

                // 向上级目录查找.sln文件
                DirectoryInfo directory = new DirectoryInfo(currentDirectory);
                while (directory != null)
                {
                    // 查找当前目录下的.sln文件
                    FileInfo[] slnFiles = directory.GetFiles("*.sln");
                    if (slnFiles != null && slnFiles.Length > 0)
                    {
                        return directory.FullName;
                    }

                    // 继续向上级目录查找
                    directory = directory.Parent;
                }

                // 未找到解决方案文件
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取解决方案路径失败: {ex.Message}");
                return null;
            }
        }
    }
}
