
using HalconDotNet;
using Plugin.Matching.Models;
using System.Collections.ObjectModel;
using VM.Halcon.Models;

namespace Plugin.Matching.Services
{
    public enum TempalteMatchType
    {
        ShapeModel, //形状匹配
        NccModel, //灰度匹配
        LocalDeformable//形变匹配
    }
    public  interface ITemplateMatchService
    {
        DrawingObjectInfo Roi { get; set; }

        public ObservableCollection<TemplateMatchResult> MatchResults {  get; set; }
        Task CreateTemplate(HObject image, HObject hObject);

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="image">图像源</param>
        bool Run(HObject image);

        bool SaveTemplate();

        bool LoadTemplate();
    }
}
