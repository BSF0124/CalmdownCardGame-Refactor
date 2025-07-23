using UnityEngine;
using UnityEngine.UI;

public class UI_SettingPanel : MonoBehaviour
{
    [Header("UI Refrences")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle fullScreenToggle;
    public Dropdown resolutionDropdown;
    public Button closeButton;

    private SettingManager settings => SettingManager.Instance;
    private SaveManager save => SaveManager.Instance;
    private UIManager ui => UIManager.Instance;

    private void Awake()
    {
        // 1) 해상도 옵션 채우기
        resolutionDropdown.ClearOptions();
        foreach (var r in settings.resolutions)
            resolutionDropdown.options.Add(
                new Dropdown.OptionData($"{r.width}x{r.height}")
            );

        // 2) 현재 값으로 UI 적용
        masterSlider.value = settings.MasterVolume;
        bgmSlider.value = settings.BgmVolume;
        sfxSlider.value = settings.SfxVolume;
        fullScreenToggle.isOn = settings.IsFullScreen;
        resolutionDropdown.value = settings.ResolutionIndex;

        // 3) 이벤트 연결
        masterSlider.onValueChanged.AddListener(v =>
        {
            settings.MasterVolume = v;
            settings.ApplySettings();
        });
        bgmSlider.onValueChanged.AddListener(v =>
        {
            settings.BgmVolume = v;
            settings.ApplySettings();
        });
        sfxSlider.onValueChanged.AddListener(v =>
        {
            settings.SfxVolume = v;
            settings.ApplySettings();
        });
        fullScreenToggle.onValueChanged.AddListener(b =>
        {
            settings.IsFullScreen = b;
            settings.ApplySettings();
        });
        resolutionDropdown.onValueChanged.AddListener(i =>
        {
            settings.ResolutionIndex = i;
            settings.ApplySettings();
        });
        closeButton.onClick.AddListener(() =>
        {
            save.SaveGame();
            ui.HidePanel(gameObject.name);
        });
    }
}
