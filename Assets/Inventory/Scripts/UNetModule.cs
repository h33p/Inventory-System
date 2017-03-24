using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace GreenByteSoftware.Inventory {
	
	public class UNetModule : NetworkBehaviour {

		public Inventory targtInventory;

		public SyncListItem items;

		void Update () {
			
		}

	}

	public class SyncListItem : SyncListStruct<InventoryItem> {}

}
