using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using static ItemDatabase;

public class Inventory : MonoBehaviour
{
	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public int slotAmount;
	//your actual inventory!!!*****
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start()
    {
		database = GetComponent<ItemDatabase>();
		slotAmount = 9;
		inventoryPanel = GameObject.Find("Inventory Panel");
		slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;
		for (int i =0; i < slotAmount; i++)
        {
			items.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[i].transform.SetParent(slotPanel.transform);
			
        }

	}

	public void AddItem(int id)
    {
		//add item to count if stackable
		Item itemToAdd = database.FetchItemByID(id);
		if (itemToAdd.Stackable = true && InventoryCheck(itemToAdd))
        {
			for (int i = 0; i < items.Count; i++)
            {
				if (items[i].ID == id)
                {
					//change the number or the picture to show multiple of the item
					ItemDatabase data
						data.amount++;
                }
			}
		}

		for (int i =0;i<items.Count;i++)
        {
			if(items[i].ID ==-1)
			{ 
				items[i] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.transform.SetParent(slots[i].transform);
				itemObj.transform.position = Vector2.zero;
				itemObj.name = itemToAdd.Slug;
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				break;
			}
        }
		//if the max ammount as been reached; 3
		if (itemData)
        {
			return;
        }
	}
	//check if item is in the inventory list and return a bool
	bool InventoryCheck(Item item)
    {
		for (int i = 0; i < items.Count; i++)
			if (items[i].ID == item.ID)
				return true;
		return false;
    }
}
