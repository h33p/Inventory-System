using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenByteSoftware.Inventory {
	[System.Serializable]
	public class SpriteNetwork {

		public int spriteID;

		public Sprite GetSprite (SpritesPackage pack) {
			return (pack.sprites.Length > spriteID) ? pack.sprites[spriteID] : null;
		}
	}

	[CreateAssetMenu(fileName = "Sprites Package", menuName = "Inventory/Sprites Package", order = 1)]
	public class SpritesPackage : ScriptableObject {
		public Sprite[] sprites;
	}
}
