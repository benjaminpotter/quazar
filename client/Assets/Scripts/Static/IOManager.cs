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

    public static StarSystemData LoadFromByteStream(byte[] stream)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using(MemoryStream ms = new MemoryStream(stream))
        {
            object obj = bf.Deserialize(ms);
            return (StarSystemData) obj;
        }
    }

    public static UnityEngine.GameObject LoadAsset(string filepath)
    {
        return (UnityEngine.GameObject) UnityEngine.Resources.Load(filepath);
    }

    public static UnityEngine.GameObject[] LoadAssets(string filepath)
    {
        UnityEngine.Object[] objects = UnityEngine.Resources.LoadAll(filepath);
        UnityEngine.GameObject[] gameObjects = new UnityEngine.GameObject[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            gameObjects[i] = objects[i] as UnityEngine.GameObject;
        }

        return gameObjects;
    }
}
