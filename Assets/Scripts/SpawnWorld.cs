using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorld : MonoBehaviour
{
    [SerializeField] private List<GameObject> world_SpawnList = new List<GameObject>();
    [SerializeField] private int dimension = 10;
    [SerializeField] private Transform parentFolder;
    [SerializeField] private GameObject player;
    private GameObject[,] tablero;
    private int x, y; //Coordenadas en el tablero
    //Elevar las casillas (posible animación de arriba abajo) y cambiarlas de coloer auno mas claro

    // Start is called before the first frame update
    void Start()
    {
        tablero = new GameObject[dimension, dimension];

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                Vector3 pos = new Vector3(i * 2, 0, j * 2);
                GameObject nuevaCasilla = world_SpawnList[Random.Range(0, world_SpawnList.Count)];
                GameObject casillaInstanciada = Instantiate(nuevaCasilla, pos, Quaternion.identity, parentFolder);
                tablero[i, j] = casillaInstanciada;
            }
        }
        GameObject casilla;
        do
        {
            x = Random.Range(0, tablero.GetLength(0));
            y = Random.Range(0, tablero.GetLength(1));
            casilla = tablero[x, y];
        } while (casilla.name == "square_water_detail(Clone)");

        Instantiate(player, new Vector3(0, 2, 0) + casilla.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
