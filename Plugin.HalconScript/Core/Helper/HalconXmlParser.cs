using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Plugin.HalconScript.Core.Helper
{
        public static class HalconXmlParser
        {
            /// <summary>
            /// 从文件路径解析 Halcon HDevelop XML
            /// </summary>
            /// <param name="filePath">XML文件路径</param>
            /// <returns>解析后的HDevelopFile对象</returns>
            /// <exception cref="ArgumentNullException">路径为空</exception>
            /// <exception cref="System.IO.FileNotFoundException">文件不存在</exception>
            /// <exception cref="XmlException">XML格式错误</exception>
            public static HDevelopFile ParseHalconXml(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath), "XML文件路径不能为空");

                // 加载XML文件
                XDocument doc = XDocument.Load(filePath);
                return ParseHalconXml(doc);
            }

            /// <summary>
            /// 从XML字符串解析 Halcon HDevelop XML
            /// </summary>
            /// <param name="xmlContent">XML字符串</param>
            /// <returns>解析后的HDevelopFile对象</returns>
            /// <exception cref="ArgumentNullException">XML字符串为空</exception>
            /// <exception cref="XmlException">XML格式错误</exception>
            public static HDevelopFile ParseHalconXmlFromString(string xmlContent)
            {
                if (string.IsNullOrEmpty(xmlContent))
                    throw new ArgumentNullException(nameof(xmlContent), "XML字符串不能为空");

                XDocument doc = XDocument.Parse(xmlContent);
                return ParseHalconXml(doc);
            }

            /// <summary>
            /// 内部核心解析逻辑（基于XDocument）
            /// </summary>
            private static HDevelopFile ParseHalconXml(XDocument doc)
            {
                // 根节点 <hdevelop>
                XElement root = doc.Root ?? throw new XmlException("XML根节点<hdevelop>不存在");
                HDevelopFile hdevelopFile = new HDevelopFile
                {
                    FileVersion = root.Attribute("file_version")?.Value ?? string.Empty,
                    HalconVersion = root.Attribute("halcon_version")?.Value ?? string.Empty
                };

                // 解析 <procedure> 节点
                XElement procedureNode = root.Element("procedure");
                if (procedureNode != null)
                {
                    Procedure procedure = new Procedure
                    {
                        Name = procedureNode.Attribute("name")?.Value ?? string.Empty
                    };

                    // 1. 解析接口参数 <interface>
                    ParseInterfaceNode(procedureNode.Element("interface"), procedure.Interface);

                    // 2. 解析过程体 <body>
                    ParseBodyNode(procedureNode.Element("body"), procedure.Body);

                    // 3. 解析文档参数 <docu>
                    ParseDocuNode(procedureNode.Element("docu"), procedure.DocParameterIds);

                    hdevelopFile.Procedure = procedure;
                }

                return hdevelopFile;
            }

            /// <summary>
            /// 解析接口参数节点（io/oo/ic/oc）
            /// </summary>
            private static void ParseInterfaceNode(XElement interfaceNode, ProcedureInterface procedureInterface)
            {
                if (interfaceNode == null) return;

                // 解析输入图像参数（io节点）
                procedureInterface.InputIconicParams = ParseParameterNodes(interfaceNode.Element("io"));
                // 解析输出图像参数（oo节点）
                procedureInterface.OutputIconicParams = ParseParameterNodes(interfaceNode.Element("oo"));
                // 解析输入控制参数（ic节点）
                procedureInterface.InputControlParams = ParseParameterNodes(interfaceNode.Element("ic"));
                // 解析输出控制参数（oc节点）
                procedureInterface.OutputControlParams = ParseParameterNodes(interfaceNode.Element("oc"));
            }

            /// <summary>
            /// 解析参数节点（par）
            /// </summary>
            private static List<Parameter> ParseParameterNodes(XElement parentNode)
            {
                List<Parameter> parameters = new List<Parameter>();
                if (parentNode == null) return parameters;

                // 遍历所有par节点
                foreach (XElement parNode in parentNode.Elements("par"))
                {
                    Parameter param = new Parameter
                    {
                        Name = parNode.Attribute("name")?.Value ?? string.Empty,
                        BaseType = parNode.Attribute("base_type")?.Value ?? string.Empty,
                        // 维度转int，解析失败则设为0
                        Dimension = int.TryParse(parNode.Attribute("dimension")?.Value, out int dim) ? dim : 0
                    };
                    parameters.Add(param);
                }

                return parameters;
            }

            /// <summary>
            /// 解析过程体（l=代码行，c=注释行）
            /// </summary>
            private static void ParseBodyNode(XElement bodyNode, List<CodeElement> bodyElements)
            {
                if (bodyNode == null) return;

                // 遍历body下所有子节点（l/c）
                foreach (XElement node in bodyNode.Elements())
                {
                    CodeElement codeElement = new CodeElement
                    {
                        // 区分节点类型：l=Code，c=Comment
                        Type = node.Name.LocalName == "l" ? CodeElementType.Code : CodeElementType.Comment,
                        // 去除首尾空白，保留代码/注释的原始格式
                        Content = node.Value.Trim()
                    };

                    // 跳过空内容的元素
                    if (!string.IsNullOrEmpty(codeElement.Content))
                    {
                        bodyElements.Add(codeElement);
                    }
                }
            }

            /// <summary>
            /// 解析文档参数节点
            /// </summary>
            private static void ParseDocuNode(XElement docuNode, List<string> docParamIds)
            {
                if (docuNode == null) return;

                // 遍历parameters下的parameter节点
                foreach (XElement paramNode in docuNode.Descendants("parameter"))
                {
                    string paramId = paramNode.Attribute("id")?.Value ?? string.Empty;
                    if (!string.IsNullOrEmpty(paramId))
                    {
                        docParamIds.Add(paramId);
                    }
                }
            }
        }
    
}
