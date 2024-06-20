using System;
using System.Collections.Generic;

public class CustomProtocolParameters<T>
{
    public int Size { get; set; }
    public T obj { get; set; }

    public string SourceIp { get; set; }

    public int SourcePort { get; set; }

    public string DestIp { get; set; }

    public int DestPort { get; set; }

    public Dictionary<string, string> Headers { get; set; }

    public string Method { get; set; }
}
