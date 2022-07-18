using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFigureRules
{
    public Directional[] DirectionMove { get; set;}
    public Directional[] AttackDirection { get; set;}
    public int distanceMove {get; set;}
    public bool Move(string cellName);
    public bool Attack(string cellName);

    
}
