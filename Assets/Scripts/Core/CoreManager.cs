using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoSingleton<CoreManager>
{
    public GameManager game { get; private set; }
    public DeckManager deck { get; private set; }
    public UIManager ui { get; private set; }
    public SceneLoader scene { get; private set; }
    public StageManager stage { get; private set; }
    public InventoryManager inventory { get; private set; }
    public DataManager data { get; private set; }
    public SaveManager save { get; private set; }
    public SettingManager setting { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Init();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    private void Init()
    {
        game = FindAnyObjectByType<GameManager>();
        deck = FindAnyObjectByType<DeckManager>();
        ui = FindAnyObjectByType<UIManager>();
        scene = FindAnyObjectByType<SceneLoader>();
        stage = FindAnyObjectByType<StageManager>();
        inventory = FindAnyObjectByType<InventoryManager>();
        data = FindAnyObjectByType<DataManager>();
        save = FindAnyObjectByType<SaveManager>();
        setting = FindAnyObjectByType<SettingManager>();
    }
}