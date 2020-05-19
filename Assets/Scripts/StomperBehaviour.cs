using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomperBehaviour : MonoBehaviour
{
    [SerializeField] float height;

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (boxCollider2D == null || spriteRenderer == null)
            Debug.LogError("No BoxCollider2D or SpriteRenderer found, please add");
    }

    void Update()
    {
        if (boxCollider2D == null || spriteRenderer == null)
            return;

        boxCollider2D.size = new Vector2(1, height);
        spriteRenderer.size = new Vector2(1, height);
        boxCollider2D.offset = new Vector2(0, height / -2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (height < 2.5f)
            return;

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
