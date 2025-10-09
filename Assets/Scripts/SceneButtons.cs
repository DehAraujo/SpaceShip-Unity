using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("LoaderScene");
    }
}
