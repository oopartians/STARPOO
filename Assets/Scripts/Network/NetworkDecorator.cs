using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDecorator
{
    public static string AttachHeader(string header, string message = null)
    {
        StringBuilder builder = new StringBuilder(header);
        builder.Append(":");
        if(message != null)
            builder.Append(message);

        return builder.ToString();
    }
    public struct NetworkMessage
    {
        public string header;
        public string message;
    }
    public static NetworkMessage StringToMessage(string str)
    {
        NetworkMessage nm;
		nm.header = Char.ToString(str[0]);
        nm.message = str.Remove(0,2);

        return nm;
    }
}

