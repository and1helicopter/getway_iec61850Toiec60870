namespace Abstraction
{
    public class Data:IData
    {
        public Source Source { get; set; }
        public Destination Destination { get; set; }
        public dynamic Value { get; set; }

        public ItemDestination ItemDestination { get; set; }
        public ItemSource ItemSource { get; set; }
    }

    interface IData
    {
        
    }

}
