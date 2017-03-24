using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenByteSoftware.Inventory {
	public class Slot : MonoBehaviour {

		public Inventory inventory;
		public Text countText;
		public int x;
		public int y;
		public Image displayItem;
		public InventoryItem item;

		public void Setup (Inventory inv, int pX, int pY, Sprite display = null) {
			inventory = inv;
			x = pX;
			y = pY;
			displayItem.sprite = display;
		}

		void Update () {
			if (item == null || item.itemType == null) {
				displayItem.enabled = false;
				countText.enabled = false;
				return;
			}

			if (item.count > 1) {
				countText.enabled = true;
				countText.text = "" + item.count;
			} else
				countText.enabled = false;

			displayItem.enabled = true;
			displayItem.sprite = item.itemType.sprite;
		}

		public void Click () {
			inventory.OnClick (x, y);
		}
	}
}