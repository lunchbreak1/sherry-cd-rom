using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the audio using play/pause buttons and a slider.
/// </summary>
public class MusicPlayerController : MonoBehaviour
{
    [Tooltip("The audio source component")]
    public AudioSource audioSource;

    [Tooltip("The play button")]
    public Button playButton;

    [Tooltip("The pause button")]
    public Button pauseButton;

    [Tooltip("The slider that shows the current time of the audio")]
    public Slider audioSlider;

    [Tooltip("The slider that allows the user to change the time of the audio")]
    public Slider interactiveSlider;

    void Start()
    {
        // Button click listeners
        playButton.onClick.AddListener(PlayAudio);
        pauseButton.onClick.AddListener(PauseAudio);

        // Slider drag listeners
        interactiveSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Initialize slider
        audioSlider.minValue = 0;
        audioSlider.maxValue = 1;
        audioSlider.value = 0;
    }

    void Update()
    {
        if (audioSource.clip != null && audioSource.isPlaying)
        {
            float normalizedTime = audioSource.time / audioSource.clip.length;
            audioSlider.value = normalizedTime;
        }
    }

    /// <summary>
    /// Play the audio.
    /// </summary>
    void PlayAudio() => audioSource.Play();

    /// <summary>
    /// Pause the audio.
    /// </summary>
    void PauseAudio() => audioSource.Pause();

    /// <summary>
    /// Change the audio's time to the slider's value.
    /// </summary>
    /// <param name="value"></param>
    public void OnSliderValueChanged(float value)
    {
        if (audioSource.clip != null)
        {
            audioSource.time = value * audioSource.clip.length;
        }
    }

    public void ChangeTrack(AudioClip clip)
    {
        audioSource.clip = clip;
        interactiveSlider.value = 0;
        audioSource.time = 0;
        audioSlider.value = 0;
        audioSource.Play();
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
}
