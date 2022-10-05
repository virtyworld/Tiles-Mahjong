using System.Collections;
using UnityEngine;

public class CellComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private CardComponent cardComponent;
    
    public CardComponent CardComponent => cardComponent;

    public void Setup(CardComponent card)
    {
        cardComponent = card;
    }

    private void Start()
    {
        spriteRenderer.sprite = cardComponent.SpriteRendererPrefab.sprite;
    }

    public void DestroyCell()
    {
        StartCoroutine(Dell());
    }

    private IEnumerator Dell()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
