
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameManager : MonoBehaviour
{

    public void LoadShooterScene()
    {
        SceneManager.LoadScene("Alien carnage");
    }

    public void LoadRunnerScene()
    {
        SceneManager.LoadScene("Jurrasic Escape");
    }
}
