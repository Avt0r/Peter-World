using UnityEngine;

//Класс полки
//На него кладутся захватываемые предметы
[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class Shelf : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //При коллизии с захватываемым объектом помечаем что он на полке
        if (collision.gameObject.TryGetComponent<Draggable>(out var obj))
        {
            obj.OnShelf = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Отключаем метку при выходе
        if (collision.gameObject.TryGetComponent<Draggable>(out var obj))
        {
            obj.OnShelf = false;
        }
    }
}
