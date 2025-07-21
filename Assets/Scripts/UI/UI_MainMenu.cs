using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneLoader.Instance.LoadScene("Game");
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
