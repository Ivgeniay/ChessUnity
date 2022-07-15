using UnityEngine;

[CreateAssetMenu(fileName = "Figure", menuName = "Chess/New Figure")]
public class FigureConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _color;
    [SerializeField] private Role _role;

    public string Color => _color;
    public Sprite Sprite => this._sprite;
    public Role Role => _role;
}
