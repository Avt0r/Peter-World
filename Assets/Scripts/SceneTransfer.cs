using UnityEngine;
using UnityEngine.SceneManagement;

//����� �������� ����� �������
public class SceneTransfer: MonoBehaviour
{

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
