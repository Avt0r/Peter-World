using UnityEngine;

//Класс захватываемого предмета
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{

    private bool _is_dragging = false; //Проверка захвачен ли предмет
    public bool OnShelf = false; //Проверка на полке ли предмет

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _camera;
    [SerializeField] private Collider2D _drag_collider;

    //Управляем гравитацией предмета с помощью переменной
    private void FixedUpdate()
    {
        if (!OnShelf)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            _rb.linearVelocityY = 0f;
            _rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        CheckAndMoveIfDrag();
    }

    //Проверка захвата и перемещение предмета
    private void CheckAndMoveIfDrag()
    {
        //ПК
        if (Input.GetMouseButtonDown(0) && !_is_dragging)
        {
            if (CheckColliderClicked) _is_dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _is_dragging = false;
        }

        //Смартфон
        if (Input.touches.Length > 0 && !_is_dragging)
        {
            if (Input.touches[0].phase == TouchPhase.Began && CheckColliderTouched)
            {
                _is_dragging = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _is_dragging = false;
            }
        }

        //Двигаем предмет если он в захвате
        if (_is_dragging)
        {
            CameraSlider.Lock = true;

            if (Input.touches.Length < 0)
            {
                Vector3 pos = _camera.ScreenToWorldPoint(Input.touches[0].position);
                transform.position = new(pos.x, pos.y, transform.position.z);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new(pos.x, pos.y, transform.position.z);
            }
        }
        else
        {
            CameraSlider.Lock = false;
        }
    }

    // Проверка попадания мыши в коллайдер
    private bool CheckColliderClicked => _drag_collider.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
    // Проверка попадания пальца в коллайдер
    private bool CheckColliderTouched => _drag_collider.OverlapPoint(_camera.ScreenToWorldPoint(Input.touches[0].position));    
}
