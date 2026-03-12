using HalconDotNet;
using NLog;
using Plugin.HalconScript.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Plugin.HalconScript.Services
{
    public class HalconXmlParserService : IScriptParserService
    {


        

        public HalconProcedure ParseFromFile(string filePath)
        {
            try
            {
                var doc = XDocument.Load(filePath);
                return ParseXDocument(doc);
            }
            catch (Exception ex)
            {
               // _logger.Error($"解析Halcon文件失败: {ex.Message}", ex);
                throw;
            }
        }

        public HalconProcedure ParseFromString(string xmlContent)
        {
            try
            {
                var doc = XDocument.Parse(xmlContent);
                return ParseXDocument(doc);
            }
            catch (Exception ex)
            {
               // _logger.Error($"解析Halcon XML失败: {ex.Message}", ex);
                throw;
            }
        }

        private HalconProcedure ParseXDocument(XDocument doc)
        {

            var procedure = new HalconProcedure();
            var hdevelop = doc.Element("hdevelop");
            var procElem = hdevelop?.Element("procedure");
            if (procElem == null) throw new FormatException("无效的Halcon XML格式");

            procedure.Name = procElem.Attribute("name")?.Value ?? "HalconProcedure";
            // 解析接口参数
            var interfaceElem = doc.Elements("procedure");
            foreach (var item in interfaceElem)
            {
                  var ioElem = item.Element("name");
                    ParseParameters<IconicInputList>(item, "io", procedure);
                    ParseParameters<IconicOutputList>(item, "oo", procedure);
                    ParseParameters<ControlInputList>(item, "ic", procedure);
                    ParseParameters<ControlOutputList>(item, "oc", procedure);
                // 解析代码体
                var bodyElem = item.Element("body");
                if (bodyElem != null)
                {
                    foreach (var lineElem in bodyElem.Elements())
                    {
                        Line line = lineElem.Name.LocalName switch
                        {
                            "c" => new CommentLine { Content = lineElem.Value },
                            "l" => new CodeLine { Content = lineElem.Value },
                            _ => null
                        };
                        if (line != null) procedure.BodyLines.Add(line);
                    }
                }
            }
           

            return procedure;
        }

        private void ParseParameters<T>(XElement interfaceElem, string nodeName, HalconProcedure procedure) where T : ParameterList, new()
        {
            var listElem = interfaceElem.Element(nodeName);
            if (listElem == null) return;

            var list = new T();
            foreach (var parElem in listElem.Elements("par"))
            {
                var name = parElem.Attribute("name")?.Value;
                if (!string.IsNullOrEmpty(name)) list.Parameters.Add(name);
            }
            procedure.InterfaceParameters.Add(list);
        }

        public string GenerateXml(HalconProcedure procedure)
        {
            try
            {
                var doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", null),
                    new XElement("hdevelop",
                        new XAttribute("file_version", "1.1"),
                        new XAttribute("halcon_version", "12.0"),
                        new XElement("procedure",
                            new XAttribute("name", procedure.Name),
                            GenerateInterfaceElement(procedure),
                            GenerateBodyElement(procedure)
                        )
                    )
                );

                using var sw = new StringWriter();
                doc.Save(sw);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                //_logger.Error($"生成Halcon XML失败: {ex.Message}", ex);
                throw;
            }
        }

        private XElement GenerateInterfaceElement(HalconProcedure procedure)
        {
            var interfaceElem = new XElement("interface");
            AddParameterList(interfaceElem, "io", procedure.IconicInputs, "iconic");
            AddParameterList(interfaceElem, "oo", procedure.IconicOutputs, "iconic");
            AddParameterList(interfaceElem, "ic", procedure.ControlInputs, "ctrl");
            AddParameterList(interfaceElem, "oc", procedure.ControlOutputs, "ctrl");
            return interfaceElem;
        }

        private void AddParameterList(XElement parent, string nodeName, List<string> parameters, string baseType)
        {
            if (!parameters.Any()) return;
            var listElem = new XElement(nodeName);
            foreach (var param in parameters)
            {
                listElem.Add(new XElement("par",
                    new XAttribute("name", param),
                    new XAttribute("base_type", baseType),
                    new XAttribute("dimension", "0")
                ));
            }
            parent.Add(listElem);
        }

        private XElement GenerateBodyElement(HalconProcedure procedure)
        {
            var bodyElem = new XElement("body");
            foreach (var line in procedure.BodyLines)
            {
                var nodeName = line is CommentLine ? "c" : "l";
                bodyElem.Add(new XElement(nodeName, line.Content));
            }
            return bodyElem;
        }

        public void SaveToFile(string filePath, HalconProcedure procedure)
        {
            try
            {
                var xml = GenerateXml(procedure);
                File.WriteAllText(filePath, xml, Encoding.UTF8);
                //_logger.Info($"Halcon脚本已保存: {filePath}");
            }
            catch (Exception ex)
            {
              //  _logger.Error($"保存Halcon文件失败: {ex.Message}", ex);
                throw;
            }
        }
    }
}
