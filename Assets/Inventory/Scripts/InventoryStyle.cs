using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenByteSoftware.Inventory {

	public enum NameAlignment {
		TopCentre = 0, TopLeft = 1, TopRight = 2, BottomCentre = 3, BottomLeft = 4, BottomRight = 5
	};

	[CreateAssetMenu(fileName = "Inventory Style", menuName = "Inventory/Inventory Style", order = 1)]
	public class InventoryStyle : ScriptableObject {

		public GameObject inventoryImage;
		public GameObject slotImage;
		public GameObject slotClickableImage;
		public NameAlignment titleAlignment;
		public Font titleFont;

	}
}