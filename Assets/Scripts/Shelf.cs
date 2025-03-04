using UnityEngine;

//����� �����
//�� ���� �������� ������������� ��������
[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class Shelf : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��� �������� � ������������� �������� �������� ��� �� �� �����
        if (collision.gameObject.TryGetComponent<Draggable>(out var obj))
        {
            obj.OnShelf = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //��������� ����� ��� ������
        if (collision.gameObject.TryGetComponent<Draggable>(out var obj))
        {
            obj.OnShelf = false;
        }
    }
}
