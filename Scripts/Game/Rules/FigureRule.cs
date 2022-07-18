using System;
using UnityEngine;

public abstract class FigureRule : MonoBehaviour
{

    protected Directional[] DirectionMove { get; set;}
    protected Directional[] AttackDirection { get; set;}
    protected int maxDistanceMove {get; set;}
    protected int maxDistanceAttack { get; set;}
    public abstract void GetOnField (string cellName);
    protected abstract bool Move(string cellName);
    protected abstract bool Attack(string cellName);


}

