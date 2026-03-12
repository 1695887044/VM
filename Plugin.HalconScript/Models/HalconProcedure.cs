using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Plugin.HalconScript.Models
{
    [XmlRoot("procedure")]
    public class HalconProcedure
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "HalconProcedure";

        [XmlArray("interface")]
        [XmlArrayItem("io", typeof(IconicInputList))]
        [XmlArrayItem("oo", typeof(IconicOutputList))]
        [XmlArrayItem("ic", typeof(ControlInputList))]
        [XmlArrayItem("oc", typeof(ControlOutputList))]
        public List<ParameterList> InterfaceParameters { get; set; } = new();

        [XmlIgnore]
        public List<string> IconicInputs => GetParameters<IconicInputList>();
        [XmlIgnore]
        public List<string> IconicOutputs => GetParameters<IconicOutputList>();
        [XmlIgnore]
        public List<string> ControlInputs => GetParameters<ControlInputList>();
        [XmlIgnore]
        public List<string> ControlOutputs => GetParameters<ControlOutputList>();

        [XmlArray("body")]
        [XmlArrayItem("c", typeof(CommentLine))]
        [XmlArrayItem("l", typeof(CodeLine))]
        public List<Line> BodyLines { get; set; } = new();

        [XmlIgnore]
        public string Body => string.Join(Environment.NewLine, BodyLines.Select(l => l.Content));

        private List<string> GetParameters<T>() where T : ParameterList
            => InterfaceParameters.OfType<T>().SelectMany(p => p.Parameters).ToList();

        public void AddIconicInput(string name) => AddParameter<IconicInputList>(name);
        public void AddIconicOutput(string name) => AddParameter<IconicOutputList>(name);
        public void AddControlInput(string name) => AddParameter<ControlInputList>(name);
        public void AddControlOutput(string name) => AddParameter<ControlOutputList>(name);

        private void AddParameter<T>(string name) where T : ParameterList, new()
        {
            var list = InterfaceParameters.OfType<T>().FirstOrDefault() ?? new T();
            if (!InterfaceParameters.Contains(list)) InterfaceParameters.Add(list);
            list.Parameters.Add(name);
        }
    }

    public abstract class ParameterList
    {
        [XmlElement("par")]
        public List<string> Parameters { get; set; } = new();
    }
    public class IconicInputList : ParameterList { }
    public class IconicOutputList : ParameterList { }
    public class ControlInputList : ParameterList { }
    public class ControlOutputList : ParameterList { }

    public abstract class Line
    {
        [XmlText]
        public string Content { get; set; } = "";
    }
    public class CommentLine : Line { }
    public class CodeLine : Line { }
}
