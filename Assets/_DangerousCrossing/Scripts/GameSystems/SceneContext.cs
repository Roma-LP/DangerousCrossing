using _DangerousCrossing.Scripts.ChestLockCore;
using _DangerousCrossing.Scripts.Enemy;
using _DangerousCrossing.Scripts.GameSystems;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
using _DangerousCrossing.Scripts.ObstacleLineCore;
using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private TouchInputReader _touchInputReader;
    [SerializeField] private ObstacleLineContext _obstacleLineContext;
    [SerializeField] private PlayerSpawnContext _playerSpawnContext;
    [SerializeField] private DialogService _dialogService;
    [SerializeField] private EnemySpawnContext _enemySpawnContext;
    [SerializeField] private EndObstacleLineZone _endObstacleLineZone;

    public TouchInputReader TouchInputReader => _touchInputReader;

    private GamePlaySceneHandler _gamePlaySceneHandler;

    private void Awake()
    {
        ApplicationSettings();
        Bootstrapper();
    }

    private void Start()
    {
        _gamePlaySceneHandler.StartGamePlayScene();
    }

    private void ApplicationSettings()
    {
        Application.targetFrameRate = 60;
    }

    private void Bootstrapper()
    {
        _obstacleLineContext.Init();
        _playerSpawnContext.Init(TouchInputReader);
        _enemySpawnContext.Init(_playerSpawnContext.PlayerPersonInstance);
        _gamePlaySceneHandler =
            new GamePlaySceneHandler(_playerSpawnContext, _obstacleLineContext, _endObstacleLineZone, _enemySpawnContext);
    }

    private void OnDestroy()
    {
        _gamePlaySceneHandler.Dispose();
    }

    [Button]
    private void TestOpen()
    {
        _dialogService.CallDialog(typeof(ChestLockDialog));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TestOpen();
        }
    }
}