using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardType
{
    BluePotion,
    BluePotion2,
    BluePotion3,
    EmptyBottle
}

public class CardComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRendererPrefab;
    [field: SerializeField] private CardType cardType;

    private Action<CardComponent> onCardClicked;
    private Action<int> onCardsCountAction;
    private int orderLayer;
    private bool isHideCard;
    
    public SpriteRenderer SpriteRendererPrefab => spriteRendererPrefab;
    public CardType CardType => cardType;

    public void Setup(Action<CardComponent> onCardClicked, Action<int> onCardsCountAction,int orderLayer)
    {
        this.onCardClicked = onCardClicked;
        this.orderLayer = orderLayer;
        this.onCardsCountAction = onCardsCountAction;
    }

    private void Start()
    {
        onCardsCountAction?.Invoke(1);
        spriteRendererPrefab.sortingOrder = orderLayer;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()|| isHideCard)return;
        onCardClicked?.Invoke(this);
    }

    public void HideCard()
    {
        isHideCard = true;
        spriteRendererPrefab.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        spriteRendererPrefab.sortingOrder = orderLayer;
    }

    public void UnHideCard()
    {
        isHideCard = false;
        spriteRendererPrefab.color = new Color(1f, 1f, 1f, 1f);
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}