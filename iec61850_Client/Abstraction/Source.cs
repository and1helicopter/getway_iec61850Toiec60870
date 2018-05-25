namespace Abstraction
{
    public abstract class Source : ISource
    {
        public abstract bool IsRun { get; set; }

        public abstract dynamic GetValue(Data data);
        public abstract dynamic SetValue(Data data);
        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic ShortInfo();
        public abstract bool InitHandlers();
    }

    public abstract class ItemSource
    {
        public abstract dynamic Path { get; set; }
    }

    public interface ISource
    {
        dynamic GetValue(Data data);
        dynamic SetValue(Data data);            //Объект который изменяет значение и который изменяется
        bool Start();
        bool Stop();
        dynamic ShortInfo();
        bool InitHandlers();
    }
}
