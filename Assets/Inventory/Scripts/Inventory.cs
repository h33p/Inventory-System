using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenByteSoftware.Inventory {
	
	public class Inventory : MonoBehaviour {

		private Slot[,] slots;
		public InventoryData data;
		public InventoryStyle style;

		public InventoryItem spawnItem;

		private RectTransform inventoryBackground;
		private Transform slotInstance;
		public Transform inventoryParent;

		public Vector3 inventoryPos;


		void Start () {
			BuildInventory ();
		}

		void Update () {
			if (Input.GetKeyDown (KeyCode.T)) {
				slots [Random.Range (0, data.sizeX), Random.Range (0, data.sizeY)].item = new InventoryItem (spawnItem, (short)Random.Range (1, spawnItem.itemType.stackSize));
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

			inventoryBackground.localPosition = inventoryPos;
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

		public void OnHover (int x, int y) {
			if (slots [x, y].item.itemType == null)
				return;
			
			InventoryVisuals.singleton.mouseHoverTransform.position = slots [x, y].transform.position;
			InventoryVisuals.singleton.mouseHoverText.enabled = true;
			InventoryVisuals.singleton.mouseHoverBackground.enabled = true;
			InventoryVisuals.singleton.mouseHoverText.text = slots [x, y].item.itemType.name + "\n" + slots [x, y].item.itemType.description;
		}

		public void OnHoverOff (int x, int y) {
			InventoryVisuals.singleton.mouseHoverText.enabled = false;
			InventoryVisuals.singleton.mouseHoverBackground.enabled = false;
		}

		public void OnClick (int x, int y, bool left, ref InventoryItem mouseItem) {
			OnHoverOff (0, 0);
			if (left) {
				if (mouseItem.itemType == null) {
					mouseItem = slots [x, y].item;
					slots [x, y].item.itemType = null;
					slots [x, y].item.count = 0;
					slots [x, y].item.dataBits = 0;
				} else if (slots [x, y].item.itemType == null) {
					slots [x, y].item = mouseItem;
					mouseItem.count = 0;
					mouseItem.dataBits = 0;
					mouseItem.itemType = null;
				} else if (mouseItem.itemType == slots [x, y].item.itemType && mouseItem.dataBits == slots [x, y].item.dataBits) {
					short r = (short)Mathf.Min (mouseItem.count, mouseItem.itemType.stackSize - slots [x, y].item.count);
					mouseItem.count -= r;
					slots [x, y].item.count += r;
					if (mouseItem.count == 0) {
						mouseItem.count = 0;
						mouseItem.dataBits = 0;
						mouseItem.itemType = null;
					}
				} else {
					InventoryItem t = slots [x, y].item;
					slots [x, y].item = mouseItem;
					mouseItem = t;
				}
			} else {
				if (mouseItem.itemType == null) {
					mouseItem = new InventoryItem (slots [x, y].item, (short) (slots [x, y].item.count / 2));
					slots [x, y].item.count -= (short) (slots [x, y].item.count / 2);
					if (slots [x, y].item.count == 0) {
						slots [x, y].item.itemType = null;
						slots [x, y].item.count = 0;
						slots [x, y].item.dataBits = 0;
					}
					if (mouseItem.count == 0) {
						mouseItem.count = 0;
						mouseItem.dataBits = 0;
						mouseItem.itemType = null;
					}
				} else if (slots [x, y].item.itemType == null) {
					slots [x, y].item = new InventoryItem (mouseItem, 1);
					mouseItem.count--;
					if (mouseItem.count == 0) {
						mouseItem.count = 0;
						mouseItem.dataBits = 0;
						mouseItem.itemType = null;
					}
				} else if (mouseItem.itemType == slots [x, y].item.itemType && mouseItem.dataBits == slots [x, y].item.dataBits && slots [x, y].item.count < slots[x,y].item.itemType.stackSize) {
					mouseItem.count--;
					slots [x, y].item.count++;
					if (mouseItem.count == 0) {
						mouseItem.count = 0;
						mouseItem.dataBits = 0;
						mouseItem.itemType = null;
					}
				} else {
					InventoryItem t = slots [x, y].item;
					slots [x, y].item = mouseItem;
					mouseItem = t;
				}
			}
			OnHover (x, y);
		}

	}
}