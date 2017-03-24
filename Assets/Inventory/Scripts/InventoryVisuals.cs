using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenByteSoftware.Inventory {

public class InventoryVisuals : MonoBehaviour {

		public static InventoryVisuals singleton;

		public SpritesPackage sprites;
		public InventoryItem mouseItem;
		public Text mouseHoverText;
		public Transform mouseHoverTransform;
		public Image mouseHoverBackground;

		public Transform mouseItemTransform;
		public Image mouseItemImage;
		public Text mouseItemText;

		void Awake () {
			singleton = this;
		}

		void Update () {

			if (mouseItem.itemType == null) {
				mouseItemText.enabled = false;
				mouseItemImage.enabled = false;
			} else {
				mouseItemImage.enabled = true;
				mouseItemImage.sprite = mouseItem.itemType.sprite.GetSprite (sprites);
				if (mouseItem.count > 1) {
					mouseItemText.enabled = true;
					mouseItemText.text = "" + mouseItem.count;
				} else {
					mouseItemText.enabled = false;
				}
				mouseItemTransform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, mouseItemTransform.position.z);
			}
		}
	}
}