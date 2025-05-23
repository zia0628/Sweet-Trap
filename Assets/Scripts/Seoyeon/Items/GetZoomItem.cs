using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetZoomItem : MonoBehaviour
{
    [SerializeField] private CameraZoom zoom;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string needItemName;

    private Collider2D itemCollider;
    private ItemPickUp itemPickUp;

    private void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = false;

        itemPickUp = GetComponent<ItemPickUp>();
        itemPickUp.isConditionMet = false;
    }

    private void Update()
    {
        GetItem();
    }

    private void GetItem()
    {
        if (zoom.isZoom && Camera.main.orthographicSize < (zoom.minZoom + 0.1f))
        {
            itemCollider.enabled = true;
            itemPickUp.isConditionMet = IsItemExist(needItemName);
        }
        else
            itemCollider.enabled = false;
    }

    // 필요한 아이템이 인벤토리에 있는지 확인
    private bool IsItemExist(string itemName)
    {
        if (itemName == "None") return true;

        bool isExist = false;
        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        foreach (InventoryItem item in inventoryItems.Values)
        {
            if (item.item.DisplayName == itemName)
            {
                isExist = true;
                break;
            }
        }

        return isExist;
    }
}
