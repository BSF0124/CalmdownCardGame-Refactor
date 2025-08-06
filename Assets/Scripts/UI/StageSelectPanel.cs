using System.Linq;
using Core;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StageSelectPanel : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] private Button prevWorldButton;
        [SerializeField] private Button nextWorldButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Text worldTitleText;

        [Header("Stage Buttons")]
        [SerializeField] private Button[] stageButtons;


        [Header("World Names")]
        [SerializeField] private string[] worldNames =
        {
            "금병영 스투디오",
            "배도라운지",
            "천하 제일 카드대회",
            "지하철 1호선",
            "OO 학원"
        };
        [Header("Sprites")]
        [SerializeField] private Sprite lockedSprite;
        [SerializeField] private Sprite[] unlockedSprite;
        [SerializeField] private Sprite[] clearedSprite;

        private readonly int[] worldStageCounts = { 3, 4, 5, 5, 3 };

        private int currentWorld = 0;
        private IPlayerDataManager playerDataMgr;
        private ISceneTransitionManager sceneTransMgr;
        private IGameManager gameMgr;

        void Awake()
        {
            playerDataMgr = CoreManager.I.GetManager<IPlayerDataManager>();
            sceneTransMgr = CoreManager.I.GetManager<ISceneTransitionManager>();
            gameMgr = CoreManager.I.GetManager<IGameManager>();

            if (playerDataMgr == null || sceneTransMgr == null)
                Debug.LogError("[StageSelectPanel] Manager not found.");

            prevWorldButton.onClick.AddListener(() => ChangeWorld(-1));
            nextWorldButton.onClick.AddListener(() => ChangeWorld(+1));
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));

            for (int i = 0; i < stageButtons.Length; i++)
            {
                int localIndex = i;
                stageButtons[i].onClick.AddListener(() => OnStageClicked(localIndex));
            }

            UpdatePanel();
        }

        public void OpenPanel()
        {
            gameObject.SetActive(true);
            currentWorld = 0;
            UpdatePanel();
        }

        private void ChangeWorld(int dir)
        {
            currentWorld = Mathf.Clamp(currentWorld + dir, 0, worldStageCounts.Length - 1);
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            int highestCleared = playerDataMgr.HighestClearedStage;
            int stagesInCurrentWorld = worldStageCounts[currentWorld];
            int globalOffset = worldStageCounts.Take(currentWorld).Sum();

            // 월드 이름 설정
            bool firstStageCleared = highestCleared >= globalOffset + 1;
            if(firstStageCleared)
                worldTitleText.text = $"월드 {currentWorld + 1}: {worldNames[currentWorld]}";
            else
                worldTitleText.text = $"월드 {currentWorld + 1}: ???";


            prevWorldButton.interactable = currentWorld > 0;
            nextWorldButton.interactable = currentWorld < worldStageCounts.Length - 1;


            for (int i = 0; i < stageButtons.Length; i++)
            {
                var btnGO = stageButtons[i].gameObject;
                var img = stageButtons[i].GetComponent<Image>();

                if (i < stagesInCurrentWorld)
                {
                    btnGO.SetActive(true);

                    int globalStageNum = globalOffset + i + 1;
                    if (globalStageNum <= highestCleared)
                    {
                        img.sprite = clearedSprite[globalStageNum - 1];
                        stageButtons[i].interactable = true;
                    }
                    else if (globalStageNum == highestCleared + 1)
                    {
                        img.sprite = unlockedSprite[globalStageNum - 1];
                        stageButtons[i].interactable = true;
                    }
                    else
                    {
                        img.sprite = lockedSprite;
                        stageButtons[i].interactable = false;
                    }
                }
                else
                {
                    btnGO.SetActive(false);
                }
            }
        }

        private void OnStageClicked(int localIndex)
        {
            int globalOffset = worldStageCounts.Take(currentWorld).Sum();
            int globalStageNum = globalOffset + localIndex + 1;

            gameMgr.SelectedStage = globalStageNum;
            sceneTransMgr.LoadScene(Enums.SceneType.Dual);
            print(globalStageNum);
        }
    }
}