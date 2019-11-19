using UnityEngine;
public class switchToSettings : MonoBehaviour
{
	public GameObject mainPanel;
	public GameObject settingsPanel;
    
    public void changeToSettings()
	{
		mainPanel.SetActive(false);
		settingsPanel.SetActive(true);
	}

	public void changeToMain()
	{
		mainPanel.SetActive(true);
		settingsPanel.SetActive(false);
	}
}
