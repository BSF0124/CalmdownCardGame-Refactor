using UnityEngine;
using UnityEngine.UI;

public class UI_StageIcon : MonoBehaviour
{
    public Button button;
    public Image image;
    public GameObject lockOverlay;

    // 아이콘 상태 설정
    public void SetStage(int stageID, bool unlocked)
    {
        lockOverlay.SetActive(!unlocked);
        button.interactable = unlocked;
    }
}
