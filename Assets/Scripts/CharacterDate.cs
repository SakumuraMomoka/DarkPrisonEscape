using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character Data")]
public class CharacterDate : ScriptableObject
{
    public string characterName;

    public int maxHp;
    public int attack;
}
