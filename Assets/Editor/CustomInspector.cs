using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof (ScenarioManager)), CanEditMultipleObjects]
public class CustomInspector : Editor {

    public override void OnInspectorGUI()
    {
      //  base.OnInspectorGUI();
        ScenarioManager scenarioManager = (ScenarioManager)target;

        
        scenarioManager.actor[0] = (GameObject)EditorGUILayout.ObjectField("Actor 1", scenarioManager.actor[0], typeof(GameObject), true);
        scenarioManager.actor[1] = (GameObject)EditorGUILayout.ObjectField("Actor 2",scenarioManager.actor[1], typeof(GameObject));
        scenarioManager.actor[2] = (GameObject)EditorGUILayout.ObjectField("Actor 3", scenarioManager.actor[2], typeof(GameObject), true);
        scenarioManager.actor[3] = (GameObject)EditorGUILayout.ObjectField("Actor 4", scenarioManager.actor[3], typeof(GameObject));

        scenarioManager.Background = (GameObject)EditorGUILayout.ObjectField("Background",scenarioManager.Background, typeof(GameObject));
        scenarioManager.dialogue = (Text)EditorGUILayout.ObjectField("Text Box", scenarioManager.dialogue, typeof(Text));
        scenarioManager.altChoice = (Text)EditorGUILayout.ObjectField("Text Box 2", scenarioManager.altChoice, typeof(Text));
        scenarioManager.thirdChoice = (Text)EditorGUILayout.ObjectField("Text Box 3", scenarioManager.thirdChoice, typeof(Text));
        scenarioManager.scenario = EditorGUILayout.TextField("Scenario", scenarioManager.scenario);
        scenarioManager.currentView = EditorGUILayout.IntField("View", scenarioManager.currentView);
        scenarioManager.walkSpeed = EditorGUILayout.Slider("Walk Speed", scenarioManager.walkSpeed, 0, 1);
        scenarioManager.shakeIntensity = EditorGUILayout.Slider("Shake Intensity", scenarioManager.shakeIntensity, 0, 1);
        scenarioManager.shakeSpeed = EditorGUILayout.Slider("Shake Speed", scenarioManager.shakeSpeed, .0001f, 100);
        scenarioManager.coloredText = EditorGUILayout.Toggle("Colored Text", scenarioManager.coloredText);
        scenarioManager.scrollingText = EditorGUILayout.Toggle("Scrolling Text", scenarioManager.scrollingText);
        if (scenarioManager.scrollingText)
        {
            scenarioManager.textSpeed = EditorGUILayout.Slider("Text Speed", scenarioManager.textSpeed, 1, 100);
        }

        scenarioManager.autoScroll = EditorGUILayout.Toggle("Auto-Scroll", scenarioManager.autoScroll);
        if (scenarioManager.autoScroll)
        {
            scenarioManager.autoDelay = EditorGUILayout.Slider("Auto Delay", scenarioManager.autoDelay, 2.5f, 60);
        }

        scenarioManager.music = EditorGUILayout.Toggle("Music", scenarioManager.music);
        
        if (scenarioManager.music)
        {
            scenarioManager.musicVolume = EditorGUILayout.Slider("Music Volume", scenarioManager.musicVolume, 0, 1);
        }
       
    
    }
}
