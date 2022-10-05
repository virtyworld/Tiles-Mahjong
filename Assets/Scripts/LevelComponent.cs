using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    [SerializeField] private CardsPanelComponent cardsPanelComponentPrefab;
    [SerializeField] private List<Transform> cellsHolder;
    [SerializeField] private CellComponent cellPrefab;

    private Action<CardComponent> onCardClickedAction;
    private Action onVictoryAction;
    private Action onLoseAction;
    private Action<int> onCardsCountAction;
    private List<CellComponent> cardInCell = new List<CellComponent>();
    private int countCards;
    private int removedCardsCount;
    private bool isGameEnding;
    
    public void Setup(Action onVictoryAction,Action onLoseAction)
    {
        this.onVictoryAction = onVictoryAction;
        this.onLoseAction = onLoseAction;
    }
    
    void Start()
    {
        onCardClickedAction += CardMove;
        onCardsCountAction += CardsCount;
        CardsPanelComponent cpc = Instantiate(cardsPanelComponentPrefab, transform);
        cpc.Setup(onCardClickedAction,onCardsCountAction);
    }

    private void CardsCount(int count)
    {
        countCards += count;
    }
    private void CardMove(CardComponent cardComponent)
    {
        if (!isGameEnding) GameLogic(cardComponent);
    }

    private void GameLogic(CardComponent cardComponent)
    {
        int count = 0;
      
        for (int i = 0; i < cellsHolder.Count; i++)
        {
            if (cellsHolder[i].childCount==0)
            {
                CellComponent cellSprite = Instantiate(cellPrefab, cellsHolder[i].transform);
                cellSprite.Setup(cardComponent);
                cardInCell.Add(cellSprite);
                break;
            }
        }
        if (cardInCell.Count<=7)
        {
            foreach (var card in cardInCell)
            {
                if (card.CardComponent.CardType==cardComponent.CardType)
                {
                    count++;
                }
            }
        }
        
        if (count>=3)DestroyCard(cardComponent.CardType);
        
        if (cardInCell.Count>=7) LoseGame();
        
        if (countCards==removedCardsCount)Victory();
    }

    private void DestroyCard(CardType cardType)
    {
        List<CellComponent> cc = new List<CellComponent>(cardInCell);

        for (int i = 0; i < cardInCell.Count; i++)
        {
            if (cardInCell[i].CardComponent.CardType==cardType)
            {
                 cardInCell[i].DestroyCell();
                 cc.Remove(cardInCell[i]);
                 removedCardsCount++;
            }
        }
        cardInCell.Clear();
        cardInCell.AddRange(cc);
    }
    
    
    private void LoseGame()
    {
        isGameEnding = true;
        onLoseAction?.Invoke();
    }

    private void Victory()
    {
        isGameEnding = true;
        onVictoryAction?.Invoke();
    }
}
