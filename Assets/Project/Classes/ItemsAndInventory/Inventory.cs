using System.Collections.Generic;
using Project.Interfaces;

namespace Project.Classes.ItemsAndInventory
{
    public class Inventory
    {
        private List<ICanBePickedUp> content = new List<ICanBePickedUp>();

        public List<ICanBePickedUp> Content
        {
            get
            {
                return content;
                return new List<ICanBePickedUp>(content); // todo don't know is this ok
            }

            set => content = value;
        }

        public bool TryAdd(ICanBePickedUp item)
        {
            if (false)
            {
                // todo some conditions
                return false;
            }

            content.Add(item);
            return true;
        }
    }
}