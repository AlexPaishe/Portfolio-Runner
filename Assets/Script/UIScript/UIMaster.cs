using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    [SerializeField] private PauseMenu UI;

    /// <summary>
    /// Реализация открытия первого экрана
    /// </summary>
    public void CloseFirstScreen()
    {
        UI.FirstScreen.SetActive(false);
        UI.SecondScreen.SetActive(true);
    }

    /// <summary>
    /// Реализация окончания анимации
    /// </summary>
    public void EndAnima()
    {
        UI.openPause = false;
        if(UI.go == false)
        {
            UI.go = true;
        }
        else if (UI.go == true)
        {
            UI.go = false;
        }
    }

    /// <summary>
    /// Реализация зкрытия второго экрана
    /// </summary>
    public void CloseSecondScreen()
    {
        UI.SecondScreen.SetActive(false);
        UI.FirstScreen.SetActive(true);
    }

    /// <summary>
    /// Реализация возвращения в нормальное течение времени
    /// </summary>
    public void NormalizedTime()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Выход в меню
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
