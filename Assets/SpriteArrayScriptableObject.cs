using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpriteArrayScriptableObject", order = 1)]
public class SpriteArrayScriptableObject : ScriptableObject
{
    public Sprite[] sprites;
}