using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LocalisationManager localisationManager;
    public SettingsManager settingsManager;
    public TimeManager timeManager;
    public InstancesManager instancesManager;
    public GameSavesManager gameSavesManager;
    public CraftingManager craftingManager;

    public Player player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        localisationManager = GetComponent<LocalisationManager>();
        settingsManager = GetComponent<SettingsManager>();
        timeManager = GetComponent<TimeManager>();
        instancesManager = GetComponent<InstancesManager>();
        gameSavesManager = GetComponent<GameSavesManager>();
        craftingManager = GetComponent<CraftingManager>();

        Init();
    }

    private void Init()
    {
        SaveSystem.Init();
        localisationManager.Init();
        settingsManager.Init();
        craftingManager.Init();
    }

    public void StartNewGame()
    {
        timeManager.StartTime();
    }

    public void LoadGame()
    {
        gameSavesManager.LoadSave();
        timeManager.StartTime();
    }

    public void SaveGame()
    {
        gameSavesManager.SaveGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
