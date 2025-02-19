using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    AudioPlayer audioPlayer;
    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer.GetComponent<AudioSource>().volume = 0.2f;
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene(5);
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadLevel7()
    {
        SceneManager.LoadScene(7);
    }
    public void LoadLevel8()
    {
        SceneManager.LoadScene(8);
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene(9);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
