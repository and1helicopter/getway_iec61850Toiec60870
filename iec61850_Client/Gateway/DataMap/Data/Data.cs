using Gateway.DataMap.Destination;
using Gateway.DataMap.Source;

namespace Gateway.DataMap.Data
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

        private Source.Source Source { get; set; }
        private Destination.Destination Destination { get; set; }
        private SourcePath SourcePath { get; set; }
        private DestinationPath DestinationPath { get; set; }
        private dynamic Value { get; set; }

    }

    interface ISource
    {
        
    }

    interface IDestination
    {
        
    }
}