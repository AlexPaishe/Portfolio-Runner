using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject FirstScreen;
    public GameObject SecondScreen; 

    private PlayerController _player;
    [SerializeField] private Animator _pause;
    [SerializeField] private Animator _menuPause;

    [SerializeField] private GameObject[] _buttoms;

    public bool go;
    public bool openPause;
    private bool _isPlay = false;

    private void Awake()
    {
        int news = PlayerPrefs.GetInt("News");
        if (news == 0)
        {
            PlayerPrefs.SetInt("News", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(news == 1)
        {
            Time.timeScale = 0.001f;
            FirstScreen.SetActive(false);
            SecondScreen.SetActive(true);
            _buttoms[0].SetActive(false);
        }
        else if(news == 2)
        {
            Time.timeScale = 1;
            _isPlay = true;
            _buttoms[0].SetActive(true);
            _buttoms[1].SetActive(false);
            FirstScreen.SetActive(true);
            SecondScreen.SetActive(false);
        }
    }

    private void Start()
    {
        go = true;
        openPause = true;
        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (_isPlay == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && go == false && _player.go == true && openPause == false)
            {
                openPause = true;
                Time.timeScale = 0.001f;
                _pause.SetTrigger("Close");
            }

            if (Input.GetKeyDown(KeyCode.Escape) && go == true && _player.go == true && openPause == false)
            {
                openPause = true;
                _menuPause.SetTrigger("Close");
            }

            if (_player.go == false && go == true)
            {
                _menuPause.SetTrigger("Close");
            }

            if (_player.go == false && go == false && openPause == false)
            {
                openPause = true;
                _pause.SetTrigger("Death");
            }
        }
    }

    /// <summary>
    /// Реализация возвращение в игру
    /// </summary>
    public void Return()
    {
        openPause = true;
        go = true;
        _menuPause.SetTrigger("Close");
    }

    /// <summary>
    /// Реализация кнопки включения игры
    /// </summary>
    public void PlayButton()
    {
        go = true;
        openPause = true;
        _menuPause.SetTrigger("Close");
        _buttoms[0].SetActive(true);
        _buttoms[1].SetActive(false);
        _isPlay = true;
        PlayerPrefs.SetInt("News", 2);
    }

    /// <summary>
    /// Реализация перезагрузки уровня
    /// </summary>
    public void Restart()
    {
        PlayerPrefs.SetInt("News", 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("News", 0);
    }
}
