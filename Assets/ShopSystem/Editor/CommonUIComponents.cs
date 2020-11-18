using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShopSystem.Editor
{
    public class CommonUIComponents
    {
        public static Label CreateLabel(string text, int fontSize, float margin = 5f)
        {
            var label = new Label(text);
            label.style.marginTop = label.style.marginBottom = margin;
            label.style.fontSize = fontSize;
            label.style.overflow = Overflow.Hidden;

            return label;
        }

        public static VisualElement CreateIcon(float width, float height, StyleBackground background)
        {
            var icon = new VisualElement();
            icon.style.alignItems = Align.Center;
            icon.style.width = width;
            icon.style.height = height;
            icon.style.backgroundImage = background;
            if (background == null)
            {
                icon.style.backgroundColor = Color.gray;
            }

            return icon;
        }

        public static ObjectField CreateImageField(string text, EventCallback<ChangeEvent<UnityEngine.Object>> changeEvent)
        {
            var imageField = new ObjectField(text);
            imageField.objectType = typeof(Texture2D);
            imageField.RegisterValueChangedCallback(changeEvent);

            return imageField;
        }

        public static ObjectField CreateImageField(string text, Action<Texture2D> changeEvent)
        {
            var imageField = new ObjectField(text);
            imageField.objectType = typeof(Texture2D);
            imageField.RegisterValueChangedCallback((image) => changeEvent((Texture2D)(image.newValue)));

            return imageField;
        }

        public static ColorField CreateColorField(string text, Color defaultColor, EventCallback<ChangeEvent<UnityEngine.Color>> changeEvent)
        {
            var titleColor = new ColorField(text);
            titleColor.value = defaultColor;
            titleColor.RegisterValueChangedCallback(changeEvent);

            return titleColor;
        }

        public static VisualElement CreateButton(string text, int fontSize,
         float width, float height, StyleBackground background, float margin = 3)
        {
            var button = new Button();
            button.text = text;
            button.style.fontSize = fontSize;
            button.style.width = width;
            button.style.height = height;

            return button;
        }
    }
}