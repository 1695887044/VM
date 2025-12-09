using HalconDemo.Enums;
using HalconDotNet;

namespace HalconDemo.Models
{
   public  class DrawingObjectInfo
    {


        public DrawingObjectInfo()
        {
                
        }
        public DrawingObjectInfo(DrawShapeType shape , HObject obj , HTuple[] hTuple)
        {
            this.ShapeType = shape;
            this.Hobject = obj;
            this.HTuples = hTuple;
        }
        public DrawShapeType ShapeType { get; set; }

        public HObject Hobject { get; set; }

        public HTuple[] HTuples { get; set; }
    }
}
