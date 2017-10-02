using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Snapper : MonoBehaviour
{
    static Vector3 prevPosition;
    public const float snapValue = 0.5f;
    static bool doSnap = true;

    static Snapper()
    {
        EditorApplication.update += Update;
    }

    static void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            Snap();
        }
    }

    [MenuItem("Addon/Auto Snap")]
    static void SetSnap()
    {
        doSnap = !doSnap;
        Debug.LogWarning("Snapping function set to: " + doSnap);
    }

    static void Snap()
    {
        if (!doSnap || Selection.transforms.Length == 0) return;

        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            Selection.transforms[i].transform.position = Selection.transforms[i].transform.position.Round();
        }
    }
}

public static class SnapExtensions
{
    public static float Round(this float input)
    {
        return Snapper.snapValue * Mathf.Round((input / Snapper.snapValue));
    }

    public static Vector3 Round(this Vector3 input)
    {
        input.x = input.x.Round();
        input.y = input.y.Round();
        input.z = input.z.Round();
        return input;
    }
}