using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using ShopSystem.DataObject;

namespace ShopSystem.Editor
{
    public class ShopVisualEditorWindow : EditorWindow
    {
        private const string Title = "Shop Visual Editor";
        private const float MinimumWidth = 500f;
        private const float MinimumHeight = 300f;

        private ScrollView m_ProductContainer;
        private List<VisualElement> entries;
        private Shop m_shop;
        private SortMenu m_sortMenu;
        private FilterMenu m_filterMenu;
        public Shop ShopData => m_shop;
        public SortMenu SortMenuRef => m_sortMenu;
        public ScrollView ProductContainer => m_ProductContainer;

        [MenuItem("ShopSystem/Visual Editor")]
        public static void OpenWindow()
        {
            ShopVisualEditorWindow window = GetWindow<ShopVisualEditorWindow>(Title);
            window.minSize = new Vector2(MinimumWidth, MinimumHeight);
        }

        private void OnEnable()
        {
            m_shop = JsonUtility.FromJson<Shop>(Resources.Load<TextAsset>("shop").text);
            entries = new List<VisualElement>();
            var root = this.rootVisualElement;
            m_ProductContainer = new ScrollView();
            m_ProductContainer.style.flexGrow = new StyleFloat(1f);
            m_ProductContainer.showHorizontal = false;

            m_sortMenu = new SortMenu(this);
            root.Add(m_sortMenu.CreateLayout());
            m_filterMenu = new FilterMenu(this);
            root.Add(m_filterMenu.Create());
            m_shop.products.ForEach(item => AddProduct(item, m_ProductContainer));

            root.Add(m_ProductContainer);
        }

        public void AddProduct(Product data, VisualElement parent)
        {
            var productView = ProductEditorUICreator.CreateProductPanel(data);
            parent.Add(productView);
            entries.Add(productView);
        }

        private void AddBundle(Bundle bundle, VisualElement parent)
        {
            Box itemBox = new Box();
            itemBox.userData = bundle;
            itemBox.SetMargin(2, 2, 5, 5);

            Foldout foldOut = new Foldout();
            foldOut.text = $"{bundle.name} ${bundle.price}";
            foldOut.SetValueWithoutNotify(false);
            itemBox.Add(foldOut);
            parent.Add(itemBox);
        }
    }
}