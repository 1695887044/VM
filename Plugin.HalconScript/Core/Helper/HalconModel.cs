using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.HalconScript.Core.Helper
{
    /// <summary>
    /// Halcon HDevelop XML 根对象
    /// </summary>
    public class HDevelopFile
    {
        /// <summary>XML文件版本</summary>
        public string FileVersion { get; set; }

        /// <summary>Halcon版本</summary>
        public string HalconVersion { get; set; }

        /// <summary>过程信息</summary>
        public Procedure Procedure { get; set; }
    }

    /// <summary>
    /// Halcon 过程（Procedure）
    /// </summary>
    public class Procedure
    {
        /// <summary>过程名称</summary>
        public string Name { get; set; }

        /// <summary>接口参数</summary>
        public ProcedureInterface Interface { get; set; } = new ProcedureInterface();

        /// <summary>过程体（代码+注释）</summary>
        public List<CodeElement> Body { get; set; } = new List<CodeElement>();

        /// <summary>文档参数ID列表</summary>
        public List<string> DocParameterIds { get; set; } = new List<string>();
    }

    /// <summary>
    /// 过程接口参数（输入/输出）
    /// </summary>
    public class ProcedureInterface
    {
        /// <summary>输入图像参数（io节点）</summary>
        public List<Parameter> InputIconicParams { get; set; } = new List<Parameter>();

        /// <summary>输出图像参数（oo节点）</summary>
        public List<Parameter> OutputIconicParams { get; set; } = new List<Parameter>();

        /// <summary>输入控制参数（ic节点）</summary>
        public List<Parameter> InputControlParams { get; set; } = new List<Parameter>();

        /// <summary>输出控制参数（oc节点）</summary>
        public List<Parameter> OutputControlParams { get; set; } = new List<Parameter>();
    }

    /// <summary>
    /// 参数信息
    /// </summary>
    public class Parameter
    {
        /// <summary>参数名</summary>
        public string Name { get; set; }

        /// <summary>基础类型（iconic=图像/ctrl=控制参数）</summary>
        public string BaseType { get; set; }

        /// <summary>维度</summary>
        public int Dimension { get; set; }
    }

    /// <summary>
    /// 代码元素（区分代码行/注释行）
    /// </summary>
    public class CodeElement
    {
        /// <summary>元素类型（Code=代码行，Comment=注释行）</summary>
        public CodeElementType Type { get; set; }

        /// <summary>内容（代码/注释文本）</summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 代码元素类型枚举
    /// </summary>
    public enum CodeElementType
    {
        /// <summary>代码行（l节点）</summary>
        Code,
        /// <summary>注释行（c节点）</summary>
        Comment
    }
}
