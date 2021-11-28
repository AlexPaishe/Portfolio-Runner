using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject _gameOver;
    private PlayerController _player;
    private ScoreMoney _score;
    private bool _theEnd = false;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _score = FindObjectOfType<ScoreMoney>();
        _gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(_player.go == false && _theEnd == false)
        {
            _theEnd = true;
            StartCoroutine(TheEnd());
        }
    }

    /// <summary>
    /// Реализация окончания игры
    /// </summary>
    /// <returns></returns>
    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.5f);
        _gameOver.SetActive(true);
        _score.theEnd = true;
    }

    /// <summary>
    /// Реализация перезагрузки уровня
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
