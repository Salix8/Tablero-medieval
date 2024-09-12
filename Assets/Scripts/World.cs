using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private List<GameObject> world_SpawnList = new List<GameObject>();
    [SerializeField] private int dimension = 10;
    [SerializeField] private Transform parentFolder;
    [SerializeField] private GameObject player;
    private Tile[,] tiles;
    private int x, y; //Coordenadas en el tablero
    //Elevar las casillas (posible animación de arriba abajo) y cambiarlas de coloer auno mas claro
    private Vector3 selectedTilePosition;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[dimension, dimension];

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                Vector3 pos = new Vector3(i * 2, 0, j * 2);
                GameObject nuevaCasilla = world_SpawnList[Random.Range(0, world_SpawnList.Count)];
                GameObject casillaInstanciada = Instantiate(nuevaCasilla, pos, Quaternion.identity, parentFolder);
                tiles[i, j] = casillaInstanciada.GetComponent<Tile>();
            }
        }
        GameObject casilla;
        do
        {
            x = Random.Range(0, tiles.GetLength(0));
            y = Random.Range(0, tiles.GetLength(1));
            casilla = tiles[x, y].gameObject;
        } while (casilla.name == "square_water_detail(Clone)");

        Instantiate(player, new Vector3(0, 2, 0) + casilla.gameObject.transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void CasillasDisponibles(int distancia, bool isSelected)
    //{
    //    //List<int> disponibles = new List<int>();
    //    for (int i = 1; i <= distancia; i++)
    //    {
    //        if (!isSelected)
    //        { // Cuando se selecciona
    //            tiles[x + i, y - i].ChangeColor();
    //            tiles[x + i, y].ChangeColor();
    //            tiles[x + i, y + i].ChangeColor();
    //            tiles[x, y - i].ChangeColor();

    //            tiles[x, y + i].ChangeColor();
    //            tiles[x - i, y - i].ChangeColor();
    //            tiles[x - i, y].ChangeColor();
    //            tiles[x - i, y + i].ChangeColor();
    //            //disponibles.Add(x * dimension + y); // disponibles[i]/dim disponibles[j]%dim
    //        }
    //        else // Cuando se deja de selleccionar
    //        {
    //            tiles[x + i, y - i].RevertColor();
    //            tiles[x + i, y].RevertColor();
    //            tiles[x + i, y + i].RevertColor();
    //            tiles[x, y - i].RevertColor();

    //            tiles[x, y + i].RevertColor();
    //            tiles[x - i, y - i].RevertColor();
    //            tiles[x - i, y].RevertColor();
    //            tiles[x - i, y + i].RevertColor();

    //        }
    //    }
    //}
    //public void casillasDisponibles(int distancia, bool isSelected)
    //{
    //    int startX = x - distancia;
    //    int startY = y - distancia;
    //    int endX = x + distancia;
    //    int endY = y + distancia;

    //    for (int i = startX; i <= endX; i++)
    //    {
    //        if (i < 0 || i >= dimension)
    //            break;
    //        for (int j = startY; j <= endY; j++)
    //        {
    //            if (i == x && j == y)
    //                continue;

    //            if (j < 0 || j >= dimension) 
    //                break;

    //            if (!isSelected)
    //            {
    //                tiles[i, j].ChangeColor();
    //                tiles[i, j].isDisponible = true;
    //            }
    //            else
    //            {
    //                tiles[i, j].RevertColor();
    //                tiles[i, j].isDisponible = false;
    //            }
    //        }
    //    }
    //}
    //public void casillasDisponibles(int distancia, bool isSelected)
    //{
    //    int startX = Mathf.Max(x - distancia, 0);
    //    int startY = Mathf.Max(y - distancia, 0);
    //    int endX = Mathf.Min(x + distancia, dimension - 1);
    //    int endY = Mathf.Min(y + distancia, dimension - 1);

    //    for (int i = startX; i <= endX; i++)
    //    {
    //        for (int j = startY; j <= endY; j++)
    //        {
    //            if (i == x && j == y)
    //                continue;

    //            if (!isSelected)
    //            {
    //                tiles[i, j].ChangeColor();
    //                tiles[i, j].isDisponible = true;
    //            }
    //            else
    //            {
    //                tiles[i, j].RevertColor();
    //                tiles[i, j].isDisponible = false;
    //            }
    //        }
    //    }
    //}
    public void casillasDisponibles(int distancia, bool isSelected)
    {
        int startX = Mathf.Max(x - distancia, 0);
        int startY = Mathf.Max(y - distancia, 0);
        int endX = Mathf.Min(x + distancia, dimension - 1);
        int endY = Mathf.Min(y + distancia, dimension - 1);

        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (i == x && j == y)
                    continue;

                if (!isSelected)
                {
                    tiles[i, j].ChangeColor();
                    tiles[i, j].isDisponible = true;
                }
                else
                {
                    tiles[i, j].RevertColor();
                    tiles[i, j].isDisponible = false;

                    //selectedTilePosition = tiles[i, j].gameObject.transform.position;
                }
            }
        }
    }


    //public Vector3 posicionCasilla()
    //{
    //    for(int i = 0; i < dimension; i++)
    //    {
    //        for(int j = 0; j < dimension; j++)
    //        {
    //            if (tiles[i,j].OnMouseDown())
    //            {
    //                return tiles[i,j].gameObject.transform.position;
    //            }
    //        }
    //    }
    //    return Vector3.zero;
    //}
    public Vector3 posicionCasilla()
    {
        return selectedTilePosition;
    }


}
