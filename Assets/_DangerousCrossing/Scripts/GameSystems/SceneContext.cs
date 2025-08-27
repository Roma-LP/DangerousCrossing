using System;
using _DangerousCrossing.Scripts.ObstacleLineCore;
using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.UI;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private TouchInputReader _touchInputReader;
    [SerializeField] private ObstacleLineContext _obstacleLineContext;
    [SerializeField] private PlayerSpawnContext _playerSpawnContext;

    public TouchInputReader TouchInputReader => _touchInputReader;

    private void Awake()
    {
        ApplicationSettings();
        Bootstrapper();
    }

    private void Start()
    {
        _obstacleLineContext.Launch();
    }

    private void ApplicationSettings()
    {
        Application.targetFrameRate = 60;
    }

    private void Bootstrapper()
    {
        _obstacleLineContext.Init();
        _playerSpawnContext.Init(TouchInputReader);
    }
}