using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.HalconScript.Services
{
    public enum HalconKeywordCategory
    {
        Operator,    // 算子
        ControlFlow, // 控制流（if/for/while）
        DataType,    // 数据类型（HImage/HTuple）
        Function,    // 内置函数
        Constant     // 常量（H_MSG_TRUE）
    }

    public interface IKeywordService
    {
        bool IsKeyword(string word);
        IEnumerable<string> GetKeywords(HalconKeywordCategory category);
        IEnumerable<string> SearchKeywords(string prefix);
    }
}
