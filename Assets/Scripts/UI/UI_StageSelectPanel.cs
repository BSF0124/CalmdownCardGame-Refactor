using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageSelectPanel : MonoBehaviour
{
    [Header("Buttons")]
    public Button nextWorldButton;
    public Button previousWorldButton;
    public Button backButton;

    [Header("Stage Layout")]
    public Transform stageLayoutParent;
    public GameObject stagePrefab;

    [Header("Config")]
    public int[] stagesPerWorld = { 3, 4, 5, 5, 3 };
    private int currentWorld = 0;

    private void Start()
    {
        UpdateNavButtons();
        PopulateStageLayout();
    }

    public void NextWorld()
    {
        currentWorld++;
        UpdateNavButtons();
        PopulateStageLayout();
    }

    public void PreviousWorld()
    {
        currentWorld--;
        UpdateNavButtons();
        PopulateStageLayout();
    }

    private void UpdateNavButtons()
    {
        previousWorldButton.gameObject.SetActive(currentWorld > 0);
        nextWorldButton.gameObject.SetActive(currentWorld < stagesPerWorld.Length - 1);
    }

    private void PopulateStageLayout()
    {
        foreach (Transform t in stageLayoutParent) Destroy(t.gameObject);

        int count = stagesPerWorld[currentWorld];
        int baseStageID = stagesPerWorld.Take(currentWorld).Sum();
        for (int i = 1; i <= count; i++)
        {
            var go = Instantiate(stagePrefab, stageLayoutParent);
            var icon = go.GetComponent<UI_StageIcon>();
            int stageID = baseStageID + i;
            icon.SetStage(stageID, StageManager.Instance.CanPlayStage(stageID));
            icon.button.onClick.AddListener(() => OnStageSelected(stageID));
        }
    }

    private void OnStageSelected(int stageID)
    {
        if (StageManager.Instance.CanPlayStage(stageID))
            SceneLoader.Instance.LoadScene(Enums.SceneType.Dual);
        else
            Debug.Log("Locked.");
    }

    public void OnBackButton()
    {
        UIManager.Instance.HidePanel(gameObject.name);
    }
}
