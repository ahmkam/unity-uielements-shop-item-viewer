using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShopSystem.Editor
{
    public static class UIElementsExtension
    {
        public static void SetMargin(this VisualElement element, float top, float bottom, float left, float right)
        {
            element.style.marginTop = top;
            element.style.marginBottom = bottom;
            element.style.marginLeft = left;
            element.style.marginRight = right;
        }

        public static void SetPadding(this VisualElement element, float top, float bottom, float left, float right)
        {
            element.style.paddingTop = top;
            element.style.paddingBottom = bottom;
            element.style.paddingLeft = left;
            element.style.paddingRight = right;
        }

        public static void SetBorderWidth(this VisualElement element, float thickeness)
        {
            element.style.borderTopWidth = thickeness;
            element.style.borderBottomWidth = thickeness;
            element.style.borderLeftWidth = thickeness;
            element.style.borderRightWidth = thickeness;
        }

        public static void SetBorderColor(this VisualElement element, Color color)
        {
            element.style.borderTopColor = color;
            element.style.borderBottomColor = color;
            element.style.borderLeftColor = color;
            element.style.borderRightColor = color;
        }
    }
}