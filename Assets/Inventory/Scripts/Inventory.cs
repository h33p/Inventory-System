using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenByteSoftware.Inventory {
	public class Inventory : MonoBehaviour {

		private Slot[,] slots;
		public InventoryData data;
		public InventoryStyle style;
		public InventoryItem mouseItem = null;

		public InventoryItem spawnItem;

		public Transform mouseItemTransform;
		public Image mouseItemImage;
		public Text mouseItemText;
		private RectTransform inventoryBackground;
		private Transform slotInstance;
		public Transform inventoryParent;


		// Use this for initialization
		void Start () {
			BuildInventory ();
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown (KeyCode.T)) {
				slots [Random.Range (0, data.sizeX), Random.Range (0, data.sizeY)].item = new InventoryItem (spawnItem, (short)Random.Range (1, spawnItem.itemType.stackSize));
			}

			if (mouseItem == null) {
				mouseItemText.enabled = false;
				mouseItemImage.enabled = false;
			} else {
				mouseItemImage.sprite = mouseItem.itemType.sprite;
				if (mouseItem.count > 1) {
					mouseItemText.enabled = true;
					mouseItemText.text = "" + mouseItem.count;
				} else {
					mouseItemText.enabled = false;
				}
				mouseItemTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
			}
		}

		public void BuildInventory () {
			slots = new Slot[data.sizeX, data.sizeY];

			inventoryBackground = (RectTransform) Instantiate (style.inventoryBackground).transform;
			inventoryBackground.SetParent (inventoryParent);

			Text iT = inventoryBackground.GetComponentInChildren<Text> ();
			iT.text = data.name;
			iT.alignment = style.titleAlignment;
			iT.fontSize = style.titleSize;
			iT.color = style.color;

			inventoryBackground.localPosition = new Vector3 (0, 0, 0);
			inventoryBackground.sizeDelta = new Vector2 (style.addX + style.slotMultiplier * data.sizeX, style.addY + style.slotMultiplier * data.sizeY);

			float offsetX = -inventoryBackground.sizeDelta.x * 0.5f;
			float offsetY = -inventoryBackground.sizeDelta.y * 0.5f;


			slotInstance = Instantiate (style.slot).transform;
			slotInstance.SetParent (inventoryBackground);
			slotInstance.localPosition = new Vector3(offsetX + style.slotStartX, offsetY + style.slotStartY, 0);
			slots [0, 0] = slotInstance.GetComponent<Slot> ();
			slots [0, 0].Setup (this, 0, 0);
			for (int y = 0; y < data.sizeY; y++) {
				for (int x = 0; x < data.sizeX; x++) {
					if (x != 0 || y != 0) {
						slots [x, y] = Instantiate (slotInstance).GetComponent<Slot> ();
						slots [x, y].transform.SetParent (inventoryBackground);
						slots [x, y].Setup (this, x, y);
						slots [x, y].transform.localPosition = new Vector3 (offsetX + style.slotStartX + x * style.slotMultiplier, offsetY + style.slotStartY + y * style.slotMultiplier, 0);
					}
				}
			}

		}

		public void OnClick (int x, int y) {
			if (mouseItem == null) {
				mouseItem = slots [x, y].item;
				slots [x, y].item = null;
			} else if (slots [x, y].item == null) {
				slots [x, y].item = mouseItem;
				mouseItem = null;
			} else if (mouseItem.itemType == slots [x, y].item.itemType && mouseItem.dataBits == slots [x, y].item.dataBits) {
				short r = (short)Mathf.Min (mouseItem.count, mouseItem.itemType.stackSize - slots [x, y].item.count);
				mouseItem.count -= r;
				slots [x, y].item.count += r;
				if (mouseItem.count == 0)
					mouseItem = null;
			} else {
				InventoryItem t = slots [x, y].item;
				slots [x, y].item = mouseItem;
				mouseItem = t;
			}
		}

	}
}