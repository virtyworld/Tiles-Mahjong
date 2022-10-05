using System;
using System.Collections.Generic;
using UnityEngine;

public class CellOnPanel : MonoBehaviour
{
    [SerializeField] private CardComponent[] cards;

    private Action<CardComponent> onCardClicked;
    private Action<int> onCardsCountAction;
    private List<CardComponent> listCardComponent = new List<CardComponent>();

    public void Setup(Action<CardComponent> onCardClicked,Action<int> onCardsCountAction)
    {
        this.onCardClicked = onCardClicked;
        this.onCardsCountAction = onCardsCountAction;
    }
    
    void Start()
    {
        onCardClicked += RemoveCard;
        
        for (int i = 0; i < cards.Length; i++)
        {
            CardComponent cardComponent = Instantiate(cards[i],transform);
            Debug.Log(cards.Length + " "+(cards.Length-i));
          
            cardComponent.Setup(onCardClicked,onCardsCountAction,cards.Length-i);
            cardComponent.transform.position = new Vector3(transform.position.x, transform.position.y + i*.3f, 0);
            listCardComponent.Add(cardComponent);
            if (i>=1)
            {
                HideCell(cardComponent);
            }
        }
    }

    private void HideCell(CardComponent cardComponent)
    {
        cardComponent.HideCard();
    }

    private void RemoveCard(CardComponent cardComponent)
    {
        listCardComponent.RemoveAt(0);
        listCardComponent.AddRange(listCardComponent);
        cardComponent.DestroyCard();
        UnHideCell();
    }

    private void UnHideCell()
    {
        for (int i = 0; i < listCardComponent.Count; i++)
        {
            listCardComponent[i].UnHideCard();
        }
    }
}
