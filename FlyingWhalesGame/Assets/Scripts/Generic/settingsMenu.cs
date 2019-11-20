using UnityEngine;
using UnityEngine.Audio;

public class settingsMenu : MonoBehaviour
{
	public AudioMixer audioMix;

	public void SetVolumeMaster(float volume)
	{
		audioMix.SetFloat("masterVolume", volume);
	}
	public void SetVolumeBGM(float volume)
	{
		audioMix.SetFloat("BGMVolume", volume);
	}
	public void SetVolumeSFX(float volume)
	{
		audioMix.SetFloat("SFXVolume", volume);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
}
