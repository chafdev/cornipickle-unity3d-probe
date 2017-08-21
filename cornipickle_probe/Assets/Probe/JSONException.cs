using System;
using System.Runtime.Serialization;

[Serializable]
internal class JSONException : Exception
{
    public JSONException()
    {
    }

    public JSONException(string message) : base(message)
    {
    }

    public JSONException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected JSONException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}