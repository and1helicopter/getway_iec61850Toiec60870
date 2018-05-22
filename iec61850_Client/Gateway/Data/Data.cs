using Gateway.Destination;
using Gateway.Source;

namespace Gateway.Data
{
    public class Data:ISource, IDestination
    {
        public Data(Source.Source source, Abstraction.Destination destination, SourcePath sourcePath)
        {
            Source = source;
            Destination = destination;
            SourcePath = sourcePath;
            //DestinationPath = destinationPath;
        }

        public Source.Source GetSource()
        {
            return Source.GetSource();
        }

        //public Abstraction.Destination GetDestination()
        //{
        //    return Destination.GetDestination();
        //}

        public Source.Source Source { get; set; }
        public Abstraction.Destination Destination { get; set; }
        public SourcePath SourcePath { get; set; }
       // public DestinationPath DestinationPath { get; set; }
        public dynamic Value { get; set; }

    }

    interface ISource
    {
        
    }

    interface IDestination
    {
        
    }
}