using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneManiger : MonoBehaviour
{
    //private audioManiger aManiger;
    // Start is called before the first frame update
    void Start()
    {
        //aManiger = GameObject.FindGameObjectWithTag("audioManiger").GetComponent<audioManiger>();
        Application.targetFrameRate = 100;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void GoBackTOMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ReloadLvl()
    {
        //MainCammra.GetComponent<>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void quitApplication()
    {
        Application.Quit();
    }
}
