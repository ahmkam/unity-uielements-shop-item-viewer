using System.Collections.Generic;

namespace ShopSystem.DataObject
{
    [System.Serializable]
    public class Bundle
    {
        public int entryId;
        public string bundleId;
        public string name;
        public float price;
        public List<string> products;
    }
}

