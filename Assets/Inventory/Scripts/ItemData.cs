using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenByteSoftware.Inventory {

	[System.Serializable]
	public struct ItemProperties {
		public string name;
		public int id;
		public int value;
		public bool display;
	}

	[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Iten Data", order = 1)]
	public class ItemData : ScriptableObject {

		public Image image;
		public bool stackable;
		public ItemProperties[] properties;

	}
}