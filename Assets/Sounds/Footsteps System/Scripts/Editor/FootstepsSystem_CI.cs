using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(FootstepsSystem))]

public class FootstepsSystem_CI : Editor
{
    SerializedProperty audioLists, 
    footstepsInterval,
    groundLayer,
    downwardRayLength,
    defaultTag;

    void OnEnable(){
        audioLists = serializedObject.FindProperty("audioLists");
        footstepsInterval = serializedObject.FindProperty("footstepsInterval");
        groundLayer = serializedObject.FindProperty("groundLayer");
        downwardRayLength = serializedObject.FindProperty("downwardRayLength");
        defaultTag = serializedObject.FindProperty("defaultTag");
    }

    public override void OnInspectorGUI(){
        var button = GUILayout.Button(Resources.Load("FootstepsSystemArtwork") as Texture, GUILayout.Width(355), GUILayout.Height(200));
        EditorGUILayout.HelpBox("Don't forget to leave a nice review if you like this tool. It helps alot! Press on the image to get to the store page.", MessageType.Info);
        if (button) Application.OpenURL("http://u3d.as/1Tgk");

        FootstepsSystem script = (FootstepsSystem) target;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Surface Audios & Footsteps", EditorStyles.boldLabel);
        //Audio Lists
        EditorGUILayout.PropertyField(audioLists, new GUIContent("Audio Lists", "Add walkable surface tags with their respective audios for each foot"));
        //Footstep Interval
        EditorGUILayout.PropertyField(footstepsInterval, new GUIContent("Footsteps Interval", "The amount of time between each footstep sound"));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Ground Detection", EditorStyles.boldLabel);
        //Ground Layer
        EditorGUILayout.PropertyField(groundLayer, new GUIContent("Ground Layer", "The layer of the grounds that the ray can hit"));
        //Downward Ray Length
        EditorGUILayout.PropertyField(downwardRayLength, new GUIContent("Downward Ray Length", "The length of the downward ray that identifies the ground type"));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Fallback", EditorStyles.boldLabel);
        //Default Tag
        EditorGUILayout.PropertyField(defaultTag, new GUIContent("Default Tag", "If character walks over an undefined tag surface (not added), will play the default surface"));

        serializedObject.ApplyModifiedProperties();
    }
}
