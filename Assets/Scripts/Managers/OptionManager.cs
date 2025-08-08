using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Managers
{
    [DefaultExecutionOrder(-50)]
    public class OptionManager : MonoBehaviour, IOptionManager
    {
        [SerializeField] private GameObject optionPanel;

        [Header("Audio")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;

        [Header("Display")]
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        private Resolution[] _resolutions;

        [Header("Confirm")]
        [SerializeField] private Button confirmButton;

        void Awake()
        {
            CoreManager.I.RegisterManager<IOptionManager>(this);
            Init();
            LoadOption();
            HideOptionPanel();
        }

        private void Init()
        {
            // 해상도 옵션 리스트 업데이트
            _resolutions = Screen.resolutions
                .Select(r => new Resolution { width = r.width, height = r.height })
                .Distinct()
                .OrderBy(r => r.width)
                .ToArray();

            resolutionDropdown.options = _resolutions
                .Select(r => new Dropdown.OptionData($"{r.width} x {r.height}"))
                .ToList();

            // UI 이벤트 등록
            masterSlider.onValueChanged.AddListener(v => SetVolume("MasterVolume", v));
            bgmSlider.onValueChanged.AddListener(v => SetVolume("BGMVolume", v));
            sfxSlider.onValueChanged.AddListener(v => SetVolume("SFXVolume", v));

            resolutionDropdown.onValueChanged.AddListener(idx =>
            {
                var res = _resolutions[idx];
                Screen.SetResolution(res.width, res.height, fullscreenToggle.isOn);
            });

            fullscreenToggle.onValueChanged.AddListener(isOn => Screen.fullScreen = isOn);

            confirmButton.onClick.AddListener(SaveOption);
        }

        // 옵션 패널 열기
        public void ShowOptionPanel() => optionPanel.SetActive(true);

        // 옵션 패널 닫기
        public void HideOptionPanel() => optionPanel.SetActive(false);

        // 옵션 값 불러오기
        public void LoadOption()
        {
            // 볼륨
            var m = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            var b = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            var s = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

            masterSlider.value = m;
            bgmSlider.value = b;
            sfxSlider.value = s;

            SetVolume("MasterVolume", m);
            SetVolume("BGMVolume", b);
            SetVolume("SFXVolume", s);

            // 해상도 & 전체화면
            var resIdx = PlayerPrefs.GetInt("ResolutionIndex", _resolutions.Length - 1);
            var isFull = PlayerPrefs.GetInt("FullScreen", 1) == 1;

            resolutionDropdown.value = Mathf.Clamp(resIdx, 0, _resolutions.Length - 1);
            fullscreenToggle.isOn = isFull;

            var res = _resolutions[resolutionDropdown.value];
            Screen.SetResolution(res.width, res.height, isFull);
        }

        // 옵션 값 저장
        public void SaveOption()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
            PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);

            PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
            PlayerPrefs.SetInt("FullScreen", fullscreenToggle.isOn ? 1 : 0);

            PlayerPrefs.Save();
            HideOptionPanel();
        }

        private void SetVolume(string param, float sliderValue)
        {
            var dB = Mathf.Lerp(-80f, 0f, sliderValue);
            audioMixer.SetFloat(param, dB);
        }
    }
}