using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectArrayScriptableObject", order = 1)]
public class ObjectArrayScriptableObject : ScriptableObject
{
    public GameObject[] objects;
}