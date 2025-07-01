using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StartGame : MonoBehaviour {
    public string path;
    public ScenarioManager scenarioManager;
    public Options options;


    public void NewGame ()
    {
        SceneManager.LoadScene("Scenario1");
	}

    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            

            Debug.Log("Loading" + data.scenario + " " + data.view + " from " + Application.persistentDataPath + path);
            scenarioManager.scenario = data.scenario;
            scenarioManager.currentView = data.view;
            scenarioManager.autoScroll = data.auto;
            scenarioManager.scrollingText = data.text;
            scenarioManager.coloredText = data.color;
            scenarioManager.music = data.music;
            scenarioManager.autoDelay = data.autoDelay;
            scenarioManager.textSpeed = data.textSpeed;
            scenarioManager.musicVolume = data.vol;

            options.autoPlay.isOn = data.auto;
            options.textScroll.isOn = data.text;
            options.colorText.isOn = data.color;
            options.music.isOn = data.music;
            options.autoDelay.value = data.autoDelay;
            options.textSpeed.value = data.textSpeed;
            options.volume.value = data.vol;

            file.Close();


        }
    }

    public void LoadGame(string FilePath)
    {

        if (File.Exists(Application.persistentDataPath + FilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FilePath, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);


            Debug.Log("Loading" + data.scenario + " " + data.view + " from " + Application.persistentDataPath + FilePath);
            scenarioManager.scenario = data.scenario;
            scenarioManager.currentView = data.view;
            scenarioManager.autoScroll = data.auto;
            scenarioManager.scrollingText = data.text;
            scenarioManager.coloredText = data.color;
            scenarioManager.music = data.music;
            scenarioManager.autoDelay = data.autoDelay;
            scenarioManager.textSpeed = data.textSpeed;
            scenarioManager.musicVolume = data.vol;

            options.autoPlay.isOn = data.auto;
            options.textScroll.isOn = data.text;
            options.colorText.isOn = data.color;
            options.music.isOn = data.music;
            options.autoDelay.value = data.autoDelay;
            options.textSpeed.value = data.textSpeed;
            options.volume.value = data.vol;


            file.Close();


        }
    }
}
