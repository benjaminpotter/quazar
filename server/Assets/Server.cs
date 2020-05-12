using UnityEngine;
using UnityEngine.Assertions;

using Unity.Collections;
using Unity.Networking.Transport;

public class Server : MonoBehaviour
{
    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections;

    void Start ()
    {
        driver = NetworkDriver.Create();
        var endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = 9000;
        if (driver.Bind(endpoint) != 0)
            Debug.Log("Failed to bind to port 9000");
        else
            driver.Listen();

        connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
    }

    void SendWorldFile(NetworkConnection conn)
    {
        byte[] loadedBytes = System.IO.File.ReadAllBytes("world.bin");

        NativeArray<byte> buffer = new NativeArray<byte>(loadedBytes, Allocator.Temp);
        //buffer.CopyFrom(loadedBytes); // causes problems
        

        Debug.Log(buffer);

        var writer = driver.BeginSend(conn);
        writer.WriteBytes(buffer);
        driver.EndSend(writer);
    }

    void Update()
    {
        driver.ScheduleUpdate().Complete();

        // Clean up connections
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }

        // Accept new connections
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
            Debug.Log("Accepted a connection");
        }

        DataStreamReader stream;

        // for every current connection
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
                continue;

            NetworkEvent.Type cmd;

            // any events currently in the buffer
            while ((cmd = driver.PopEventForConnection(connections[i], out stream)) != NetworkEvent.Type.Empty)
            {

                // client sent data
                if (cmd == NetworkEvent.Type.Data)
                {
                    string request = stream.ReadString().ToString();
                    Debug.Log("Recieved request " + request);

                    if (request == "REQUEST|worldfile")
                    {
                        SendWorldFile(connections[i]);
                        //var writer = driver.BeginSend(connections[i]);
                        //writer.WriteString(new NativeString64("this is a world file"));
                        //driver.EndSend(writer);

                        Debug.Log("Sending world files to: " + connections[i].InternalId);
                    }

                    
                }

                // client disconnected
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                }
            }
        }
    }

    public void OnDestroy()
    {
        driver.Dispose();
        connections.Dispose();
    }
}
