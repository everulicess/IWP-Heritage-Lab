using UnityEngine;

[CreateAssetMenu(fileName = "CodexEntry", menuName = "Codex/Entry")]
public class CodexEntry : ScriptableObject
{
    [Space]
    public string title;
    [Space]
    [TextArea(1,5)]
    public string description;
    [Space]
    public Sprite illustration;
}
