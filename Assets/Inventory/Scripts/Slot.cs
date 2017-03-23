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

		public void Setup (Inventory inv, int pX, int pY, Image display = null) {
			inventory = inv;
			x = pX;
			y = pY;
			displayItem = display;
		}

		public void Click () {
			inventory.OnClick (x, y);
		}
	}
}