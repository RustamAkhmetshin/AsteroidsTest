using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DBManager : MonoBehaviour, IDBManager
{

    public void SerializeAndSave(List<Level> levels) 
    {
        Stream stream = File.Open(Application.persistentDataPath +  "data.xml", FileMode.Create);
        
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, levels);
        stream.Close();
    }
    
    
    public List<Level> DeserializeAndLoad() 
    {
        if (File.Exists(Application.persistentDataPath +  "data.xml"))
        {
            Stream stream = File.Open(Application.persistentDataPath +  "data.xml", FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            
            List<Level> levels = (List<Level>)formatter.Deserialize(stream);
        
             stream.Close();

            return levels;
        }

        return null;
    }
}
