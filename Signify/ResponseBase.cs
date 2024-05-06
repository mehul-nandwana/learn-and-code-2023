namespace Signify
{
    public class ResponseBase<T>
    {
        public int errorCode;
        public string message;
        public T value;
    }
}
