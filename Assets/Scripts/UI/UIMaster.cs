using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMaster : MonoBehaviour
{
    //this entire thing could be done better, but this is what the time permitted for now

    [SerializeField] private GameObject GameplayCanvas;
    [SerializeField] private GameObject UICanvas;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject exitPanel;

    [SerializeField] private TMP_Dropdown volleyDD;
    [SerializeField] private TMP_Dropdown misDD;
    [SerializeField] private TMP_Dropdown nukaDD;

    [SerializeField] private TMP_InputField volleyCount;
    [SerializeField] private TMP_InputField misCount;
    [SerializeField] private TMP_InputField nukaCount;

    [SerializeField] private VolleySpawner volleySpawner;
    [SerializeField] private VolleySpawner misSpawner;
    [SerializeField] private VolleySpawner nukaSpawner;

    [SerializeField] private Material volleyMat;
    [SerializeField] private Material misMat;
    [SerializeField] private Material nukaMat;
    
    [SerializeField] private Texture2D redTexture;
    [SerializeField] private Texture2D yellowTexture;
    [SerializeField] private Texture2D blueTexture;


    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        volleyMat.SetTexture("_BaseMap", yellowTexture);
        misMat.SetTexture("_BaseMap", blueTexture);
        nukaMat.SetTexture("_BaseMap", redTexture);
    }

    private void Update()
    {
        ChangeVolley(int.Parse(volleyCount.text), int.Parse(misCount.text), int.Parse(nukaCount.text));

        if(Input.GetKey(KeyCode.Escape))
        {
            if(!isPaused)
            {
                GameplayCanvas.SetActive(false);
                UICanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                GameplayCanvas.SetActive(true);
                UICanvas.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
    }

    public void ExitPause()
    {
        GameplayCanvas.SetActive(true);
        UICanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        infoPanel.SetActive(false);
        settingsPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void ShowExit()
    {
        infoPanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void ChangeMaterials()
    {
        ChangeVolleyMat(volleyDD.value);
        ChangeMisMat(misDD.value);
        ChangeNukaMat(nukaDD.value);

    }

    public void ChangeVolleyMat(int index)
    {
        switch(index)
        {
            case 0:
                volleyMat.SetTexture("_BaseMap", yellowTexture);
                break;
            case 1:
                volleyMat.SetTexture("_BaseMap", blueTexture);
                break;
            case 2:
                volleyMat.SetTexture("_BaseMap", redTexture);
                break;
        }
    }

    public void ChangeMisMat(int index)
    {
        switch (index)
        {
            case 0:
                misMat.SetTexture("_BaseMap", yellowTexture);
                break;
            case 1:
                misMat.SetTexture("_BaseMap", blueTexture);
                break;
            case 2:
                misMat.SetTexture("_BaseMap", redTexture);
                break;
        }
    }

    public void ChangeNukaMat(int index)
    {
        switch (index)
        {
            case 0:
                nukaMat.SetTexture("_BaseMap", yellowTexture);
                break;
            case 1:
                nukaMat.SetTexture("_BaseMap", blueTexture);
                break;
            case 2:
                nukaMat.SetTexture("_BaseMap", redTexture);
                break;
        }
    }

    public void ChangeVolley(int a, int b,int c)
    {
        volleySpawner.volleyCount = a;
        misSpawner.volleyCount = b;
        nukaSpawner.volleyCount = c;
    }

    public void ExitDemo()
    {
        Application.Quit();
    }
}
