using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenByteSoftware.Inventory {
	[CreateAssetMenu(fileName = "Inventory Data", menuName = "Inventory/Inventory Data", order = 1)]
	public class InventoryData : ScriptableObject {

		public string name;
		public int sizeX;
		public int sizeY;

	}
}