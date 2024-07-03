using UnityEngine;

public class StarSpawner : MonoBehaviour 
{
    public GameObject starPrefab;       // Prefab de la estrella
    public float spawnInterval = 6.0f;  // Intervalo de tiempo entre instancias
    public int spawnCount = 0;          // Número de estrellas creadas

    private float timer = 0.0f;

    void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            InstantiateStar();
            timer = 0.0f;
        }
    }

    void InstantiateStar()
    {

        if (spawnCount < 4)
        {
            // Instanciar una nueva estrella en una posición aleatoria dentro de la pantalla
            Vector3 spawnPosition = new Vector3(
                Random.Range(-6.0f, 6.0f),  // x
                Random.Range(-3.0f, 3.0f),  // y
                6.0f                        // z
            );

            Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            spawnCount++;
        }
    }
}
