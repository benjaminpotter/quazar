using UnityEngine;
using UnityEngine.Assertions;

using Unity.Collections;
using Unity.Networking.Transport;

public class Client : MonoBehaviour
{
    public NetworkDriver driver;
    private NetworkConnection connection;
    public bool done;

    bool attemptedConnect;

    public void Connect(string address)
    {
        driver = NetworkDriver.Create();
        connection = default(NetworkConnection);

        // create connection
        var endpoint = NetworkEndPoint.Parse(address, 9000);
        //endpoint.Port = 9000;
        connection = driver.Connect(endpoint);
        attemptedConnect = true;

        Debug.Log("Created connection to server: " + address);
    }

    public void Disconnect()
    {
        done = true;
        connection.Disconnect(driver);
        connection = default(NetworkConnection);
    }

    void Update()
    {
        if (!attemptedConnect)
            return;

        driver.ScheduleUpdate().Complete();

        // verify connection successful
        if (!connection.IsCreated)
        {
            if (!done)
                Debug.Log("Something went wrong during connect");
            return;
        }

        DataStreamReader stream;
        NetworkEvent.Type cmd;

        // while there are events in the buffer
        while ((cmd = connection.PopEvent(driver, out stream)) != NetworkEvent.Type.Empty)
        {
            // if the event is of type connect
            if (cmd == NetworkEvent.Type.Connect)
            {
                Debug.Log("We are now connected to the server");

                // ask for server file
                var writer = driver.BeginSend(connection);
                writer.WriteString(new NativeString64("REQUEST|worldfile"));
                driver.EndSend(writer);
            }

            // if the server has sent data
            else if (cmd == NetworkEvent.Type.Data)
            {
                NativeArray<byte> buffer = new NativeArray<byte>(stream.Length, Allocator.Temp);
                stream.ReadBytes(buffer);

                Debug.Log("Recieved world file");

                GameManager.instance.LoadStarSystem( IOManager.LoadFromByteStream(buffer.ToArray()) );
            }

            // if server closes connection
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from server");
                connection = default(NetworkConnection);
            }
        }
    }

    void OnDestroy()
    {
        driver.Dispose();
    }
}
