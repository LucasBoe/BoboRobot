using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpriteWhileOnTrigger : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    float opacity = 1;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
            Destroy(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOn)
        {
            opacity = Mathf.Min(opacity + 0.1f, 1);
            UpdateOpacity(opacity);
        }
        else
        {
            if (opacity > 0)
            {
                opacity -= 0.1f;
                UpdateOpacity(opacity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOn = false;
        }
    }

    void UpdateOpacity(float _opacity)
    {
        spriteRenderer.color = new Color(1, 1, 1, _opacity);
    }
}
