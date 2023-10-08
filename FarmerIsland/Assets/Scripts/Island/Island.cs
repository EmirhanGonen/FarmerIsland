using UnityEditor;
using UnityEngine;

public class Island : Singleton<Island>
{
    [SerializeField] private Vector2 _scale = Vector2.one * 9;
    [SerializeField] private float _limitThreshold = 0.00f;

    [SerializeField] private Color _guiColor = Color.white;
    public Color GUIColor => _guiColor;

    public Vector2 Scale
    {
        get => _scale; set
        {
            _scale = value;
            transform.localScale = _scale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        GetUpAndRightBorders(out Vector2 Up, out Vector2 Right);

        Gizmos.DrawSphere(Up, 0.50f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Right, 0.50f);
    }


    private void Start()
    {
        transform.localScale = _scale;
    }

    public void GetUpAndRightBorders(out Vector2 Up, out Vector2 Right)
    {
        Up = new Vector2(transform.position.x, transform.position.y + (_scale.y / 2.00f));
        Right = new Vector2(transform.position.x + (_scale.x / 2.00f), transform.position.y);
    }
    public void GetIslandBorders(out float HorizontalLimit, out float VerticalLimit, bool IncludeThreshold = true)
    {
        GetUpAndRightBorders(out Vector2 Up, out Vector2 Right);

        // -_limitThreshold pivotdan dolayý objenin yarýsý adanýn dýþýna çýkabiliyordu.

        float _threshold = IncludeThreshold ? _limitThreshold : 0.00f;

        HorizontalLimit = Right.x - _threshold;
        VerticalLimit = Up.y - _threshold;
    }
}