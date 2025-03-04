using UnityEngine;
using UnityEngine.SceneManagement;

//Класс перехода между сценами
public class SceneTransfer: MonoBehaviour
{

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
