using UnityEngine;

//Слайд камеры
public class CameraSlider : MonoBehaviour
{
   
    [SerializeField] private int _x_pos_min, _x_pos_max; //Границы слайда
    [SerializeField] private float _sensitivity; //Чувствительность

    private bool _is_dragging; //Захват
    private Vector2 _start_touch; //Начальная точка на экране после нажатия
    private Vector2 _delta; // Смещение от начальной точки
    private float _start_pos; //Начальная позиция камеры

    public static bool Lock;//Блокировка движения камеры, если мы двигаем Draggable предметы

    private void Start()
    {
        Lock = false;
    }

    private void Update()
    {
        if (Lock) return;

        //ПК
        if (Input.GetMouseButtonDown(0) && !_is_dragging)
        {
            _is_dragging = true;
            _start_touch = Input.mousePosition;
            _start_pos = transform.position.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _is_dragging = false;
        }


        //Смартфон
        if (Input.touches.Length > 0 && !_is_dragging)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _is_dragging = true;
                _start_touch = Input.touches[0].position;
                _start_pos = transform.position.x;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _is_dragging = false;
            }
        }

        //Высчитываем насколько сдвинулся курсор/палец от начальной позиции
        if (_is_dragging)
        {
            if (Input.touches.Length < 0)
            {
                _delta = _start_touch - Input.touches[0].position;
            }
            else if (Input.GetMouseButton(0))
            {
                _delta = _start_touch - (Vector2)Input.mousePosition;
            }
        }
        else
        {
            _delta = Vector2.zero;
        }

        //Двигаем камеру
        if (_delta.magnitude > 0)
        {
            _delta *= _sensitivity;
            
            if (_start_pos + _delta.x < _x_pos_min) _delta.x = _x_pos_min - _start_pos;
            if (_start_pos + _delta.x > _x_pos_max) _delta.x = _x_pos_max - _start_pos;

            transform.position = new Vector3(_start_pos + _delta.x, transform.position.y, transform.position.z);
        }

    }
}
