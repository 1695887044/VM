using Plugin.HalconScript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.HalconScript.Services
{
    public interface IScriptParserService
    {
        HalconProcedure ParseFromFile(string filePath);
        HalconProcedure ParseFromString(string xmlContent);
        string GenerateXml(HalconProcedure procedure);
        void SaveToFile(string filePath, HalconProcedure procedure);
    }
}
