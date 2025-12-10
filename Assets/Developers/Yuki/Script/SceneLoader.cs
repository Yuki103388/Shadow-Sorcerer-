using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
   public void Menu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Game()
    {
        SceneManager.LoadScene("Main");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Settings");
    }

}
