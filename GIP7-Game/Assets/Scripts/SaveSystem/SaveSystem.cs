using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class SaveSystem : ISave
{
    private static string defaultPath = "/SaveData/";
    private static string defaultFileName = "data.dat";

    /*
    private static SaveSystem instance = null;

    
    /// <summary>
    /// Gets the singleton of the SaveSystem.
    /// </summary>
    public static SaveSystem Instance { get { return instance = (instance == null) ? new SaveSystem() : instance; } } */

    public int ItemCount { get { return objects.Count; } }

    private List<object> objects;

    public SaveSystem()
    {
        objects = new List<object>();
    }

    public SaveSystem(List<object> objects)
    {
        this.objects = objects;
    }

    /// <summary>
    /// Adds the given object to the save buffer if it's serializable.
    /// </summary>
    /// <param name="element">The object you want to save.</param>
    public void Add(object element)
    {
        if (element.IsSerializable())
        {
            objects.Add(element);
        }
        else
        {
            throw new SerializationException("The given object is not serializable");
        }
    }

    /// <summary>
    /// Instead of adding the list to the save buffer add each object in the list individually to the save buffer.
    /// </summary>
    /// <param name="elements">The list of objects you want to add individually</param>
    public void AddObjectsIndivually(List<object> elements)
    {
        foreach (object o in elements)
        {
            if (o.IsSerializable())
            {
                objects.Add(o);
            }
            else
            {
                throw new SerializationException("The given object is not serializable");
            }
        }
    }

    /// <summary>
    /// Works the same as replace <see cref="Replace(object)"/> excepts this method give you control of the add when the object you want to replace doesn't exist.
    /// </summary>
    /// <param name="element">The object you want to replace.</param>
    /// <param name="addWhenNotInBuffer">Wether you want to add the object to the buffer or not when it hasn't been found in the buffer.</param>
    public void Replace(object element, bool addWhenNotInBuffer = true)
    {
        bool replaced = false;
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].Equals(element))
            {
                objects[i] = element;
                replaced = true;
            }
        }

        if (!replaced && addWhenNotInBuffer) Add(element);
    }

    /// <summary>
    /// Checks if the given object exists within the save buffer.
    /// </summary>
    /// <param name="element">The object you want to check.</param>
    /// <returns>Wether the object exists within the save buffer.</returns>
    public bool Exists(object element)
    {
        foreach (object o in objects)
        {
            if (o.Equals(element))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks wether an object of the given type exists within the save buffer.
    /// </summary>
    /// <typeparam name="T">The type you want to check.</typeparam>
    /// <returns>Wether an object exists within the buffer.</returns>
    public bool Exists<T>()
    {
        foreach (object o in objects)
        {
            if (o.GetType() == typeof(T))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if multiple objects of the same type exist within the save buffer.
    /// </summary>
    /// <typeparam name="T">The type you want to check.</typeparam>
    /// <returns>Wether the buffer has multiple objects of the given type.</returns>
    public bool MultipleExists<T>()
    {
        bool found = false;
        foreach (object o in objects)
        {
            if (o.GetType() == typeof(T))
            {
                if (found)
                {
                    return true;
                }
                else
                {
                    found = true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Removes the given object from the save buffer.
    /// </summary>
    /// <param name="element">The object you want to remove.</param>
    public void Remove(object element)
    {
        objects.Remove(element);
    }

    /// <summary>
    /// Clears the buffer.
    /// </summary>
    public void Clear()
    {
        this.objects.Clear();
    }

    /// <summary>
    /// Saves the objects from the save buffer to a file.
    /// </summary>
    public void Save()
    {
        Save(defaultFileName);
    }

    /// <summary>
    /// Saves the buffer of objects to a file with the given filename.
    /// </summary>
    /// <param name="fileName">The name of the file you want to save the objects to.</param>
    public void Save(string fileName)
    {
        string path = Application.persistentDataPath + defaultPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += fileName;

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fs = new FileStream(path, FileMode.CreateNew);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, objects);
        fs.Close();
    }

    /// <summary>
    /// Loads all the objects from a serialized file.
    /// </summary>
    public void Load()
    {
        Load(defaultFileName);
    }

    /// <summary>
    /// Loads all objects from the given FileName.
    /// </summary>
    /// <param name="FileName">The name of the file you want to load.</param>
    /// <returns>Wether the file has been loaded or not.</returns>
    public bool Load(string fileName)
    {
        string path = Application.persistentDataPath + defaultPath + fileName;
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.OpenRead(path))
            {

                List<object> deserializedObjects = (List<object>)bf.Deserialize(fs);
                foreach (object o in deserializedObjects)
                {
                    objects.Add(o);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the first object with the given type. 
    /// You might want to check for multiple objects of a type before using this unless you are absolutely sure there is nothing in the save buffer.
    /// <see cref="MultipleExists{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetObject<T>()
    {
        foreach (object o in objects)
        {
            if (o.GetType() == typeof(T))
            {
                return (T)o;
            }
        }
        return default(T);
    }

    /// <summary>
    /// Gets a list with objects with a certain type.
    /// </summary>
    /// <typeparam name="T">The type of object you want a list from.</typeparam>
    /// <returns></returns>
    public List<T> GetObjects<T>()
    {
        List<T> types = new List<T>();
        foreach (object o in objects)
        {
            if (o.GetType() == typeof(T))
            {
                types.Add((T)o);
            }
        }
        return types;
    }

    /// <summary>
    /// Gets all the objects currently in the save buffer.
    /// </summary>
    /// <returns>A list with all the objects currently in the save buffer.</returns>
    public List<object> GetObjects()
    {
        return new List<object>(objects);
    }

    /// <summary>
    /// Removes all objects in the object save buffer of a certain type.
    /// </summary>
    /// <typeparam name="T">The type of object you want to delete.</typeparam>
    public void RemoveAll<T>()
    {
        foreach (object o in objects.ToArray())
        {
            if (o.GetType() == typeof(T))
            {
                objects.Remove(o);
            }
        }
    }

    /// <summary>
    /// Returns a set with all the types of objects in the buffer.
    /// </summary>
    /// <returns>The types of objects currently in the buffer.</returns>
    public HashSet<Type> GetTypes()
    {
        HashSet<Type> types = new HashSet<Type>();
        foreach(object o in objects)
        {
            types.Add(o.GetType());
        }
        return types;
    }
}

public static class ExtensionMethods
{
    /// <summary>
    /// Source: http://stackoverflow.com/a/4037838
    /// Checks wether the object is serializable.
    /// </summary>
    /// <param name="obj">The object you want to serialize.</param>
    /// <returns></returns>
    public static bool IsSerializable(this object obj)
    {
        Type t = obj.GetType();

        return Attribute.IsDefined(t, typeof(DataContractAttribute)) || t.IsSerializable || (obj is IXmlSerializable);
    }
}

[System.Serializable]
public class TestData
{
    private int test1;
    private int test2;
    private string test3;
    private float test4;

    public TestData(int test1, int test2, string test3, float test4)
    {
        this.test1 = test1;
        this.test2 = test2;
        this.test3 = test3;
        this.test4 = test4;
    }

    public int Test1
    {
        get
        {
            return test1;
        }

        set
        {
            test1 = value;
        }
    }

    public int Test2
    {
        get
        {
            return test2;
        }

        set
        {
            test2 = value;
        }
    }

    public string Test3
    {
        get
        {
            return test3;
        }

        set
        {
            test3 = value;
        }
    }

    public float Test4
    {
        get
        {
            return test4;
        }

        set
        {
            test4 = value;
        }
    }
}

[System.Serializable]
public class TestData2
{
    private int test1;
    private string test3;
    private float test4;

    public TestData2(int test1, string test3, float test4)
    {
        this.test1 = test1;
        this.test3 = test3;
        this.test4 = test4;
    }

    public int Test1
    {
        get
        {
            return test1;
        }

        set
        {
            test1 = value;
        }
    }

    public string Test3
    {
        get
        {
            return test3;
        }

        set
        {
            test3 = value;
        }
    }

    public float Test4
    {
        get
        {
            return test4;
        }

        set
        {
            test4 = value;
        }
    }
}
