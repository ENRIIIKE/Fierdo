using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Slime))]
[CanEditMultipleObjects]
public class SlimeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Slime slimeScript = target as Slime;

        base.OnInspectorGUI();

        if (GUILayout.Button("MoveToTarget"))
        {
            slimeScript.Move();
        }
    }
}
