using System.Collections.Generic;

namespace ShopSystem.DataObject
{
    [System.Serializable]
    public class Shop
    {
        public List<ItemType> itemTypes;
        public List<Product> products;
        public List<Bundle> bundles;
    }
}

