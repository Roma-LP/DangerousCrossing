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
    [SerializeField] private InputReaderTouch _inputReaderTouch;
    [SerializeField] private InputReaderJoystick _inputReaderJoystick;
    [SerializeField] private ObstacleLineContext _obstacleLineContext;
    [SerializeField] private PlayerSpawnContext _playerSpawnContext;
    [SerializeField] private DialogService _dialogService;
    [SerializeField] private EnemySpawnContext _enemySpawnContext;
    [SerializeField] private EndObstacleLineZone _endObstacleLineZone;
    [SerializeField] private CamerasContext _camerasContext;

    //public InputReaderTouch InputReaderTouch => _inputReaderTouch;

    private GamePlaySceneHandler _gamePlaySceneHandler;

    private void Awake()
    {
        ApplicationSettings();
        Bootstrapper();
    }

    private void Start()
    {
        _gamePlaySceneHandler.StartGamePlayScene();
        _camerasContext.SetFollowCamera(_playerSpawnContext.PlayerPersonInstance.transform);
    }

    private void ApplicationSettings()
    {
        Application.targetFrameRate = 60;
    }

    private void Bootstrapper()
    {
        _obstacleLineContext.Init();
        _playerSpawnContext.Init(_inputReaderJoystick, _camerasContext.PlayerCamera.transform);
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