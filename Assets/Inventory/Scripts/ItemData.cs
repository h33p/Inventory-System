using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace GreenByteSoftware.Inventory {

	[System.Serializable]
	public struct ItemProperties {
		public string name;
		public int id;
		public int value;
		public bool display;
	}

	[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item Data", order = 1)]
	public class ItemData : ScriptableObject {

		public string name;
		public string description;

		public SpriteNetwork sprite;
		public short stackSize = 1;
		public ItemProperties[] properties;

	}

	[System.Serializable]
	public struct InventoryItem {

		public ItemData itemType;
		public short count;
		public int dataBits;

		public InventoryItem (InventoryItem baseItem, short newCount) {
			itemType = baseItem.itemType;
			dataBits = baseItem.dataBits;
			count = newCount;
		}

		public InventoryItem (ItemData item, short cnt, int data) {
			itemType = item;
			count = cnt;
			dataBits = data;
		}

	}
}