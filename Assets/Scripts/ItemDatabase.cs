using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;



public class ItemDatabase : MonoBehaviour
{
	private List<Item> database = new List<Item> ();
	private JsonData itemData;


	void Start()
	{
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase();

		Debug.Log(FetchItemByID(0).Description);
	}

	public Item FetchItemByID(int id)
	{
		for (int i = 0; i < database.Count; i++)
			if (database[i].ID == id)
				return database[i];
		return null;
		
    }
	void ConstructItemDatabase()
	{
		for (int i = 0; i < itemData.Count; i++)
		{
		
			database.Add(new Item(
				(int)itemData[i]["id"],
				itemData[i]["title"].ToString(),
				(int)itemData[i]["value"],
				(int)itemData[i]["stats"]["loadoutmodifier"],
				float.Parse(itemData[i]["stats"]["speed"].ToString()),
				float.Parse(itemData[i]["stats"]["survivability"].ToString()),
				itemData[i]["description"].ToString(),
				(bool)itemData[i]["stackable"],
				itemData[i]["slug"].ToString()));
		}
	}

public class Item
	{
		public int ID { get;  set; }
		public string Title { get;  set; }
		public int Value { get;  set; }
		public int LoadoutModifier { get;  set; }
		public float Speed { get;  set; }
		public float Survivability { get;  set; }
		public string Description { get;  set; }
		public bool Stackable { get;  set; }
		public string Slug { get;  set; }
		public Sprite Sprite { get; set; }
		public Item (int id, string title, int value, int loadoutmodifier, float speed, float survivability, string description, bool stackable, string slug)
		{
			this.ID = id;
			this.Title = title;
			this.Value = value;
			this.LoadoutModifier = loadoutmodifier;
			this.Speed = speed;
			this.Survivability = survivability;
			this.Description = description;
			this.Stackable = stackable;
			this.Slug = slug;
			this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
		}
		public Item()
		{
			this.ID = -1;
		}

	}
}