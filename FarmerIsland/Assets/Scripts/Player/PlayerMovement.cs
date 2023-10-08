using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0.00f, 10.00f)] private float _moveSpeed = 0.00f;
    private Rigidbody2D _rigidbody2D = null;

    #region Event Registers

    private void OnEnable()
    {
        Joystick.OnDragAction += Move;
    }
    private void OnDisable()
    {
        Joystick.OnDragAction -= Move;
    }

    #endregion


    private void Awake()
    {
        GetComponents();
    }
    private void Start()
    {
        CheckJoystick();
        SetRigidbodyValues();
    }

    private void Update()
    {
        ClampPositionToIslandLimit();
    }

    private void GetComponents()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void CheckJoystick()
    {
        Joystick _joystick = FindObjectOfType<Joystick>();

        //Joystick yok ise hata atýcak.
        Debug.Assert(_joystick, "Add The Joystick In Scene");

#if UNITY_EDITOR
        if (!_joystick)
            EditorApplication.isPlaying = false;
#else
        if (!_joystick)
        Application.Quit();
#endif
    }
    private void SetRigidbodyValues()
    {
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.freezeRotation = true;
    }

    private void Move(Vector2 MoveDirection)
    {
        _rigidbody2D.velocity = _moveSpeed * MoveDirection.normalized;
    }
    private void ClampPositionToIslandLimit()
    {
        Island.Instance.GetIslandBorders(out float HorizontalLimit, out float VerticalLimit);

        float X = HorizontalLimit;
        float Y = VerticalLimit;

        X = Mathf.Clamp(transform.position.x, -X, X);
        Y = Mathf.Clamp(transform.position.y, -Y, Y);

        transform.position = new Vector3(X, Y, transform.position.z);
    }
}