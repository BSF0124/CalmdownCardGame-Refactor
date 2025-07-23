using System;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneLoader.Instance.LoadScene(Enums.SceneType.Stage);
    }

    public void OnSettingButton()
    {
        UIManager.Instance.ShowPanel("SettingPanel");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
