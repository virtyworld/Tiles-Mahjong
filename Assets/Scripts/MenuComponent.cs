using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuComponent : MonoBehaviour
{
    [SerializeField] private GameObject retryPanel;
    [SerializeField] private Button retryButton;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private Button continueButton;

    private Action onRetryAction;
    private Action onContinueAction;

    public void Setup(Action onContinueAction, Action onRetryAction)
    {
        this.onRetryAction = onRetryAction;
        this.onContinueAction = onContinueAction;
    }

    private void Start()
    {
        retryButton.onClick.AddListener(ClickRetry);
        continueButton.onClick.AddListener(ClickContinue);
    }

    public void ShowContinue()
    {
        retryPanel.SetActive(false);
        continuePanel.SetActive(true);
    }

    public void ShowRetry()
    {
        retryPanel.SetActive(true);
        continuePanel.SetActive(false);
    }

    private void ClickRetry()
    {
        onRetryAction?.Invoke();
        Hide();
    }

    private void ClickContinue()
    {
        onContinueAction?.Invoke();
        Hide();
    }

    private void Hide()
    {
        continuePanel.SetActive(false);
        retryPanel.SetActive(false);
    }
}