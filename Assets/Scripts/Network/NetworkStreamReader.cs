using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;


public class NetworkStreamReader 
{
    
    string restString;
    
    public static byte[] Read(NetworkStream stream, int size){
        using (var ms = new MemoryStream())
        {
            byte[] part = new byte[size];
            int bytesRead;
            int readed = 0;
            while((bytesRead = stream.Read(part, 0, part.Length)) > 0)
            {
                ms.Write(part, 0, bytesRead);
                readed += bytesRead;
                if(part.Length > bytesRead){
                    break;
                }
                
            }
            byte[] buffer = ms.ToArray();
            
            return buffer;    
        }
        
    }
}