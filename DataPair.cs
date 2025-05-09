namespace MemoryForge;

public class DataPair // the class to hold our pairs. Primary key ID, key/value relationship, bool for match tracking
{
    public int Id {get; set;}
    public string Key {get; set;}
    public string Value {get; set;}
    public bool Matched {get; set;}

    public DataPair(int id, string key, string value)
    
    {
        Id = id;
        Key = key;
        Value = value;
        Matched = false;
    }
}