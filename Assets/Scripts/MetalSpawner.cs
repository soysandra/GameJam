using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MetalSpawner : MonoBehaviour
{
    [Header("Tilemap donde se generan los puntos")]
    public Tilemap tilemapPuntos;

    [Header("Prefabs")]
    public GameObject metalBuenoPrefab;
    public GameObject metalMaloPrefab;

    [Header("Cantidad a generar")]
    public int cantidadBuenos = 5;
    public int cantidadMalos = 5;

    [Header("Restricciones")]
    public Transform jugador;
    public float distanciaMinimaJugador = 3f;

    private List<Vector3> posicionesValidas = new List<Vector3>();

    void Start()
    {
        ObtenerPosicionesValidas();
        GenerarObjetos();
    }

    void ObtenerPosicionesValidas()
    {
        posicionesValidas.Clear();

        BoundsInt bounds = tilemapPuntos.cellBounds;

        foreach (var celda in bounds.allPositionsWithin)
        {
            if (tilemapPuntos.HasTile(celda))
            {
                Vector3 pos = tilemapPuntos.GetCellCenterWorld(celda);

                // Evitar posiciones cerca del jugador
                if (Vector3.Distance(pos, jugador.position) >= distanciaMinimaJugador)
                {
                    posicionesValidas.Add(pos);
                }
            }
        }
    }

    void GenerarObjetos()
    {
        // Mezclar posiciones (shuffle)
        for (int i = 0; i < posicionesValidas.Count; i++)
        {
            Vector3 temp = posicionesValidas[i];
            int randomIndex = Random.Range(i, posicionesValidas.Count);
            posicionesValidas[i] = posicionesValidas[randomIndex];
            posicionesValidas[randomIndex] = temp;
        }

        int totalNecesario = cantidadBuenos + cantidadMalos;

        if (posicionesValidas.Count < totalNecesario)
        {
            Debug.LogWarning("No hay suficientes celdas en el Tilemap para generar todos los puntos.");
            totalNecesario = posicionesValidas.Count;
        }

        int index = 0;

        // Generar buenos
        for (int i = 0; i < cantidadBuenos && index < posicionesValidas.Count; i++)
        {
            Instantiate(metalBuenoPrefab, posicionesValidas[index], Quaternion.identity);
            index++;
        }

        // Generar malos
        for (int i = 0; i < cantidadMalos && index < posicionesValidas.Count; i++)
        {
            Instantiate(metalMaloPrefab, posicionesValidas[index], Quaternion.identity);
            index++;
        }
    }
}
