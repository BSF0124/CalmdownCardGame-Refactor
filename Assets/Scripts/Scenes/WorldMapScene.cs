using UnityEngine;
using UnityEngine.UI;

public class WorldMapScene : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button selectStageButton;
    [SerializeField] Button myCardButton;
    [SerializeField] Button nngButton;

    [Header("Panels")]
    [SerializeField] GameObject selectStagePanel;
    [SerializeField] GameObject myCardPanel;
    [SerializeField] GameObject nngPanel;

    void Awake()
    {
        selectStageButton.onClick.AddListener(OnSelectStageButtonClicked);
        myCardButton.onClick.AddListener(OnCardListButtonClicked);
        nngButton.onClick.AddListener(OnNNGButtonClicked);
    }

    private void OnSelectStageButtonClicked() => selectStagePanel.SetActive(true);
    private void OnCardListButtonClicked()=> myCardPanel.SetActive(true);
    private void OnNNGButtonClicked()=> nngPanel.SetActive(true);
}
