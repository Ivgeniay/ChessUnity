using UnityEngine;

[CreateAssetMenu(fileName = "Figure", menuName = "Chess/New Figure")]
public class FigureConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private ColorList _color;
    [SerializeField] private Role _role;

    public ColorList Color => _color;
    public Sprite Sprite => this._sprite;
    public Role Role => _role;
}
