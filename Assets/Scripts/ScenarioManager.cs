using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



[Serializable, RequireComponent(typeof(AudioSource))]
public class ScenarioManager : MonoBehaviour {

    public int currentView; //Which view is being displayed.
    public ViewManager viewManager; //The list of views written in XML.
    public View view; //The current view being displayed.
    public string scenario; //The XML file from which the views are read.
    public enum Choice {A, B, C }; //Which "path" the player can select when given a choice.
    public Choice choice; //Which choice is currently selected.
    public GameObject Background; //The background.
    public GameObject[] actor = new GameObject [4]; //A list of actors.
    public Animator BackgroundSprite; //The appearance of the background.
    public Animator[] sprite = new Animator[4]; //The appearance of the four characters in the scene and the background.
    public Action[] action = new Action[4]; //A list of actions performed by each actor.
    public Text dialogue, altChoice, thirdChoice; //The message that is displayed in each view and the alternate messages displayed when the player is given a choice.
    public AudioSource audio; //The music playing in the background.
    public Vector3 shakeVector; //An arbitrary vector that's used in the "shake" animation.
    [Range(0, 1)]
    public float walkSpeed, shakeIntensity; //How fast the actors walk, and how intensely they shake.
    [Range(.0001f, 100)]
    public float shakeSpeed; //The frequency at which the actors shake in the "shake" animation.
    public float shakePause { get { return (1 / shakeSpeed); } }
    public bool coloredText; //Should the text display different colors?
    public bool scrollingText; //Is the text set to scroll, or fill automatically?
    [Range(1, 100)]
    public float textSpeed; //How fast the text fills the screen.
    public float textPause { get { return (1 / textSpeed); } } //The time between when each character is printed.
    public bool autoScroll; //Should the game change views automatically?
    [Range(2.5f,60)] public  float autoDelay; //How long the game stays on a view before changing.
    public bool music; //Is the music playing?
    [Range(0, 1)] public float musicVolume; //How loud the music is playing.
    public bool openChoice; //Prevents choice views from automatically transitioning.



    public void Start () //Called when the game starts, sets up all the values and shit.
    { 

        for (int i = 0; i < actor.Length; i++)
        {
            sprite[i] = actor[i].GetComponent<Animator>();
        }

        BackgroundSprite = Background.GetComponent<Animator>();
        dialogue.supportRichText = true;
        audio = GetComponent<AudioSource>();
        shakeVector = Vector3.right;

        loadScenario(scenario);
    }
	
	
	public void Update () { //Called every frame to check from input and changes.

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && currentView < viewManager.views.Count - 1)
            {
                StopAllCoroutines();
                CancelInvoke();
                nextView();
            }

        if(Input.GetKeyDown(KeyCode.Backspace) && currentView > 0)
            {
                StopAllCoroutines();
                CancelInvoke();
                currentView--;
                changeView(currentView);
            }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            CancelInvoke();
            currentView = viewManager.views.Count - 1;
            changeView(currentView);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            CancelInvoke();
            currentView = 0;
            changeView(currentView);
        }

        if (scrollingText) { }
        else
        {
            StopAllCoroutines();
            if (coloredText)
            {
                if (view.color == null)
                {
                    dialogue.text = view.txt;
                }
                else
                {
                    dialogue.text = view.color;
                }
                
            }
            else
            {
                dialogue.text = view.txt;
            }
        }

        if(currentView > viewManager.views.Count)
        {
            currentView = viewManager.views.Count;
        }


        if (view.GetType() == typeof(ChoiceView))
        {
            choiceViewManager((ChoiceView)view);
          //  StartCoroutine(choose((ChoiceView)view));
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            print();
        }
      
        ControlMusic();

    }

    public void FixedUpdate() //Called every second or so to handle animations smoothly, like walking.
    {

        for (int i = 0; i < action.Length; i++)
        {
            setSpecialAction(actor[i], action[i]);
        }    
    }

    public void loadScenario(string path) //Loads either an XML file scenario or a Unity scene.
    {
     //   currentView = 0;

        try
        {
            viewManager = ViewManager.Load(path);

            if(currentView > 0)
            {
                skipToCurrentView();
            }
            else
            {
                changeView(currentView);
            }

            
            
            Debug.Log(path + " loaded");
            scenario = path;
        }
        catch (NullReferenceException exception)
        {
            SceneManager.LoadScene(path);
            Debug.Log(path + " loaded");
        }

        catch (FileNotFoundException exception)
        {
            SceneManager.LoadScene(path);
            Debug.Log(path + " loaded");
        }
    }

    public void loadScenario(string path, int viewNumber) //Loads either an XML file scenario or a Unity scene.
    {
        currentView = viewNumber;

        try
        {
            viewManager = ViewManager.Load(path);
            changeView(currentView);
            Debug.Log(path + " loaded");
            scenario = path;
        }
        catch (NullReferenceException exception)
        {
            SceneManager.LoadScene(path);
            Debug.Log(path + " loaded");
        }

        catch (FileNotFoundException exception)
        {
            SceneManager.LoadScene(path);
            Debug.Log(path + " loaded");
        }
    }

    public void nextView()
    {
        currentView++;
        changeView(currentView);
    }

    

    public virtual void changeView(int viewNumber) //Every time the player clicks, the next "view" is loaded from the view list.
    {

        view = viewManager.views[viewNumber];
        writeActions();

        for(int i = 0; i < sprite.Length; i++)
        {
            sprite[i].Play((string)action[i].sprite);
        }
        Invoke("setActorPositions", .01f);
        BackgroundSprite.Play((string)view.bg);
        displayText((string)view.txt, (string)view.color, (string)view.font);
        altChoice.text = "";
        thirdChoice.text = "";

        openChoice = false;
        choice = Choice.A;
        Invoke("openChoiceView", 1);

        playMusic((string)view.music);

        if (view.transition == null) { }
        else
        {
            loadScenario(view.transition, 0);
        }

        if (autoScroll && currentView < viewManager.views.Count - 1)
        {
            Invoke("nextView", autoDelay);
        }

        

    }

    public void skipToCurrentView()
    {
        for(int i = 0; i <= currentView; i++)
        {
            StopAllCoroutines();
            CancelInvoke();
            changeView(i);
        }
    }




    public void writeActions() //Determines how the actor appears, their location and what they will do in this scene.
    {
        action[0] = new Action(view.actor1, view.start1, view.target1, view.action1);
        action[1] = new Action(view.actor2, view.start2, view.target2, view.action2);
        action[2] = new Action(view.actor3, view.start3, view.target3, view.action3);
        action[3] = new Action(view.actor4, view.start4, view.target4, view.action4);
    }

    public void setActorPositions() //Sets actors to their positions on the x-axis.
    {
        for (int i = 0; i < actor.Length; i++)
        {
            if (action[i].startPoint == null) { }
            else
            {
                Vector3 home = new Vector3(float.Parse(action[i].startPoint), actor[i].transform.position.y, 0);
                actor[i].transform.position = home;
            }
        }
    }

    public void choiceViewManager(ChoiceView choiceView) //When the player needs to make a choice, this special view is displayed.
    {
        dialogue.text = view.txt;
        StopAllCoroutines();
   //     CancelInvoke();
        altChoice.text = choiceView.choice2;
        thirdChoice.text = choiceView.choice3;
        

        if (thirdChoice.text == "") //two choices
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                switch ((int)choice)
                {
                    case 0: choice = Choice.B; break;
                    case 1: choice = Choice.A; break;
                }
            }
        }
        else //three choices
        {
            thirdChoice.text = choiceView.choice3;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                switch ((int)choice)
                {
                    case 0: choice = Choice.C; break;
                    case 1: choice = Choice.A; break;
                    case 2: choice = Choice.B; break;
                }
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                switch ((int)choice)
                {
                    case 0: choice = Choice.B; break;
                    case 1: choice = Choice.C; break;
                    case 2: choice = Choice.A; break;
                }
            }
        }

        switch ((int)choice)
        {
            case 0:
                dialogue.color = Color.yellow;
                dialogue.fontStyle = FontStyle.Bold;

                altChoice.color = Color.white;
                altChoice.fontStyle = FontStyle.Normal;

                thirdChoice.color = Color.white;
                thirdChoice.fontStyle = FontStyle.Normal;
                break;

            case 1:
                dialogue.color = Color.white;
                dialogue.fontStyle = FontStyle.Normal;

                altChoice.color = Color.yellow;
                altChoice.fontStyle = FontStyle.Bold;

                thirdChoice.color = Color.white;
                thirdChoice.fontStyle = FontStyle.Normal;
                break;

            case 2:
                dialogue.color = Color.white;
                dialogue.fontStyle = FontStyle.Normal;

                altChoice.color = Color.white;
                altChoice.fontStyle = FontStyle.Normal;

                thirdChoice.color = Color.yellow;
                thirdChoice.fontStyle = FontStyle.Bold;
                break;

                
        }

        if (openChoice && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            switch ((int)choice)
            {
                case 0: loadScenario(choiceView.scene1, 0); break;
                case 1: loadScenario(choiceView.scene2, 0); break;
                case 2: loadScenario(choiceView.scene3, 0); break;
            }
        }



    }

    public void print()
    {
        Debug.Log(currentView);
    }

    public void openChoiceView()
    {
        openChoice = true;
    }

    public void setSpecialAction(GameObject actor, Action action)
    {
        switch ((int)action.motion)
        {
            case 0: break;
            case 1: StartCoroutine(Shake(actor)); break;
            case 2: StartCoroutine(Move(actor, action.endPoint)); break;
            default: Invoke("setActorPositions", .001f); break;
        }
    }

    public IEnumerator Shake(GameObject actor) //The actor is nervous and shaking.
    {
        actor.transform.position = actor.transform.position + shakeVector * shakeIntensity;
        shakeVector = shakeVector * -1;
        yield return new WaitForSeconds(shakePause);
    }

    public IEnumerator Move(GameObject actor, string str)
    {

        if (str == null) { }
        else
        {
            float target = float.Parse(str);

            if (actor.transform.position.x > target)
            {
                actor.transform.position = actor.transform.position - (Vector3.right * walkSpeed);
            }

            if (actor.transform.position.x < target)
            {
                actor.transform.position = actor.transform.position + (Vector3.right * walkSpeed);
            }
        }

        yield return null;
    }

    public void displayText(string str, string color, string font) //Writes what goes into the scene's text box and sets the font.
    {
        
        if (font == null){}
        else
        {
            dialogue.font = Resources.Load<Font>(font);
            altChoice.font = dialogue.font;
            thirdChoice.font = dialogue.font;
            dialogue.color = Color.white; 
        }

        if (scrollingText)
        {
            StartCoroutine(textFill(str, color));
        }
        else
        {
            if (coloredText)
            {
                if (color == null)
                {
                    dialogue.text = str;
                }
                else
                {
                    dialogue.text = color; 
                }    
            }
            else
            {
                dialogue.text = str;
            }
                
        }

    }


    public IEnumerator textFill(string str, string color) //If the text is set to scroll, this will make the text fill in gradually over time.
    {
        dialogue.text = "";
            if(scrollingText)
            {
                for (int i = 0; i < str.Length + 1; i++)
                {
                    dialogue.text = str.Substring(0, i);      
                    yield return new WaitForSeconds(textPause);
                }
                if(coloredText)
                {
                    if (color == null) { }
                    else
                    {
                        dialogue.text = color;
                    }
                }

            }

    }

    public void playMusic(string str) //This decides which music is played in the background.
    {
        if(str == null){}
        else
        {
            audio.clip = Resources.Load<AudioClip>(str);
        }
         
    }

    public void ControlMusic() //Pauses and unpauses the music accordingly and controls music volume.
    {
        audio.volume = musicVolume;
        if (music)
        {
            if (audio.isPlaying){}
            else
            {
                audio.Play();
            }
        }
        else
        {
            audio.Pause();
        }
    }
}

//TO DO: Modify the SaveData class so you can save option preferences.
