using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.GetFloat("Volumen", out float value);
        slider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void changeVolume(float volume)
    {
        audioMixer.SetFloat("Volumen", volume);
    }

    public void changeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
