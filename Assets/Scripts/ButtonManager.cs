using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// �������� ����� �� ��������
    /// </summary>
    /// <param name="name"></param>
    public async void LoadSceneName(string name)
    {
        await SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }
    /// <summary>
    /// ����� �� ����
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
