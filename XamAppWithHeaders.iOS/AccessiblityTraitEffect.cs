using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static XamAppWithHeaders.Accessibility;

[assembly: ResolutionGroupName("XamAppWithHeaders")]
[assembly: ExportEffect(typeof(XamAppWithHeaders.iOS.AccessibilityTraitEffect), "AccessibilityTraitEffect")]

namespace XamAppWithHeaders.iOS
{
    public class AccessibilityTraitEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            AddAccessibilityTraits();
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == AccessibilityTraitsProperty.PropertyName)
            {
                AddAccessibilityTraits();
            }
            else
            {
                base.OnElementPropertyChanged(args);
            }
        }

        void AddAccessibilityTraits()
        {
            var traits = Control.AccessibilityTraits;

            var newTraits = GetAccessibilityTraits(Element);

            if ((newTraits & AccessibilityTrait.Header) > 0) traits |= UIAccessibilityTrait.Header;

            Control.AccessibilityTraits = traits;
        }
    }
}
