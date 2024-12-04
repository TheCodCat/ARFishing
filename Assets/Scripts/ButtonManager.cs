using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class ButtonManager : MonoBehaviour
{
    public async void LoadSceneName(string name)
    {
        await SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
