using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public AudioSource audio;

    void Start()
    {
        // Au démarrage d'une scène, régler le son comme précedemment grâce aux PlayerPrefs

        if(PlayerPrefs.HasKey("MASTERVOLUMESLIDER"))
        {
            float sv = PlayerPrefs.GetFloat("MASTERVOLUMESLIDER");
            masterSlider.value = sv;
            SetMasterLevel(sv);
        }

        if (PlayerPrefs.HasKey("MUSICVOLUMESLIDER"))
        {
            float sv = PlayerPrefs.GetFloat("MUSICVOLUMESLIDER");
            musicSlider.value = sv;
            SetMusicLevel(sv);
        }

        if (PlayerPrefs.HasKey("SFXVOLUMESLIDER"))
        {
            float sv = PlayerPrefs.GetFloat("SFXVOLUMESLIDER");
            sfxSlider.value = sv;
            SetSFXLevel(sv);
        }
    }

    
    public void SetMasterLevel(float sliderValue) // Gérer le son des Master
    {
        float val = Mathf.Log10(sliderValue) * 20;
        PlayerPrefs.SetFloat("MASTERVOLUMESLIDER", sliderValue);
        mainMixer.SetFloat("MasterVol", val);
    }

    public void SetMusicLevel(float sliderValue) // Gérer le son de la musique
    {
        float val = Mathf.Log10(sliderValue) * 20;
        PlayerPrefs.SetFloat("MUSICVOLUMESLIDER", sliderValue);
        mainMixer.SetFloat("MusicVol", val);
    }

    public void SetSFXLevel(float sliderValue) // Gérer le son des SFX (sons bruitages)
    {
        float val = Mathf.Log10(sliderValue) * 20;
        PlayerPrefs.SetFloat("SFXVOLUMESLIDER", sliderValue);
        mainMixer.SetFloat("SFXVol", val);
        if (!audio.isPlaying)
        {
            audio.Play(); // petit son sfx test pour régler le volume
        }
        
    }
}
