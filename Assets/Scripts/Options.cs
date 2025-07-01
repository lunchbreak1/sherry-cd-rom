using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Options : MonoBehaviour {

    public GameObject optionsPanel;
    public Toggle textScroll, colorText, autoPlay, music;
    public Slider textSpeed, autoDelay, volume;
    public ScenarioManager scenarioManager;
    public AudioSource audio;

    void Start()
    {
        scenarioManager = gameObject.GetComponent<ScenarioManager>();
        audio = gameObject.GetComponent<AudioSource>();
      //  optionsPanel.active = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if(Input.GetKeyDown(KeyCode.O))
        {
            optionsPanel.active = !optionsPanel.active;
            scenarioManager.enabled = !scenarioManager.enabled;
            
            if (Time.timeScale > 0)
                {
                   Time.timeScale = 0; //ZA WARUDO!! TOKI WO TAMARE!!
                }
                else
                {
                    exitOptions();
                    Time.timeScale = 1; //TIME FLOWS AGAIN!!   
                }
        }
        **/
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)))
        {
            exitOptions();
        }


        scenarioManager.scrollingText = textScroll.isOn;
        scenarioManager.coloredText = colorText.isOn;
        scenarioManager.autoScroll = autoPlay.isOn;
        scenarioManager.music = music.isOn;

        if (music.isOn)
        {
            if (audio.isPlaying) { }
            else
            {
                audio.Play();
            }
        }
        else
        {
            audio.Pause();
        }

        scenarioManager.textSpeed = textSpeed.value;
        scenarioManager.autoDelay = autoDelay.value;
        scenarioManager.musicVolume = volume.value;
        audio.volume = volume.value;
    }

    public void exitOptions()
    {
        if (autoPlay.isOn)
        {
            scenarioManager.Invoke("nextView", scenarioManager.autoDelay);
        }
        else
        {
            scenarioManager.CancelInvoke();
        }
    }
}
