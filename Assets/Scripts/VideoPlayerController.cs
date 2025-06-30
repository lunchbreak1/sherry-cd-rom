using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// This class controls the video using a slider.
/// </summary>
public class VideoPlayerController : MonoBehaviour
{
    [Tooltip("The video player")]
    public VideoPlayer videoPlayer;

    [Tooltip("The video play button")]
    public Button playButton;

    [Tooltip("The video pause button")]
    public Button pauseButton;

    [Tooltip("The slider that shows the current time of the video")]
    public Slider videoSlider;

    [Tooltip("The slider that allows the user to change the time of the video.")]
    public Slider interactiveSlider;

    void Start()
    {
        // Button click listeners
        playButton.onClick.AddListener(PlayVideo);
        pauseButton.onClick.AddListener(PauseVideo);

        // Slider drag listeners
        interactiveSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Wait for video to prepare (for correct length)
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    void Update()
    {
        videoSlider.value = (float)(videoPlayer.time / videoPlayer.length);
    }

    /// <summary>
    /// Play the video.
    /// </summary>
    void PlayVideo() => videoPlayer.Play();

    /// <summary>
    /// Pause the video.
    /// </summary>
    void PauseVideo() => videoPlayer.Pause();

    /// <summary>
    /// Change the video's time to the slider's value.
    /// </summary>
    /// <param name="value"></param>
    public void OnSliderValueChanged(float value) => videoPlayer.time = value * videoPlayer.length;

    /// <summary>
    /// Set the slider value to 0.
    /// </summary>
    /// <param name="vp"></param>
    void OnVideoPrepared(VideoPlayer vp)
    {
        videoSlider.minValue = 0;
        videoSlider.maxValue = 1;
        videoSlider.value = 0;
    }

    public void ChangeVideo(VideoClip clip)
    {
        videoPlayer.clip = clip;
    }
}
