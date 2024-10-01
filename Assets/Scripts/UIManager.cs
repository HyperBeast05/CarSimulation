using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] modes;

    int mode=0;
  
    public void SelectionMenu(GameObject item)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        item.SetActive(true);
    }

    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NextMode()
    {
        mode++;
        if (mode > modes.Length - 1)
            mode = 0;
        if(mode==0)
            modes[modes.Length-1].SetActive(false);
        else
            modes[mode-1].gameObject.SetActive(false);
        modes[mode].SetActive(true);
    }

    public void PreviousMode()
    {
        mode--;
        if (mode < 0)
            mode = modes.Length - 1;
        if (mode == modes.Length - 1)
            modes[0].SetActive(false);
        else
            modes[mode + 1].SetActive(false);

        modes[mode].SetActive(true);
    }
    

    public void Back(GameObject item)
    {
        item.SetActive(true);
    }

    public void SelectMode()
    {
        SceneManager.LoadScene(mode+1);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
