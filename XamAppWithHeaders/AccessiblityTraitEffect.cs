﻿using System;
using System.Linq;
using Xamarin.Forms;

namespace XamAppWithHeaders
{
    public static class Accessibility
    {
        [Flags]
        public enum AccessibilityTrait
        {
            None,
            Header
        }

        public static readonly BindableProperty AccessibilityTraitsProperty =
            BindableProperty.CreateAttached("AccessibilityTraits", typeof(AccessibilityTrait), typeof(Accessibility), AccessibilityTrait.None, propertyChanged: OnAccessibilityTraitsChanged);

        public static AccessibilityTrait GetAccessibilityTraits(BindableObject view)
        {
            return (AccessibilityTrait)view.GetValue(AccessibilityTraitsProperty);
        }

        public static void SetAccessibilityTraits(BindableObject view, AccessibilityTrait value)
        {
            view.SetValue(AccessibilityTraitsProperty, value);
        }

        static void OnAccessibilityTraitsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
            {
                return;
            }

            var newTraits = (AccessibilityTrait)newValue;
            var hasTraits = newTraits != AccessibilityTrait.None;
            
            if (hasTraits)
            {
                if (!view.Effects.OfType<AccessibilityTraitEffect>().Any())
                {
                    view.Effects.Add(new AccessibilityTraitEffect());
                }
            }
            else
            {
                var accessibilityTrait = view.Effects.OfType<AccessibilityTraitEffect>().FirstOrDefault();
                if (accessibilityTrait != null)
                {
                    view.Effects.Remove(accessibilityTrait);
                }
            }
        }

        public class AccessibilityTraitEffect : RoutingEffect
        {
            public AccessibilityTraitEffect() : base("XamAppWithHeaders.AccessibilityTraitEffect")
            {
            }
        }
    }
}
