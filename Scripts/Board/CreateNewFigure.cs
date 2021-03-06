using UnityEngine;
using MonoSinglentons;

public static class CreateNewFigure
{
    private static GameObject _instance;
    private static GameObject instance {
        get {
            if (_instance == null)
            {
                var go = new GameObject("[EMPTY_OBJECT]");
                _instance = go;
            }
            return _instance;
        }
    }

    
    public static GameObject New(string position, string figure)
    {
        var parentTransform = GameObject.Find(position).transform;
        if (parentTransform.childCount > 0) return null;
        var newFigure = Instant.InstantiateNewGO(instance, parentTransform);
        var script = newFigure.AddComponent<Figure>();
        foreach (var e in ResourcesChess.GetResourses())
        {
            if (e.name == figure) 
            {
                script.Appoint(e);
            }
        }
        
        return newFigure;
    }
}