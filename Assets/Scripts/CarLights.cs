using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Side
{
    Front, Back
}

[System.Serializable]
public struct Lights
{
    public GameObject lightObjects;
    public Side side;
}

public class CarLights : MonoBehaviour
{
    public List<Lights> lights;
    public Toggle lightToggle;
    bool isFrontLightOn;
    public bool isBackLightOn;
    void Start()
    {
        isBackLightOn = false;
        if (lightToggle == null) return;
        isFrontLightOn = lightToggle.isOn;
    }

    void Update()
    {

    }

    public void FrontLightOn()
    {
        isFrontLightOn = !isFrontLightOn;
        if (isFrontLightOn)
        {
            foreach (Lights item in lights)
            {
                if (item.side == Side.Front && !item.lightObjects.activeInHierarchy)
                    item.lightObjects.SetActive(true);
            }
            lightToggle.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            foreach (Lights item in lights)
            {
                if (item.side == Side.Front && item.lightObjects.activeInHierarchy)
                    item.lightObjects.SetActive(false);
            }
            lightToggle.gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void BackLightOn()
    {
        if (isBackLightOn)
        {
            foreach (Lights item in lights)
            {
                if (item.side == Side.Back && !item.lightObjects.activeInHierarchy)
                    item.lightObjects.SetActive(true);
            }
        }
        else
        {
            foreach (Lights item in lights)
            {
                if (item.side == Side.Back && item.lightObjects.activeInHierarchy)
                    item.lightObjects.SetActive(false);
            }
        }
    }
}
