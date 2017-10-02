using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave {
    void Add(object element);
    void Remove(object element);
    void RemoveAll<T>();
    bool Exists(object element);
    void Clear();
    void Save();
    void Load();
    T GetObject<T>();
    List<T> GetObjects<T>();
    List<object> GetObjects();
}
