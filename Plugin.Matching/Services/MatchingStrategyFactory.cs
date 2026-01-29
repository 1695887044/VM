namespace Plugin.Matching.Services
{
    public enum TemplateMatchingType
    {
        Shape,
        Deformation,
        GrayScale
    }
    public static  class MatchingStrategyFactory
    {
       public static  ITemplateMatchService CreateStrtegy(TemplateMatchingType matchingType)
        {
            return matchingType switch
            {
                TemplateMatchingType.Shape => new ShapeModelService(true),
                TemplateMatchingType.Deformation => new ShapeModelService(),
                TemplateMatchingType.GrayScale => new ShapeModelService(),
                _=> new ShapeModelService()
            };
        }
        public static ITemplateMatchService CreateStrtegy(string matchingType)
        {
            return matchingType switch
            {
                "1" => new ShapeModelService(),
                "2" => new ShapeModelService(),
                "3" => new ShapeModelService(),
                _ => new ShapeModelService()
            };
        }
    }
}
