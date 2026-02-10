using UnityEngine;

public class MagnetPlayer : MonoBehaviour
{
    public float fuerzaMagnetica = 5f;
    public float radio = 5f;
    public float velocidadMaxima = 5f; // límite de velocidad

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
            AtraerObjetos();

        if (Input.GetKey(KeyCode.Q))
            RepelerObjetos();
    }

    void AtraerObjetos()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        foreach (Collider2D col in objetos)
        {
            if (col.CompareTag("MetalBueno") || col.CompareTag("MetalMalo"))
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                if (rb == null) continue;

                Vector2 direccion = (Vector2)(transform.position - col.transform.position).normalized;
                rb.AddForce(direccion * fuerzaMagnetica);

                // Usar linearVelocity en lugar de velocity
                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, velocidadMaxima);
            }
        }
    }

    void RepelerObjetos()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        foreach (Collider2D col in objetos)
        {
            if (col.CompareTag("MetalBueno") || col.CompareTag("MetalMalo"))
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                if (rb == null) continue;

                Vector2 direccion = (Vector2)(col.transform.position - transform.position).normalized;
                rb.AddForce(direccion * fuerzaMagnetica);

                // Usar linearVelocity en lugar de velocity
                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, velocidadMaxima);
            }
        }
    }
}

