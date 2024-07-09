using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cherryCount;

    [SerializeField] public TextMeshProUGUI timerCounter;

    private void Start()
    {
        if(cherryCount == null) { Debug.LogWarning("Cherry count text is null!"); }
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        CollectableManager.updateUICollectable += UpdateCollectableQty;
    }

    private void OnDisable()
    {
        CollectableManager.updateUICollectable -= UpdateCollectableQty;
    }
    #endregion

    public void UpdateCollectableQty(CollectableNames collectableNames, int qty) {
        
        cherryCount.text = "x" + qty.ToString();

    }

}
