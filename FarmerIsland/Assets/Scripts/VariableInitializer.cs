using UnityEngine;

public class VariableInitializer : MonoBehaviour
{
    [SerializeField] private FloatVariable[] FloatVariables = null;
    [SerializeField] private IntVariable[] IntVariables = null;

    private void Awake() => Initialize();

    [ContextMenu("Initial")]
    private void Initialize()
    {
        foreach (FloatVariable floatVar in FloatVariables) floatVar.Initialize();
        foreach (IntVariable intVar in IntVariables) intVar.Initialize();
    }
}