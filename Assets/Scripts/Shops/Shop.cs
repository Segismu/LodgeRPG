using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Controls;
using UnityEngine;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            yield return new ShopItem(InventoryItem.GetFromID("86cdcb8c-72f4-460b-88e4-450a28caf014"), 5, 10f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("61726d9e-d1d6-4c83-b2d9-2885452a6221"), 1, 100f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("88233aff-9d10-45c0-b905-98d0397b3bf9"), 10, 50f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("88233aff-9d10-45c0-b905-98d0397b3bf9"), 10, 50f, 0);

        }
        public void SelectFilter(ItemCategory category) { }
        public ItemCategory GetFilter() { return ItemCategory.None; }
        public void SelectMode(bool isBuying) { }
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction() { }
        public float TransactionTotal() { return 0; }
        public void AddToTransaction(InventoryItem irem, int quantity) { }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }
            return true;
        }

        public string GetShopName()
        {
            return shopName;
        }
    }
}