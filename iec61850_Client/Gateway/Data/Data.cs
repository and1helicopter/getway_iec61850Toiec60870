using Gateway.Destination;
using Gateway.Source;

namespace Gateway.Data
{
    public class Data:ISource, IDestination
    {
        public Data(Source.Source source, Destination.Destination destination, SourcePath sourcePath, DestinationPath destinationPath)
        {
            Source = source;
            Destination = destination;
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
        }

        public Source.Source GetSource()
        {
            return Source.GetSource();
        }

        public Destination.Destination GetDestination()
        {
            return Destination.GetDestination();
        }

        public Source.Source Source { get; set; }
        public Destination.Destination Destination { get; set; }
        public SourcePath SourcePath { get; set; }
        public DestinationPath DestinationPath { get; set; }
        public dynamic Value { get; set; }

    }

    interface ISource
    {
        
    }

    interface IDestination
    {
        
    }
}