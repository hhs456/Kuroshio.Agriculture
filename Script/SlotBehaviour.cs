using Kuroshio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolkid.UIGrid;
namespace Kuroshio.Agriculture { 
    public class SlotBehaviour : MonoBehaviour {
        protected InventoryManager inventory;

        private void OnEnable() {
            inventory = GetComponent<InventoryManager>();
            inventory.DataChanged += OnDataChanged;
            inventory.Validator.Validated += OnValidated;
            inventory.Validator.Invalidated += OnInvalidated;
        }

        private void OnDisable() {
            inventory.DataChanged -= OnDataChanged;
            inventory.Validator.Validated -= OnValidated;
            inventory.Validator.Invalidated -= OnInvalidated;
        }

        private void OnInvalidated(object sender, ItemSlot e) {
            e.SetData(Color.red);
        }

        private void OnValidated(object sender, ItemSlot e) {
            e.SetData(Color.green);
        }
        private void OnDataChanged(object sender, ItemSlot e) {
            e.Image.GetComponent<FarmSlotBehaviour>().PlantSeed(e.ItemId);
            var texture = Resources.Load<Texture>(e.ItemId);
            e.Image.texture = texture;
            e.SetData(Color.white);
        }
    }
}