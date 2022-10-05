using System;
using UnityEngine;

public class CardsPanelComponent : MonoBehaviour
{
    [SerializeField] private CellOnPanel[] cells;

    private int countCards;
    private Action<CardComponent> onCardClicked;
    private Action<int> onCardsCountAction;
    
    public void Setup(Action<CardComponent> onCardClicked, Action<int> onCardsCountAction)
    {
        this.onCardClicked = onCardClicked;
        this.onCardsCountAction = onCardsCountAction;
    }
    private void Start()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            CellOnPanel cop = Instantiate(cells[i], transform);
            cop.Setup(onCardClicked,onCardsCountAction);
        }
    }
}
