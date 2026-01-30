using HalconDotNet;
using Microsoft.Win32;
using Plugin.Matching.Models;
using System.Collections.ObjectModel;
using VM.Halcon.Extensions;
using VM.Halcon.Models;

namespace Plugin.Matching.Services
{
    public class ShapeModelService : BindableBase,ITemplateMatchService
    {
        private HTuple modelId;
        private ShapeModelInputParameter templateParameter;
        private ShapeModelRunParameter runParameter;
        private ObservableCollection<TemplateMatchResult> matchResults = new();

        public ObservableCollection<TemplateMatchResult>  MatchResults
        {
            get { return matchResults; }
            set { matchResults= value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 模板参数
        /// </summary>
        public ShapeModelInputParameter TemplateParameter
        {
            get { return templateParameter; }
            set { templateParameter = value; RaisePropertyChanged(); }
        }
        public ShapeModelRunParameter RunParameter
        {
            get { return runParameter; }
            set { runParameter = value; RaisePropertyChanged(); }
        }
        public ShapeModelService(bool SetDefault =true)
        {
            if (SetDefault)
            {
                RunParameter = new();
                RunParameter.ApplyDefaultParameter();
                templateParameter = new();
                templateParameter.ApplyDefaultParameter();
            }
        }
        public DrawingObjectInfo Roi { get; set; }
        public Task CreateTemplate(HObject image, HObject hObject)
        {
            var template = image.ReduceDomain(hObject).CropDomain();
            HOperatorSet.CreateShapeModel(template,
               TemplateParameter.NumLevels,
               TemplateParameter.AngleStart,
               TemplateParameter.AngleExtent,
               TemplateParameter.AngleStep,
               TemplateParameter.Optimization,
               TemplateParameter.Metric,
               TemplateParameter.Contrast,
               TemplateParameter.MinContrast, out  modelId);
            return Task.CompletedTask;
        }

        public bool Run(HObject image)
        {
            MatchResults.Clear();
            if (image == null || modelId == null) return false;
                HObject template;
                template = Roi == null ? image : image.ReduceDomain(Roi.Hobject).CropDomain();
                HOperatorSet.FindShapeModel(
                               template,
                               modelId,
                               RunParameter.AngleStart,
                               RunParameter.AngleExtent,
                               RunParameter.MinScore,
                               RunParameter.NumMatches,
                               RunParameter.MaxOverlap,
                               RunParameter.SubPixel,
                               RunParameter.NumLevels,
                               RunParameter.Greediness,
                               out var hv_Row, out var hv_Column, out var hv_Angle, out var hv_Score);
                HOperatorSet.GetShapeModelContours(out HObject modelContours, modelId, 1);
                for (int i = 0; i < hv_Score.Length; i++)
                {
                    MatchResults.Add(new TemplateMatchResult()
                    {
                        Angle = hv_Angle.DArr[i],
                        Score = hv_Score.DArr[i],
                        Row = hv_Row.DArr[i],
                        Column = hv_Column.DArr[i],
                        Contours = modelContours
                    });
                }
                return true;
          
        }

        public bool SaveTemplate()
        {
            if (modelId == null) return false;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "模板文件|*.shm;*.ncm;*.ldm|所有文件|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                HOperatorSet.WriteShapeModel(modelId, saveFileDialog.FileName);
                return true;
            } 
           
            return false;
        }

        public bool LoadTemplate()
        {
            OpenFileDialog openFileDialog   = new OpenFileDialog();
            openFileDialog.Filter = "Shape Model Files (*.shm)|*.shm|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                HOperatorSet.ReadShapeModel(openFileDialog.FileName, out modelId);
                return true;
            }
           
            return false;
        }
    }
}
