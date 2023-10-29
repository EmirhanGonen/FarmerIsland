using System.Drawing;
using System.Drawing.Printing;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Island))]
public class IslandScaler : Editor
{
    private Island _island = null;
    private const float _wpSize = 0.1f;

    private void OnEnable()
    {
        _island = target as Island;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        float zoom = HandleUtility.GetHandleSize(Vector3.zero);

        _island.GetUpAndRightBorders(out Vector2 Up, out Vector2 Right);

        Handles.Label(Right + Vector2.right / 2, "Horizontal", EditorStyles.boldLabel);
        Handles.Label(Up + Vector2.up / 2, "Vertical", EditorStyles.boldLabel);


        Vector2 DirectionX = Handles.Slider2D(new Vector3(Right.x, Right.y), Vector3.forward, Vector3.right, Vector3.up, _wpSize * zoom, Handles.CircleHandleCap, 0.10f);
        float ScaleFactorX = (DirectionX.x - _island.transform.position.x) * 2;

        Vector2 DirectionY = Handles.Slider2D(new Vector3(Up.x, Up.y), Vector3.forward, Vector3.right, Vector3.up, _wpSize * zoom, Handles.CircleHandleCap, 0.10f);
        float ScaleFactorY = (DirectionY.y - _island.transform.position.y) * 2;

        _island.Scale = new Vector2(ScaleFactorX, ScaleFactorY);
    }
}