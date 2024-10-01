using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playBTN, pauseBTN;
    [SerializeField] CanvasGroup buttons;
    void Start()
    {

    }

    void Update()
    {

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAndPause()
    {
        if (pauseBTN.activeInHierarchy)
        {
            Time.timeScale = 0;
            pauseBTN.SetActive(false);
            playBTN.SetActive(true);
            GameObject.FindGameObjectWithTag("Car").GetComponent<AudioSource>().enabled = false;
            buttons.alpha = 1;
        }
        else if(playBTN.activeInHierarchy)
        {
            Time.timeScale = 1;
            playBTN.SetActive(false);
            pauseBTN.SetActive(true);
            GameObject.FindGameObjectWithTag("Car").GetComponent<AudioSource>().enabled = true;
            buttons.alpha = 0;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
