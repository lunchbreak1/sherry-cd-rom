using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{

    public ScenarioManager scenarioManager;
    public GameObject menu, mainPanel, savePanel, optionsPanel;
    public string FileName; // This contains the name of the file. Don't add the ".txt"
                            // Assign in inspector
    private TextAsset asset; // Gets assigned through code. Reads the file.
    private TextWriter writer; // This is the writer that writes to the file
    
    void Start()
    {
        scenarioManager = gameObject.GetComponent<ScenarioManager>();
        menu.active = false;
        mainPanel.active = true;
        savePanel.active = false;
        optionsPanel.active = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)))
        {
            menu.active = !menu.active;
            mainPanel.active = true;
            savePanel.active = false;
            optionsPanel.active = false;
            scenarioManager.enabled = !scenarioManager.enabled;

            if (Time.timeScale > 0)
            {
                Time.timeScale = 0; //ZA WARUDO!! TOKI WO TAMARE!!
            }
            else
            {
                Time.timeScale = 1; 
            }
        }
    }

    public void Resume()
    {
        menu.active = false;
        scenarioManager.enabled = true;
        Time.timeScale = 1;
    }

    public void Save()
    {
        string scenario = scenarioManager.scenario;
        int view = scenarioManager.currentView;
        

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FileName);

        SaveData data = new SaveData(scenario, view, scenarioManager.autoScroll,
            scenarioManager.scrollingText, scenarioManager.coloredText, scenarioManager.music,
            scenarioManager.autoDelay, scenarioManager.textSpeed, scenarioManager.musicVolume);

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(scenario + " " + view + " saved.");
    }

    public void Save(string FilePath)
    {
        string scenario = scenarioManager.scenario;
        int view = scenarioManager.currentView;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FilePath);

        SaveData data = new SaveData(scenario, view, scenarioManager.autoScroll, 
            scenarioManager.scrollingText, scenarioManager.coloredText, scenarioManager.music,
            scenarioManager.autoDelay, scenarioManager.textSpeed, scenarioManager.musicVolume);

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(scenario + " " + view + " saved.");
    }

    public void Quit(string titleScreen)
    {
        Time.timeScale = 1;
        scenarioManager.CancelInvoke();
        scenarioManager.StopAllCoroutines();
        SceneManager.LoadScene(titleScreen);
    }


    
}
