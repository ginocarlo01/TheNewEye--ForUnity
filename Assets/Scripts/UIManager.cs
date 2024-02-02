using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cherryCount;

    public static UIManager instance;

    private void Awake() { instance = this; }

    private void Start()
    {
        if(cherryCount == null) { Debug.LogWarning("Cherry count text is null!"); }
        //cherryCount.text = "x" + "0";
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
        Debug.Log("He was heard!" + qty);
        cherryCount.text = "x" + qty.ToString();

    }

}
