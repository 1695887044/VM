using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoThemes
{
    public class AxisConfigViewModel
    {
        [Display(Name = "轴名称", Description = "当前轴的唯一标识")]
        public string AxisName { get; set; } = "X-Axis";

        [Display(Name = "使能状态")]
        public bool IsEnabled { get; set; }

        [Display(Name = "运行速度(mm/s)")]
        public double Velocity { get; set; } = 100.0;
    }
    public class LaserParameter
    {
        // --- 分组：基本设置 ---
        [Display(Name = "设备名称", GroupName = "1. 基本设置", Description = "用于标识设备的唯一名称")]
        [ReadOnly(true)] // 只读演示
        public string DeviceName { get; set; } = "Laser_01";

        [Display(Name = "启用激光", GroupName = "1. 基本设置", Description = "总开关，关闭后无法触发")]
        public bool IsEnabled { get; set; } = true;

        // --- 分组：参数微调 ---
        [Display(Name = "功率 (%)", GroupName = "2. 参数微调", Description = "激光输出功率，范围 0-100")]
        public double Power { get; set; } = 85.5;

        [Display(Name = "脉冲宽度 (ms)", GroupName = "2. 参数微调", Description = "单次脉冲的持续时间")]
        public int PulseWidth { get; set; } = 50;

        // --- 分组：其他 ---
        [Display(Name = "调试模式", GroupName = "3. 其他", Description = "开启后会记录详细日志")]
        public bool DebugMode { get; set; }
    }
}
