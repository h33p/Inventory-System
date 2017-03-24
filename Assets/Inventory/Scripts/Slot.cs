using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GreenByteSoftware.Inventory {
	public class Slot : MonoBehaviour , IPointerClickHandler {

		public Inventory inventory;
		public Text countText;
		public int x;
		public int y;
		public Image displayItem;
		public InventoryItem item;

		private int lastCount;
		private Sprite lastSprite;

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

			if (lastCount != item.count || !countText.enabled) {
				if (item.count > 1) {
					countText.enabled = true;
					countText.text = "" + item.count;
				} else
					countText.enabled = false;
				lastCount = item.count;
			}

			if (lastSprite != item.itemType.sprite || !displayItem.enabled) {
				displayItem.enabled = true;
				displayItem.sprite = item.itemType.sprite;
				lastSprite = item.itemType.sprite;
			}
		}

		public void Click (bool left) {
			inventory.OnClick (x, y, left);
		}

		public void HoverOn () {
			inventory.OnHover (x, y);
		}

		public void HoverOff () {
			inventory.OnHoverOff (x, y);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
				Click (true);
			else if (eventData.button == PointerEventData.InputButton.Right)
				Click (false);
		}
	}
}