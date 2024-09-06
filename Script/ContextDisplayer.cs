using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public struct ContextMenuArgs {
    public string name;
    [TextArea]
    public string description;
    public Sprite icon;
    public ContextMenuArgs(string name, string description, Sprite icon) {
        this.name = name;
        this.description = description;
        this.icon = icon;
    }
}

public class ContextDisplayer : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text content;
    [SerializeField] Image icon;
    [SerializeField] Text amount;
    [SerializeField] UnityEvent confirm;
    [SerializeField] UnityEvent cancel;
    
    [SerializeField] ContextMenuArgs[] contexts;

    private ContextMenuArgs currentContext;

    public event EventHandler<ContextMenuArgs> OnConfirm;
    public event EventHandler<ContextMenuArgs> OnCancel;

    public void EnableContext(string name) {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        currentContext = Array.Find(contexts, c => c.name == name);
        title.text = currentContext.name;
        content.text = currentContext.description;
        icon.sprite = currentContext.icon;        
    }

    public void EnableContext(string name, int amount) {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        currentContext = Array.Find(contexts, c => c.name == name);
        title.text = currentContext.name;
        content.text = currentContext.description;
        icon.sprite = currentContext.icon;
        this.amount.text = amount.ToString();
    }
    public void Confirm() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        confirm?.Invoke();
        OnConfirm?.Invoke(this, currentContext);
    }
    public void Cancel() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        cancel?.Invoke();
        OnCancel?.Invoke(this, currentContext);
    }
}
