using DG.Tweening;
using Logic.Connection;
using NetCodeTT.Authentication;
using NetCodeTT.Lobby;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] public AudioController AudioController;
    [SerializeField] public WelcomeWindow _welcomeWindow;
    public static ApplicationController Instance { get; private set; }
    public LobbyManager LobbyManager { get; private set; }
    public IAuth AuthenticationManager { get; private set; }
    public IConnection ConnectionManager { get; private set; }
    public GameManager GameManager { get; private set; }

    private bool isEntry = true;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
        BindManagers();
    }

    private void BindManagers()
    {
        ConnectionManager = gameObject.AddComponent<ConnectionManager>();
        AuthenticationManager = new AuthenticationManager();
        LobbyManager = gameObject.AddComponent<LobbyManager>();
        GameManager = gameObject.AddComponent<GameManager>();
    }

    private void Start()
    {
        DOTween.Init();
        ConnectionManager.Init();
        AuthenticationManager.Init();
        GameManager.Init();

        Subscribe();
    }

    private void Subscribe()
    {
        Application.wantsToQuit += OnWantToQuit;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" && isEntry)
        {
            isEntry = false;
            GameManager.onApplicationEntry?.Invoke();
        }
    }

    private bool OnWantToQuit()
    {
        return GameManager.OnWantToQuit();
    }
    
    private void OnApplicationQuit()
    {
        LobbyManager.LeaveLobby();
        StopAllCoroutines();
        DOTween.KillAll();
    }
}