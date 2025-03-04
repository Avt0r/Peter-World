using UnityEngine;

//����� �������������� ��������
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{

    private bool _is_dragging = false; //�������� �������� �� �������
    public bool OnShelf = false; //�������� �� ����� �� �������

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _camera;
    [SerializeField] private Collider2D _drag_collider;

    //��������� ����������� �������� � ������� ����������
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

    //�������� ������� � ����������� ��������
    private void CheckAndMoveIfDrag()
    {
        //��
        if (Input.GetMouseButtonDown(0) && !_is_dragging)
        {
            if (CheckColliderClicked) _is_dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _is_dragging = false;
        }

        //��������
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

        //������� ������� ���� �� � �������
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

    // �������� ��������� ���� � ���������
    private bool CheckColliderClicked => _drag_collider.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
    // �������� ��������� ������ � ���������
    private bool CheckColliderTouched => _drag_collider.OverlapPoint(_camera.ScreenToWorldPoint(Input.touches[0].position));    
}
