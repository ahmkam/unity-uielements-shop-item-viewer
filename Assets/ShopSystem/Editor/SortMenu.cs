using System;
using System.Collections.Generic;
using System.Linq;
using ShopSystem.DataObject;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ShopSystem.Editor
{
    public class SortMenu
    {
        private PopupField<string> m_sortPopUp;
        private Dictionary<string, Comparison<VisualElement>> singleValueSortMap;
        private Dictionary<string, List<string>> multiSortMap;
        private ShopVisualEditorWindow m_shopVisualEditorWindow;

        public SortMenu(ShopVisualEditorWindow shopEditor)
        {
            m_shopVisualEditorWindow = shopEditor;
            Init();
        }

        public void Init()
        {
            singleValueSortMap = new Dictionary<string, Comparison<VisualElement>>();
            singleValueSortMap.Add("default", new Comparison<VisualElement>((x, y) =>
            {
                Product vX = (Product)x.userData;
                Product vY = (Product)y.userData;
                return -vY.entryId.CompareTo(vX.entryId);
            }));
            singleValueSortMap.Add("price - ascending", new Comparison<VisualElement>((x, y) =>
            {
                Product vX = (Product)x.userData;
                Product vY = (Product)y.userData;
                double yPrice = (double)(vY.price);
                double xPrice = (double)(vX.price);
                return -yPrice.CompareTo(xPrice);
            }));
            singleValueSortMap.Add("price - descending", new Comparison<VisualElement>((x, y) =>
            {
                Product vX = (Product)x.userData;
                Product vY = (Product)y.userData;
                double yPrice = (double)(vY.price);
                double xPrice = (double)(vX.price);
                return yPrice.CompareTo(xPrice);
            }));
            singleValueSortMap.Add("name - ascending", new Comparison<VisualElement>((x, y) =>
            {
                Product vX = (Product)x.userData;
                Product vY = (Product)y.userData;
                return -vY.name.CompareTo(vX.name);
            }));
            singleValueSortMap.Add("name - descending", new Comparison<VisualElement>((x, y) =>
            {
                Product vX = (Product)x.userData;
                Product vY = (Product)y.userData;
                return vY.name.CompareTo(vX.name);
            }));

            multiSortMap = new Dictionary<string, List<string>>();
            multiSortMap.Add("Coins > Gems > Tickets", new List<string>() { "coins", "gems", "tickets" });
            multiSortMap.Add("Tickets > Coins > Gems", new List<string>() { "tickets", "coins", "gems" });
            multiSortMap.Add("Gems > Tickets > Coins", new List<string>() { "gems", "tickets", "coins" });
        }

        public Box CreateLayout()
        {
            var popupFieldValues = new List<string>();
            popupFieldValues.AddRange(singleValueSortMap.Keys.ToList());
            popupFieldValues.AddRange(multiSortMap.Keys.ToList());

            m_sortPopUp = new PopupField<string>("Sort By", popupFieldValues, popupFieldValues[0]);
            m_sortPopUp.RegisterValueChangedCallback((option) => ApplySort());
            m_sortPopUp.SetMargin(10, 10, 0, 0);
            m_sortPopUp.style.maxWidth = 500;

            var sortMenuBox = new Box();
            sortMenuBox.SetPadding(0, 0, 5, 5);
            sortMenuBox.SetMargin(5, 0, 5, 5);
            sortMenuBox.Add(m_sortPopUp);

            return sortMenuBox;
        }

        public void ApplySort()
        {
            var productContainer = m_shopVisualEditorWindow.ProductContainer;
            if (productContainer == null)
                UnityEngine.Debug.Log("container is null");

            string key = m_sortPopUp.value;
            if (singleValueSortMap.ContainsKey(key))
            {
                productContainer.Sort(singleValueSortMap[key]);
            }
            else if (multiSortMap.ContainsKey(key))
            {
                SortByItemType(multiSortMap[key]);
            }
        }

        public void SortByItemType(List<string> order)
        {
            var productContainer = m_shopVisualEditorWindow.ProductContainer;
            List<VisualElement> items = productContainer.Children().ToList();
            List<VisualElement> result = new List<VisualElement>();

            order.ForEach(x =>
            {
                List<VisualElement> temp = items.Where(element => ((Product)(element.userData)).itemTypeId == x).ToList();
                result.AddRange(temp);
            });

            productContainer.Clear();
            result.ForEach(r => productContainer.Add(r));
        }
    }
}
