using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider[] sliders;

    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Ambient;
    float MasterVolume = 1f;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float AmbientVolume = 0.5f;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/MusicGroup");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/Sfx");
        Ambient = FMODUnity.RuntimeManager.GetBus("bus:/Master/Ambient");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");

        // Imposto i valori iniziali
        Music.setVolume(MusicVolume);
        Ambient.setVolume(AmbientVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);

        // recupero le preferenze
        if (!PlayerPrefs.HasKey("Master"))
            PlayerPrefs.SetFloat("Master", 1);
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetFloat("Music", 0.5f);
        if (!PlayerPrefs.HasKey("SFX"))
            PlayerPrefs.SetFloat("SFX", 0.5f);
        if (!PlayerPrefs.HasKey("Ambient"))
            PlayerPrefs.SetFloat("Ambient", 0.5f);
        MasterVolume = PlayerPrefs.GetFloat("Master");
        MusicVolume = PlayerPrefs.GetFloat("Music");
        SFXVolume = PlayerPrefs.GetFloat("SFX");
        AmbientVolume = PlayerPrefs.GetFloat("Ambient");

        // Imposto la dimensione dello slider
        sliders[0].value = MasterVolume;
        sliders[1].value = MusicVolume;
        sliders[2].value = SFXVolume;
        sliders[3].value = AmbientVolume;
    }

    void Update()
    {
        Music.setVolume(MusicVolume);
        Ambient.setVolume(AmbientVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;

        // Salvo la preferenza
        PlayerPrefs.SetFloat("Master", MasterVolume);

        // Imposto la dimensione dello slider
        sliders[0].value=MasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;

        // Salvo la preferenza
        PlayerPrefs.SetFloat("Music", MusicVolume);

        // Imposto la dimensione dello slider
        sliders[1].value = MusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        // Salvo la preferenza
        PlayerPrefs.SetFloat("SFX", SFXVolume);

        // Imposto la dimensione dello slider
        sliders[2].value = SFXVolume;
    }

    public void AmbientVolumeLevel(float newAmbientVolume)
    {
        AmbientVolume = newAmbientVolume;

        // Salvo la preferenza
        PlayerPrefs.SetFloat("Ambient", AmbientVolume);

        // Imposto la dimensione dello slider
        sliders[3].value = AmbientVolume;
    }
}