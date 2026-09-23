using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character Data")]
public class CharacterState : ScriptableObject
{
    public string characterName;

    public int maxHp;
    public int attack;
}
