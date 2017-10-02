using Assets.Scripts.Base;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[InitializeOnLoad]
public class ScenesSaver : MonoBehaviour
{
    private static SaveSystem saveSystem = new SaveSystem();
    private static int sceneAmount = 0;

    private const int wait = 300;
    private static int currentwait = 0;

    static ScenesSaver()
    {
        EditorApplication.update += Update;
    }

    static void Update()
    {
        if (!Application.isEditor) return;

        if (++currentwait == wait)
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            if (scenes.Length != sceneAmount)
            {
                SaveScenes();
            }
            currentwait = 0;
        }
    }

    [MenuItem("Scenes/Save")]
    static void SaveScenes()
    {
        List<TinyScene> saveData = new List<TinyScene>();
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        sceneAmount = scenes.Length;
        int j = 0;
        for (int i = 0; i < scenes.Length; ++i)
        {
            if (scenes[i].enabled)
            {
                j++;
                int ind = i;
                saveData.Add(new TinyScene()
                {
                    name = scenes[i].path,
                    index = ind
                });
            }
            else
            {
                Debug.LogWarning("Scene #" + i + " : " + scenes[i].path + " is not enabled\r\nIf you want to be able to load this scene, enable it");
            }
        }
        string filePath = Application.dataPath + "/Resources/" + Files.SCENES_FNAME;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        FileStream fs = new FileStream(filePath, FileMode.CreateNew);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, saveData);
        fs.Close();
        Debug.Log(j + " scenes saved to file");
    }
}
