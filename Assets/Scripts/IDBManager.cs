
using System.Collections.Generic;

public interface IDBManager
{
    void SerializeAndSave(List<Level> levels);
    List<Level> DeserializeAndLoad();
}
