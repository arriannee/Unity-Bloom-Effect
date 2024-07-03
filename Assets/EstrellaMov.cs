using UnityEngine;

public class StarMovementBounce : MonoBehaviour
{
    public float speed = 5.0f;                 // Velocidad de movimiento lineal
    public float angularSpeed = 100.0f;        // Velocidad de rotación
    public Color emissionColor = Color.white;  // Color de emisión
    public float emissionIntensity = 1.0f;     // Intensidad de la emisión
    public float emissionFadeDuration = 0.1f;  // Duración de la disminución de la emisión
    public float blurAmount = 0.1f;            // Cantidad de desenfoque
    public float blurFadeDuration = 0.1f;      // Duración de la disminución del desenfoque

    private Rigidbody rb;
    private Material[] starMaterials;
    private Color[] originalEmissionColors;
    private float[] originalBlurAmounts;
    private bool isEmitting = false;
    private Vector3 currentDirection;
    private float currentEmissionIntensity;
    private float currentBlurAmount;
    private float fadeStartTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        starMaterials = GetComponent<Renderer>().materials;

        // Guardar los colores de emisión originales y los valores de desenfoque para ambos materiales
        originalEmissionColors = new Color[starMaterials.Length];
        originalBlurAmounts = new float[starMaterials.Length];
        for (int i = 0; i < starMaterials.Length; i++)
        {
            if (starMaterials[i].HasProperty("_EmissionColor"))
            {
                originalEmissionColors[i] = starMaterials[i].GetColor("_EmissionColor");
            }
            else
            {
                originalEmissionColors[i] = starMaterials[i].GetColor("_GlowColor");
            }

            if (starMaterials[i].HasProperty("_BlurAmount"))
            {
                originalBlurAmounts[i] = starMaterials[i].GetFloat("_BlurAmount");
            }
        }

        // Restringir el movimiento al plano XY
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        // Inicializar dirección
        currentDirection = GetRandomDirection();

        // Iniciar el movimiento y rotación
        rb.velocity = currentDirection * speed;
        rb.angularVelocity = new Vector3(0, 0, angularSpeed * Mathf.Deg2Rad);
    }

    void Update()
    {
        // Si se está moviendo, mantener la velocidad angular constante
        rb.angularVelocity = new Vector3(0, 0, angularSpeed * Mathf.Deg2Rad);

        // Desvanecer la emisión y el desenfoque gradualmente
        if (isEmitting)
        {
            float elapsed = Time.time - fadeStartTime;
            currentEmissionIntensity = Mathf.Lerp(emissionIntensity, 0f, elapsed / emissionFadeDuration);
            currentBlurAmount = Mathf.Lerp(blurAmount, 0f, elapsed / blurFadeDuration);

            if (currentEmissionIntensity <= 0 && currentBlurAmount <= 0)
            {
                currentEmissionIntensity = 0;
                currentBlurAmount = 0;
                isEmitting = false;
            }

            for (int i = 0; i < starMaterials.Length; i++)
            {
                if (starMaterials[i].HasProperty("_EmissionColor"))
                {
                    starMaterials[i].SetColor("_EmissionColor", originalEmissionColors[i] * Mathf.LinearToGammaSpace(currentEmissionIntensity));
                }
                else
                {
                    starMaterials[i].SetColor("_GlowColor", originalEmissionColors[i] * Mathf.LinearToGammaSpace(currentEmissionIntensity));
                }

                if (starMaterials[i].HasProperty("_BlurAmount"))
                {
                    starMaterials[i].SetFloat("_BlurAmount", currentBlurAmount);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Obtener la normal de la superficie de colisión
        Vector3 collisionNormal = collision.contacts[0].normal;

        // Reflejar la dirección actual en la normal de la colisión
        currentDirection = Vector3.Reflect(currentDirection, collisionNormal);

        // Asignar la nueva dirección
        rb.velocity = currentDirection * speed;

        // Iniciar la emisión y el desenfoque
        isEmitting = true;
        fadeStartTime = Time.time;
        currentEmissionIntensity = emissionIntensity;
        currentBlurAmount = blurAmount;

        // Activar la emisión y el desenfoque para ambos materiales
        for (int i = 0; i < starMaterials.Length; i++)
        {
            if (starMaterials[i].HasProperty("_EmissionColor"))
            {
                starMaterials[i].SetColor("_EmissionColor", originalEmissionColors[i] * Mathf.LinearToGammaSpace(emissionIntensity));
                starMaterials[i].EnableKeyword("_EMISSION");
            }
            else
            {
                starMaterials[i].SetColor("_GlowColor", originalEmissionColors[i] * Mathf.LinearToGammaSpace(emissionIntensity));
                starMaterials[i].EnableKeyword("_EMISSION");
            }

            if (starMaterials[i].HasProperty("_BlurAmount"))
            {
                starMaterials[i].SetFloat("_BlurAmount", blurAmount);
            }
        }
    }

    Vector3 GetRandomDirection()
    {
        // Generar una dirección aleatoria en el plano XY
        float angle = Random.Range(0f, 360f);
        float radians = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized;
    }
}
