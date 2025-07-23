using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoSingleton<SettingManager>
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Screen")]
    public Resolution[] resolutions;
    public int ResolutionIndex { get; set; }
    public bool IsFullScreen { get; set; }

    public float MasterVolume { get; set; }
    public float BgmVolume { get; set; }
    public float SfxVolume { get; set; }

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // 초기 해상도 리스트 세팅
        resolutions = Screen.resolutions.Distinct().ToArray();
    }

    // 설정 적용
    public void ApplySettings()
    {
        // 1) 해상도 / 전체화면
        var r = resolutions[Mathf.Clamp(ResolutionIndex, 0, resolutions.Length - 1)];
        Screen.SetResolution(r.width, r.height, IsFullScreen);

        // 2) 오디오 볼륨
        // mixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp01(MasterVolume)) * 20f);
        // mixer.SetFloat("BgmVolume", Mathf.Log10(Mathf.Clamp01(BgmVolume)) * 20f);
        // mixer.SetFloat("SfxVolume", Mathf.Log10(Mathf.Clamp01(SfxVolume)) * 20f);
        
    }
}
