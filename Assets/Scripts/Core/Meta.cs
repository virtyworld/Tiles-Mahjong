using System;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField] private LevelComponent[] levelComponent;
    [SerializeField] private Transform gameDirectory;
    [SerializeField] private Transform menuDirectory;
    [SerializeField] private MenuComponent menuPrefab;

    private static Action onNextLevelClickAction;
    private static Action onRetryClickAction;
    private static Action onShowVictoryPanel;
    private static Action onShowDefeatPanel;
    private LevelComponent lc1, lc2;
    private MenuComponent menuComponent;
    private int levelNumber;
    
    void Start()
    {
        onNextLevelClickAction += NextLevelClicked;
        onRetryClickAction += RetryClicked;
        onShowVictoryPanel += ShowVictoryPanel;
        onShowDefeatPanel += ShowDefeatPanel;
        
        menuComponent = Instantiate(menuPrefab, menuDirectory);
        menuComponent.Setup(onNextLevelClickAction,onRetryClickAction);
        
        levelNumber = 0;
        lc1 = Instantiate(levelComponent[levelNumber],gameDirectory);
        lc1.Setup(onShowVictoryPanel,onShowDefeatPanel);
    }

    private void NextLevelClicked()
    {
        if (lc1) Destroy(lc1.gameObject);
        levelNumber = 1;
        lc2 = Instantiate(levelComponent[1],gameDirectory);
        lc2.Setup(onShowVictoryPanel,onShowDefeatPanel);
    }

    //need factory
    private void RetryClicked()
    {
        if (levelNumber == 0)
        {
            if (lc1) Destroy(lc1.gameObject);
            lc1 = Instantiate(levelComponent[0],gameDirectory);
            lc1.Setup(onShowVictoryPanel,onShowDefeatPanel);
        }

        if (levelNumber == 1)
        {
            if (lc2) Destroy(lc2.gameObject);
            lc2 = Instantiate(levelComponent[1],gameDirectory);
            lc2.Setup(onShowVictoryPanel,onShowDefeatPanel);
        }
    }

    private void ShowVictoryPanel()
    {
        menuComponent.ShowContinue();
    }

    private void ShowDefeatPanel()
    {
        menuComponent.ShowRetry();
    }
}
