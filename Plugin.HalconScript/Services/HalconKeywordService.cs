using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.HalconScript.Services
{
    public class HalconKeywordService : IKeywordService
    {
        private readonly Dictionary<HalconKeywordCategory, HashSet<string>> _keywordsByCategory;
        private readonly HashSet<string> _allKeywords;

        public HalconKeywordService()
        {
            _keywordsByCategory = new Dictionary<HalconKeywordCategory, HashSet<string>>
        {
            { HalconKeywordCategory.Operator, new HashSet<string>() },
            { HalconKeywordCategory.ControlFlow, new HashSet<string>() },
            { HalconKeywordCategory.DataType, new HashSet<string>() },
            { HalconKeywordCategory.Function, new HashSet<string>() },
            { HalconKeywordCategory.Constant, new HashSet<string>() }
        };
            _allKeywords = new HashSet<string>();
            InitializeKeywords();
        }

        private void InitializeKeywords()
        {
            // 控制流
            AddKeywords(HalconKeywordCategory.ControlFlow, new[]
            {
            "if", "else", "elseif", "endif", "for", "endfor",
            "while", "endwhile", "try", "catch", "endtry", "return"
        });

            // 数据类型
            AddKeywords(HalconKeywordCategory.DataType, new[]
            {
            "HImage", "HRegion", "HXLD", "HTuple", "int", "real", "string"
        });

            // 内置函数
            AddKeywords(HalconKeywordCategory.Function, new[]
            {
            "abs", "acos", "asin", "atan", "cos", "sin", "tan",
            "log", "log10", "exp", "sqrt", "min", "max", "round"
        });

            // 算子（简化示例，实际需拆分原长字符串）
            AddKeywords(HalconKeywordCategory.Operator, new[]
            {
            "gen_measure_rectangle2", "measure_pos", "measure_pairs",
            "read_image", "threshold", "connection", "area_center"
        });

            // 常量
            AddKeywords(HalconKeywordCategory.Constant, new[]
            {
            "H_MSG_TRUE", "H_MSG_FALSE", "H_MSG_VOID", "H_MSG_FAIL"
        });
        }

        private void AddKeywords(HalconKeywordCategory category, IEnumerable<string> keywords)
        {
            foreach (var keyword in keywords)
            {
                _keywordsByCategory[category].Add(keyword);
                _allKeywords.Add(keyword);
            }
        }

        public bool IsKeyword(string word) => _allKeywords.Contains(word);

        public IEnumerable<string> GetKeywords(HalconKeywordCategory category) => _keywordsByCategory[category];

        public IEnumerable<string> SearchKeywords(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix)) return Enumerable.Empty<string>();
            return _allKeywords.Where(k => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }
    }
}
