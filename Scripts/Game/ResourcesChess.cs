using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ResourcesChess : MonoBehaviour 
{
    [SerializeField] private static IEnumerable<FigureConfig> resources;

    void Awake()
    {
        resources = Resources.LoadAll<FigureConfig>("ScriptableObjects");
    }

    public static IEnumerable<FigureConfig> GetResourses()
    {
        return resources;
    }
}

