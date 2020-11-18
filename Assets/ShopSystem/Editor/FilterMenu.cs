using System.Collections.Generic;
using System.Linq;
using ShopSystem.DataObject;
using UnityEngine.UIElements;

namespace ShopSystem.Editor
{
    public class FilterMenu
    {
        private ShopVisualEditorWindow m_shopVisualEditorWindow;

        public FilterMenu(ShopVisualEditorWindow shopEditor) => m_shopVisualEditorWindow = shopEditor;

        public Box Create()
        {
            var shop = m_shopVisualEditorWindow.ShopData;
            var mainContainer = m_shopVisualEditorWindow.ProductContainer;
            var sortMenu = m_shopVisualEditorWindow.SortMenuRef;

            var filterBox = new Box();
            filterBox.SetMargin(5, 5, 5, 5);
            List<Toggle> toggles = new List<Toggle>();
            shop.itemTypes.ForEach(x =>
            {
                var toggle = new Toggle(x.name);
                toggle.RegisterValueChangedCallback((a) =>
                {
                    bool isOn = a.newValue;
                    if (!isOn)
                    {
                        List<VisualElement> elements = mainContainer.Children().ToList();
                        elements = elements.Where(e => x.itemTypeId == ((Product)e.userData).itemTypeId).ToList();
                        elements.ForEach(item => mainContainer.Remove(item));
                    }
                    else
                    {
                        List<Product> removedProducts = shop.products.Where(p => p.itemTypeId == x.itemTypeId).ToList();
                        removedProducts.ForEach(rem => m_shopVisualEditorWindow.AddProduct(rem, mainContainer));
                        sortMenu.ApplySort();
                    }
                });
                toggle.SetValueWithoutNotify(true);
                filterBox.Add(toggle);
                toggles.Add(toggle);
            });

            return filterBox;
        }
    }
}
