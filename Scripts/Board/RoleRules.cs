using System;
using System.Collections.Generic;
using UnityEngine;

public static class RoleRules
{
    public static bool canMove(Figure figure, string from, string to)
    {
        //Debug.Log($"NAME: {figure.figureConfig.Role} isMooved: {figure.isMooved}");
        Directional dir = directional(from, to);
        if (dir == Directional.none) return false;

        int dist = distance(from, to);

        switch (figure.figureConfig.Role)
        {
            case Role.pawn:
                return pawnRole(figure, dir, dist);
            case Role.King:
                return kingRole(figure, dir, dist);
            case Role.Queen:
                return queenRole(figure, dir, dist);
            case Role.Knight:
                return knightRole(figure, dir, dist);
        }

        return false;
    }

    private static bool kingRole(Figure figure, Directional dir, int dist)
  {
        if (figure.figureConfig.Role != Role.King) return false;
        if (dist == 1) return true;
        return false;
  }
    private static bool pawnRole(Figure figure, Directional dir, int dist)
    {
        if (figure.figureConfig.Role != Role.pawn) return false;
        if(figure.figureConfig.Color == ColorList.White && dir == Directional.North)
        {
            if(figure.isMooved == true)
            {
                if (dist < 2)
                    return true;
            }
            else if (figure.isMooved == false)
            {
                if(dist < 3)
                    return true;
            }
        }
        else if (figure.figureConfig.Color == ColorList.Black && dir == Directional.South)
        {
            if(figure.isMooved)
            {
                if (dist < 2)
                    return true;
            }
            else
            {
                if(dist < 3)
                    return true;
            }
        }
        return false;
    }
    private static bool knightRole(Figure figure, Directional dir, int dist)
    {
        if (dir == Directional.special) return true;
        return false;
    }
    private static bool queenRole(Figure figure, Directional dir, int dist)
    {
        if (figure.figureConfig.Role != Role.Queen) return false;
        if (dir == Directional.none || dir == Directional.special) return false;

        return false;
    }

    private static int distance(string cellFrom, string cellTo)
    {
        int result = default;
        
        if (cellFrom[0] == cellTo[0])
            result = Mathf.Abs(cellFrom[1] - cellTo[1]);

        if (cellFrom[1] == cellTo[1])
            result = Mathf.Abs(cellFrom[0] - cellTo[0]);

        if(Mathf.Abs(cellFrom[0] - cellTo[0]) == Mathf.Abs(cellFrom[1] - cellTo[1]))
            result = Mathf.Abs(cellFrom[0] - cellTo[0]);
        
        Debug.Log($"DISTANCE: {result}");
        return result;
    }

    private static Directional directional(string cellFrom, string cellTo)
    {
        Directional dir = Directional.none;


        if (cellFrom[0] == cellTo[0])
        {
            if (cellFrom[1] < cellTo[1]) dir = Directional.North;
            if (cellFrom[1] > cellTo[1]) dir = Directional.South;
        }

        if (cellFrom[1] == cellTo[1])
        {
            if (cellFrom[0] < cellTo[0]) dir = Directional.East;
            if (cellFrom[0] > cellTo[0]) dir = Directional.West;
        }

        // if(Mathf.Abs(cellFrom[0] - cellTo[0]) == Mathf.Abs(cellFrom[1] - cellTo[1]))
        //     dir = Directional.diagonal;

        // if(Mathf.Abs(cellFrom[0] - cellTo[0]) == cellFrom[1] - cellTo[1])
        //     dir = Directional.NorthEast; //вниз

        // if(cellTo[0] - cellFrom[0] == Mathf.Abs(cellTo[0] - cellFrom[0]))
        //     dir = Directional.NorthEast; //вправо

        // if(Math.Abs(cellFrom[0] - cellTo[0]) == cellTo[1] - cellFrom[1])
        //     dir = Directional.NorthWest; //вверх

        // if(cellFrom[0] - cellTo[0] == Mathf.Abs(cellTo[0] - cellFrom[0]))
        //     dir = Directional.NorthEast; //влево

        if((cellTo[0] - cellFrom[0] == Mathf.Abs(cellTo[0] - cellFrom[0])) 
        && (Math.Abs(cellFrom[0] - cellTo[0]) == cellTo[1] - cellFrom[1]))
            dir = Directional.NorthEast; 

        if((cellTo[0] - cellFrom[0] == Mathf.Abs(cellTo[0] - cellFrom[0])) 
        && (Mathf.Abs(cellFrom[0] - cellTo[0]) == cellFrom[1] - cellTo[1]))
            dir = Directional.SouthEast; 

        if((cellFrom[0] - cellTo[0] == Mathf.Abs(cellTo[0] - cellFrom[0]))
        && (Math.Abs(cellFrom[0] - cellTo[0]) == cellTo[1] - cellFrom[1]))
            dir = Directional.NorthWest;

        if((cellFrom[0] - cellTo[0] == Mathf.Abs(cellTo[0] - cellFrom[0]))
        && (Mathf.Abs(cellFrom[0] - cellTo[0]) == cellFrom[1] - cellTo[1]))
            dir = Directional.SouthWest; 


        if(Mathf.Abs(cellFrom[0] - cellTo[0]) == 2 && Mathf.Abs(cellFrom[1] - cellTo[1]) == 1 ||
            Mathf.Abs(cellFrom[0] - cellTo[0]) == 1 && Mathf.Abs(cellFrom[1] - cellTo[1]) == 2)
            dir = Directional.special;

        if (cellFrom == cellTo)  dir = Directional.none;

        Debug.Log($"DIRECTION: {dir}");
        return dir;
    }




    public static List<Cell> GetAvailableCells(Figure figure)
    {
      var possibleDirections = getListOfDirections(figure);
      int dist = getDefaultDistance(figure);

      var result = new List<Cell>();

      for (int i = 0; i < possibleDirections.Count; i++)
      {
        var list = cellByDirectional(figure, possibleDirections[i], dist);

        foreach(Cell e in list)
          result.Add(e);
      }
      
      return result;
    }

  private static int getDefaultDistance(Figure figure)
  {
    int result = default;
    if (figure.figureConfig.Role == Role.pawn)
      {
        if (figure.isMooved)
          result = 1;
        else if (!figure.isMooved)
          result = 2;
      }

    //Debug.Log($"Distance: {result}");
    return result;
  }

  private static List<Directional> getListOfDirections(Figure figure)
    {
      List<Directional> dir = new List<Directional>();

      if (figure.figureConfig.Role == Role.pawn)
      {
        if (figure.figureConfig.Color == ColorList.White)
        {
          dir.Add(Directional.North);
          dir.Add(Directional.NorthEast);
          dir.Add(Directional.NorthWest);
        }
        else if (figure.figureConfig.Color == ColorList.Black)
        {
          dir.Add(Directional.South);
          dir.Add(Directional.SouthEast);
          dir.Add(Directional.SouthWest);
        }
      }

            
      return dir;
    }

    private static List<Cell> cellByDirectional(Figure figure, Directional dir, int distance)
    {
      List<Cell> cellsList = new List<Cell>();
      var from = figure.transform.parent.name;
      int maxPosition = 1;
      int nextPosition = 1;


      switch (dir)
      {
        case Directional.North:

          if (Convert.ToInt32(from[1].ToString()) + distance > 8) maxPosition = 8;
          else maxPosition = Convert.ToInt32(from[1].ToString()) + distance;

          if (Convert.ToInt32(from[1].ToString()) == 8) nextPosition = 8;
          else nextPosition = Convert.ToInt32(from[1].ToString()) + 1;

          for(int i = nextPosition ; i <= Convert.ToInt32(from[1].ToString()) + distance; i++)
          {
            var nameOfCell = from[0].ToString();
            nameOfCell += i.ToString();

            var go = GameObject.Find(nameOfCell);
            if (go.transform.childCount > 0)
            {
              //если это пешка то поле с фигурой перед ней будет недоступно
              if (figure.figureConfig.Role == Role.pawn) break;
              //для остальных фигур будет доступно это поле

              cellsList.Add(go.GetComponent<Cell>());
              break;
            }
            cellsList.Add(go.GetComponent<Cell>());
          }
          break;

        case Directional.South:

          if (Convert.ToInt32(from[1].ToString()) == 1) nextPosition = 1;
          else nextPosition = Convert.ToInt32(from[1].ToString()) - 1;

          if (Convert.ToInt32(from[1].ToString()) - distance < 1) maxPosition = 1;
          else maxPosition = Convert.ToInt32(from[1].ToString()) - distance;

          for(int i = nextPosition ; i >= Convert.ToInt32(from[1].ToString()) - distance; i--)
          {
            var nameOfCell = from[0].ToString();
            nameOfCell += i.ToString();

            var go = GameObject.Find(nameOfCell);
            if (go.transform.childCount > 0)
            {
              //если это пешка то поле с фигурой перед ней будет недоступно
              if (figure.figureConfig.Role == Role.pawn) break;
              //для остальных фигур будет доступно это поле

              cellsList.Add(go.GetComponent<Cell>());
              break;
            }
            cellsList.Add(go.GetComponent<Cell>());
          }
          break;



          case Directional.NorthEast:
            break;

          case Directional.special:
            break;
      }



      return cellsList;
    }

}
