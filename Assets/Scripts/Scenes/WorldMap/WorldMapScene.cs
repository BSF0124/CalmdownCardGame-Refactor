using Core;
using Managers;
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

        IPlayerDataManager pdm = CoreManager.I.GetManager<IPlayerDataManager>();
        pdm.HighestClearedStage = 20;
        for (int i = 0; i < 30; i++)
        {
            pdm.AddCard(i);
        }
        pdm.SavePlayerData();
    }

    private void OnSelectStageButtonClicked() => selectStagePanel.SetActive(true);
    private void OnCardListButtonClicked()
    {
        CoreManager.I.GetManager<IGameManager>().isStageSelected = false;
        myCardPanel.SetActive(true);
    }
    private void OnNNGButtonClicked()=> nngPanel.SetActive(true);
}
