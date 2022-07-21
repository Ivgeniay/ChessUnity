using System;
using System.Collections.Generic;
using UnityEngine;

public static class RoleRules
{
  public static List<Cell> GetAvailableCells(Figure figure)
  {
    List<Directional> possibleDirections = getListOfDirections(figure);
    int dist = getDefaultDistance(figure);

    List<Cell> result = new List<Cell>();

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

    if(figure.figureConfig.Role == Role.King)
      result = 1;
    if(figure.figureConfig.Role == Role.Queen)
      result = 8;
    if(figure.figureConfig.Role == Role.Rook)
      result = 8;
    if(figure.figureConfig.Role == Role.Bishop)
      result = 8;
    if (figure.figureConfig.Role == Role.pawn)
      {
        if (figure.isMooved)
          result = 1;
        else if (!figure.isMooved)
          result = 2;
      }
    return result;
  }
  private static List<Directional> getListOfDirections(Figure figure)
    {
      List<Directional> dir = new List<Directional>();
      if (figure.figureConfig.Role == Role.King || figure.figureConfig.Role == Role.Queen)
      {
        dir.Add(Directional.North);
        dir.Add(Directional.East);
        dir.Add(Directional.South);
        dir.Add(Directional.West);
        dir.Add(Directional.NorthEast);
        dir.Add(Directional.NorthWest);
        dir.Add(Directional.SouthEast);
        dir.Add(Directional.SouthWest);
      }

      if (figure.figureConfig.Role == Role.Rook)
      {
        dir.Add(Directional.North);
        dir.Add(Directional.East);
        dir.Add(Directional.South);
        dir.Add(Directional.West);
      }
      if (figure.figureConfig.Role == Role.Bishop)
      {
        dir.Add(Directional.NorthEast);
        dir.Add(Directional.NorthWest);
        dir.Add(Directional.SouthEast);
        dir.Add(Directional.SouthWest);
      }
      if (figure.figureConfig.Role == Role.Knight)
      {
        dir.Add(Directional.special);
      }

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

      switch (dir)
      {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        case Directional.North:
          for(int i = 1; i <= distance; i++)
          {
            var newPosition = (Convert.ToChar(from[0])).ToString() + (Convert.ToInt32(from[1].ToString()) + i).ToString();

            if (checkTheCell(newPosition))
            {
              try
              {
                var go = GameObject.Find(newPosition);
                if (go.transform.childCount > 0)
                {
                  //если это пешка то поле с фигурой перед ней будет недоступно
                  if (figure.figureConfig.Role == Role.pawn) break;
                  
                  //для остальных фигур будет доступно это поле, если там враг
                  if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                    break;

                  cellsList.Add(go.GetComponent<Cell>());
                  break;
                }
                cellsList.Add(go.GetComponent<Cell>());
              }
              catch {}
            }
          }
          break;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        case Directional.South:

          for(int i = 1; i <= distance; i++)
          {
            var newPosition = (Convert.ToChar(from[0])).ToString() + (Convert.ToInt32(from[1].ToString()) - i).ToString();

            if (checkTheCell(newPosition))
            {
              try
              {
                var go = GameObject.Find(newPosition);
                if (go.transform.childCount > 0)
                {
                  //если это пешка то поле с фигурой перед ней будет недоступно
                  if (figure.figureConfig.Role == Role.pawn) 
                    break;

                  //для остальных фигур будет доступно это поле, если там враг
                  if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                    break;

                  cellsList.Add(go.GetComponent<Cell>());
                  break;
                }
                cellsList.Add(go.GetComponent<Cell>());
              }
              catch{}
            }

          }
          break;


        ///////////////////////////////////////////////////////////////////////////////////////////////////
        case Directional.East:
        
        if (figure.figureConfig.Role == Role.King && figure.isInGame && !figure.isMooved)
        {
          distance = 3;

          for(int i = 1; i <= distance; i++)
            {
              var newPosition = (Convert.ToChar(from[0] + i)).ToString() + (Convert.ToInt32(from[1].ToString())).ToString();
              if (checkTheCell(newPosition))
              {
                try
                {
                  GameObject go = GameObject.Find(newPosition);
                  if (go.transform.childCount > 0)
                  {
                    var goChildFigure = go.transform.GetChild(0).GetComponent<Figure>();
                    if (goChildFigure.figureConfig.Color == figure.figureConfig.Color
                        && goChildFigure.figureConfig.Role == Role.Rook 
                        && goChildFigure.isMooved == false)
                    {
                      go = GameObject.Find("G" + Convert.ToInt32(from[1].ToString()));
                      cellsList.Add(go.GetComponent<Cell>());
                      go = GameObject.Find("F" + Convert.ToInt32(from[1].ToString()));
                      cellsList.Add(go.GetComponent<Cell>());
                      break;
                    }
                  }
                }
                catch{}
              }
            }

          break;
        }
          
          // if (figure.figureConfig.Role == Role.King && figure.isMooved == false) distance = 3;
          // {
          //   for(int i = 1; i <= distance; i++)
          //   {
          //     var newPosition = (Convert.ToChar(from[0] + i)).ToString() + (Convert.ToInt32(from[1].ToString())).ToString();
          //     if (checkTheCell(newPosition))
          //     {
          //       try
          //       {
          //         GameObject go = GameObject.Find(newPosition);
          //         if (go.transform.childCount > 0)
          //         {
          //           var goChildFigure = go.transform.GetChild(0).GetComponent<Figure>();
          //           if (goChildFigure.figureConfig.Color == figure.figureConfig.Color
          //               && goChildFigure.figureConfig.Role == Role.Rook 
          //               && goChildFigure.isMooved == false)
          //           {
          //             go = GameObject.Find("G" + Convert.ToInt32(from[1].ToString()));
          //             cellsList.Add(go.GetComponent<Cell>());
          //             go = GameObject.Find("F" + Convert.ToInt32(from[1].ToString()));
          //             cellsList.Add(go.GetComponent<Cell>());
          //             break;
          //           }
          //         }
          //       }
          //       catch{}
          //     }
          //   }
          // }

          for(int i = 1; i <= distance; i++)
          {
            var newPosition = (Convert.ToChar(from[0] + i)).ToString() + (Convert.ToInt32(from[1].ToString())).ToString();

            if (checkTheCell(newPosition))
            {
              try
              {
                var go = GameObject.Find(newPosition);
                if (go.transform.childCount > 0)
                {
                  var goChildFigure = go.transform.GetChild(0).GetComponent<Figure>();
                  if (goChildFigure.figureConfig.Color == figure.figureConfig.Color)
                    break;

                  cellsList.Add(go.GetComponent<Cell>());
                  break;
                }
                cellsList.Add(go.GetComponent<Cell>());
              }
              catch{}
            }
          }
          break;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        case Directional.West:
          for(int i = 1; i <= distance; i++)
          {
            var newPosition = (Convert.ToChar(from[0] - i)).ToString() + (Convert.ToInt32(from[1].ToString())).ToString();

            if (checkTheCell(newPosition))
            {
              try
              {
                var go = GameObject.Find(newPosition);
                if (go.transform.childCount > 0)
                {
                  if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                    break;

                  cellsList.Add(go.GetComponent<Cell>());
                  break;
                }
                cellsList.Add(go.GetComponent<Cell>());
              }
              catch{}
            }

          }
          break;


          ///////////////////////////////////////////////////////////////////////////////////////////////////
          case Directional.special:

            string[] points = new string[] {
              Convert.ToChar(from[0] - 2).ToString() + Convert.ToChar(from[1] - 1),
              Convert.ToChar(from[0] - 2).ToString() + Convert.ToChar(from[1] + 1),
              Convert.ToChar(from[0] - 1).ToString() + Convert.ToChar(from[1] - 2),
              Convert.ToChar(from[0] - 1).ToString() + Convert.ToChar(from[1] + 2),
              Convert.ToChar(from[0] + 1).ToString() + Convert.ToChar(from[1] - 2),
              Convert.ToChar(from[0] + 1).ToString() + Convert.ToChar(from[1] + 2),
              Convert.ToChar(from[0] + 2).ToString() + Convert.ToChar(from[1] - 1),
              Convert.ToChar(from[0] + 2).ToString() + Convert.ToChar(from[1] + 1)
            };

            foreach(string e in points)
            {
              if (checkTheCell(e))
              {
                try
                {
                  GameObject go = GameObject.Find(e);
                  if (go.transform.childCount > 0)
                  {
                    if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                      continue;
                  }
                  cellsList.Add(go.GetComponent<Cell>());
                }
                catch{}
              }
                
            }
            break;
          ///////////////////////////////////////////////////////////////////////////////////////////////////
          case Directional.NorthEast:

            if (figure.figureConfig.Role == Role.pawn)
              distance = 1;

            for(int i = 1; i <= distance; i++)
            {
              var newPosition = (Convert.ToChar(from[0] + i)).ToString() + (Convert.ToInt32(from[1].ToString()) + i).ToString();
              if (checkTheCell(newPosition))
                {
                  try
                  {
                    GameObject go = GameObject.Find(newPosition);
                    if (go.transform.childCount > 0)
                    {
                      if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                        break;

                      cellsList.Add(go.GetComponent<Cell>());
                      break;
                    }
                    if (figure.figureConfig.Role == Role.pawn)
                        break;
                    cellsList.Add(go.GetComponent<Cell>());
                  }
                  catch{}
                }
            }
            break;

            ///////////////////////////////////////////////////////////////////////////////////////////////////
          case Directional.NorthWest:

            if (figure.figureConfig.Role == Role.pawn)
              distance = 1;

            for(int i = 1; i <= distance; i++)
            {
              var newPosition = (Convert.ToChar(from[0] - i)).ToString() + (Convert.ToInt32(from[1].ToString()) + i).ToString();
              if (checkTheCell(newPosition))
                {
                  try
                  {
                    GameObject go = GameObject.Find(newPosition);
                    if (go.transform.childCount > 0)
                    {
                      if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                        break;

                      cellsList.Add(go.GetComponent<Cell>());
                      break;
                    }
                    if (figure.figureConfig.Role == Role.pawn)
                        break;
                    cellsList.Add(go.GetComponent<Cell>());
                  }
                  catch{}
                }
            }
            break;

            ///////////////////////////////////////////////////////////////////////////////////////////////////
          case Directional.SouthEast:

            if (figure.figureConfig.Role == Role.pawn)
              distance = 1;

            for(int i = 1; i <= distance; i++)
            {
              var newPosition = (Convert.ToChar(from[0] + i)).ToString() + (Convert.ToInt32(from[1].ToString()) - i).ToString();
              if (checkTheCell(newPosition))
                {
                  try
                  {
                    GameObject go = GameObject.Find(newPosition);
                    if (go.transform.childCount > 0)
                    {
                      if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                        break;

                      cellsList.Add(go.GetComponent<Cell>());
                      break;
                    }
                    if (figure.figureConfig.Role == Role.pawn)
                        break;
                    cellsList.Add(go.GetComponent<Cell>());
                  }
                  catch{}
                }
            }
            break;
              ///////////////////////////////////////////////////////////////////////////////////////////////////
          case Directional.SouthWest:

            if (figure.figureConfig.Role == Role.pawn)
              distance = 1;

            for(int i = 1; i <= distance; i++)
            {
              var newPosition = (Convert.ToChar(from[0] - i)).ToString() + (Convert.ToInt32(from[1].ToString()) - i).ToString();
              if (checkTheCell(newPosition))
                {
                  try
                  {
                    GameObject go = GameObject.Find(newPosition);
                    if (go.transform.childCount > 0)
                    {
                      if (go.transform.GetChild(0).GetComponent<Figure>().figureConfig.Color == figure.figureConfig.Color)
                        break;

                      cellsList.Add(go.GetComponent<Cell>());
                      break;
                    }
                    if (figure.figureConfig.Role == Role.pawn)
                        break;
                    cellsList.Add(go.GetComponent<Cell>());
                  }
                  catch{}
                }
            }
            break;
      }
      return cellsList;
    }
  private static bool checkTheCell(string cellName)
    {
      if(cellName.Length != 2) return false;

      if (cellName[0] >= 65 && cellName[0] <= 72)
      {
        if (cellName[1].ToString() == 1.ToString() 
        || cellName[1].ToString() == 2.ToString() 
        || cellName[1].ToString() == 3.ToString() 
        || cellName[1].ToString() == 4.ToString() 
        || cellName[1].ToString() == 5.ToString() 
        || cellName[1].ToString() == 6.ToString() 
        || cellName[1].ToString() == 7.ToString() 
        || cellName[1].ToString() == 8.ToString())
        {
          return true;
        }
      }
      
      return false;
    } 
}




/*
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
*/

