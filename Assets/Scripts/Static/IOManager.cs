using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class IOManager
{

    public static void Save(StarSystemData data)
    {  
        IFormatter formatter = new BinaryFormatter();  
        Stream stream = new FileStream("world.bin", FileMode.Create, FileAccess.Write, FileShare.None);  
        formatter.Serialize(stream, data);  
        stream.Close(); 
    }

    public static StarSystemData Load()
    {
        IFormatter formatter = new BinaryFormatter();  
        Stream stream = new FileStream("world.bin", FileMode.Open, FileAccess.Read, FileShare.Read);  
        StarSystemData data = (StarSystemData) formatter.Deserialize(stream);  
        stream.Close();

        return data;
    }
}
