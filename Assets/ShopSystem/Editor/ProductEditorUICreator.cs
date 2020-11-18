using ShopSystem.DataObject;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShopSystem.Editor
{
    public class ProductEditorUICreator
    {
        public static VisualElement CreateProductPanel(Product data)
        {
            Box itemBox = new Box();
            itemBox.userData = data;
            itemBox.SetMargin(2, 2, 5, 5);

            Foldout foldOut = new Foldout();
            foldOut.text = $"{data.name} \t\t Amount:{data.amount} \t\t Price:${data.price}";
            foldOut.SetValueWithoutNotify(false);
            itemBox.Add(foldOut);

            var mainPanel = new VisualElement();
            mainPanel.SetPadding(5, 5, 0, 0);
            mainPanel.SetMargin(5, 5, 0, 0);
            mainPanel.SetBorderWidth(1);
            mainPanel.SetBorderColor(Color.black);

            var productView = CreateProductView(data, true);
            ProductViewData viewData = (ProductViewData)productView.userData;
            var productBox = viewData.m_box;
            var title = viewData.m_title;
            var amount = viewData.m_amount;
            var icon = viewData.m_icon;

            var editPanel = new VisualElement();
            var titleColorPicker = CommonUIComponents.CreateColorField("Title", Color.gray, (c) => title.style.color = c.newValue);
            editPanel.Add(titleColorPicker);
            var amountColorPicker = CommonUIComponents.CreateColorField("Amount", Color.gray, (c) => amount.style.color = c.newValue);
            editPanel.Add(amountColorPicker);
            var iconImageField = CommonUIComponents.CreateImageField("Icon", (image) =>
            {
                Texture2D tex = (Texture2D)image.newValue;
                if (tex != null)
                {
                    icon.style.backgroundImage = tex;
                    icon.style.backgroundColor = Color.clear;
                }
                else
                {
                    icon.style.backgroundImage = null;
                    icon.style.backgroundColor = Color.gray;
                }
            });
            editPanel.Add(iconImageField);

            var backgroundField = CommonUIComponents.CreateImageField("Background", (tex) =>
            {
                productBox.style.backgroundImage = tex;
                productBox.style.backgroundColor = Color.clear;
                productBox.SetBorderWidth(1);
                productBox.SetBorderColor(Color.black);
            });
            editPanel.Add(backgroundField);
            editPanel.style.flexDirection = FlexDirection.ColumnReverse;
            mainPanel.Add(editPanel);

            mainPanel.Add(productBox);
            mainPanel.style.flexDirection = FlexDirection.Row;
            foldOut.Add(mainPanel);

            return itemBox;
        }

        public static VisualElement CreateProductView(Product data, bool showButton = true)
        {
            var productBox = new VisualElement();
            productBox.SetBorderWidth(1f);
            productBox.SetBorderColor(Color.black);
            productBox.style.alignItems = Align.Center;
            productBox.style.width = 120;
            productBox.style.height = showButton ? 170 : 130;

            var title = CommonUIComponents.CreateLabel(data.name, 14);
            productBox.Add(title);
            var icon = CommonUIComponents.CreateIcon(72, 72, null);
            productBox.Add(icon);
            var amount = CommonUIComponents.CreateLabel(data.amount.ToString(), 13, 8);
            productBox.Add(amount);
            if (showButton)
            {
                var buyButton = CommonUIComponents.CreateButton($"${data.price}", 12, 50, 24, null);
                productBox.Add(buyButton);
            }
            ProductViewData viewData = new ProductViewData()
            {
                m_box = productBox,
                m_title = title,
                m_icon = icon,
                m_amount = amount
            };
            productBox.userData = viewData;

            return productBox;
        }
    }
}