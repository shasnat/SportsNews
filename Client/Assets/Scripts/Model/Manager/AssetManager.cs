using UnityEngine;
using System.Collections;
using System;

public class AssetManager : MonoBehaviour {

	private Sprite[] _sprites;
	private string[] _names;

	public static AssetManager instance;

	void Start () {
		instance = this;
		LoadSprites();
	}

	private void LoadSprites() {
		_sprites = Resources.LoadAll<Sprite>("UI/Sprites/");
		_names = new string[_sprites.Length];
		for(int i=0; i< _names.Length; i++) {
			_names[i] = _sprites[i].name;
		}
	}
	
	public Sprite GetSprite(string spriteName) {
		Sprite sprite = null;
		if(!string.IsNullOrEmpty(spriteName)) {
			if(_sprites != null) {
				int index = Array.IndexOf(_names, spriteName);
				if(index >= 0 && index < _sprites.Length) {
					sprite = _sprites[index];
				}
			}
		}
		return sprite;
	}
}
