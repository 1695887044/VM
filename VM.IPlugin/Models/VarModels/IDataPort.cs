using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;
using VM.IPlugin.ModuleEvent;

namespace VM.IPlugin.Models.VarModels
{
    /// <summary>
    /// 端口基础接口（非泛型），用于在节点、连线管理器或 UI 层进行统一装箱和遍历管理
    /// </summary>
    public interface IDataPort
    {
        /// <summary>
        /// 绑定的全路径 (如: "CameraTool1.OutImage")，通常用于序列化/反序列化，或表达式解析
        /// </summary>
        string SourcePath { get; set; }

        /// <summary>
        /// 端口内部唯一名称 (如: "InputImage")，主要用于代码检索和内部逻辑
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 表达式或脚本支持 (如: "Math.Abs(ToolA.Value) + 10")，当端口不直连对象时，可通过表达式计算值
        /// </summary>
        string Expression { get; set; }

        /// <summary>
        /// UI 显示名称 (如: "输入图像")，用于在 WPF 节点上展示
        /// </summary>
        string DisPlayName { get; set; } // 建议拼写修改为 DisplayName

        /// <summary>
        /// 端口备注/说明，可以在 WPF 中绑定为 ToolTip 悬浮提示
        /// </summary>
        string Note { get; set; }

        /// <summary>
        /// 附加用户数据/标签，可用于存储一些动态生成的 UI 状态或拓展配置
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// 变量类型分类枚举值。例如: 255-基类变量, 1-图像, 2-区域, 3-几何模型 等
        /// 用于 UI 连线时的颜色区分，以及连接合法性校验
        /// </summary>
        uint Category { get; set; }


        /// <summary>
        /// 端口真实的数据类型 (System.Type)，用于 UI 层面做类型匹配校验 (防呆)
        /// </summary>
        Type? Type { get; }
    }



}
