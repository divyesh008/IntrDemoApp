using System;
using Xamarin.Forms;

namespace DemoApp.Helpers
{
    public class ViewFlipper : ContentView
    {
        /// <summary>
        /// BindableProperty for <c>FrontView</c>
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty FrontViewProperty = BindableProperty.Create<ViewFlipper, View>(
                p => p.FrontView,
                default(View),
                propertyChanged: FrontViewChanged);

        /// <summary>
        /// BindableProperty for <c>BackView</c>
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty BackViewProperty = BindableProperty.Create<ViewFlipper, View>(
                p => p.BackView,
                default(View),
                propertyChanged: BackViewChanged);

        /// <summary>
        /// BindableProperty for <c>FlipOnTap</c>
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty FlipOnTapProperty = BindableProperty.Create<ViewFlipper, bool>(
                p => p.FlipOnTap,
                true);

        /// <summary>
        /// BindableProperty for <c>FlipState</c>
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty FlipStateProperty = BindableProperty.Create<ViewFlipper, FlipState>(
                p => p.FlipState,
                FlipState.Front,
                BindingMode.TwoWay,
                propertyChanged: FlipStateChanged);

        /// <summary>
        /// BindableProperty for <c>RotationDirection</c>
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty RotationDirectionProperty = BindableProperty.Create<ViewFlipper, RotationDirection>(
                p => p.RotationDirection,
                RotationDirection.Horizontal);

        /// <summary>
        /// BindableProperty for <c>AnimationDuration</c>
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty AnimationDurationProperty = BindableProperty.Create<ViewFlipper, int>(
                p => p.AnimationDuration,
                250);

        /// <summary>
        /// Gets/Sets the front view
        /// </summary>
        [Obsolete]
        public View FrontView
        {
            get { return (View)this.GetValue(FrontViewProperty); }
            set { this.SetValue(FrontViewProperty, value); }
        }

        /// <summary>
        /// Gets/Sets the back view
        /// </summary>
        [Obsolete]
        public View BackView
        {
            get { return (View)this.GetValue(BackViewProperty); }
            set { this.SetValue(BackViewProperty, value); }
        }

        /// <summary>
        /// Gets/Sets if a flip will be perfomed when tapping anywhere inside the <c>ViewFlipper</c>
        /// </summary>
        [Obsolete]
        public bool FlipOnTap
        {
            get { return (bool)this.GetValue(FlipOnTapProperty); }
            set { this.SetValue(FlipOnTapProperty, value); }
        }

        /// <summary>
        /// Gets/Sets the current state of the <c>ViewFlipper</c>. This toggles the animation when changed
        /// </summary>
        [Obsolete]
        public FlipState FlipState
        {
            get { return (FlipState)this.GetValue(FlipStateProperty); }
            set { this.SetValue(FlipStateProperty, value); }
        }

        /// <summary>
        /// Gets/Sets the duration of the flip animation
        /// </summary>
        [Obsolete]
        public int AnimationDuration
        {
            get { return (int)this.GetValue(AnimationDurationProperty); }
            set { this.SetValue(AnimationDurationProperty, value); }
        }

        /// <summary>
        /// Gets/Sets if the flip will be in horizontal or vertical direction
        /// </summary>
        [Obsolete]
        public RotationDirection RotationDirection
        {
            get { return (RotationDirection)this.GetValue(RotationDirectionProperty); }
            set { this.SetValue(RotationDirectionProperty, value); }
        }

        /// <summary>
        /// Creates a new instance of <c>ViewFlipper</c>
        /// </summary>
        [Obsolete]
        public ViewFlipper()
        {
            this.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(this.OnTapped)
                });
        }

        /// <summary>
        /// Performs the flip
        /// </summary>
        [Obsolete]
        private async void Flip()
        {
            var animationDuration = (uint)Math.Round((double)this.AnimationDuration / 2);

            if (this.FlipState == FlipState.Front)
            {
                // Perform half of the flip
                if (this.RotationDirection == RotationDirection.Horizontal)
                {
                    await this.RotateYTo(90, animationDuration);
                }
                else
                {
                    await this.RotateXTo(90, animationDuration);
                }

                // Change the visible content
                this.Content = this.FrontView;

                // Perform second half of the flip
                if (this.RotationDirection == RotationDirection.Horizontal)
                {
                    await this.RotateYTo(0, animationDuration);
                }
                else
                {
                    await this.RotateXTo(0, animationDuration);
                }
            }
            else
            {
                // Perform half of the flip
                if (this.RotationDirection == RotationDirection.Horizontal)
                    await this.RotateYTo(90, animationDuration);
                else
                    await this.RotateXTo(90, animationDuration);

                // Change the visible content
                this.Content = this.BackView;

                // Perform second half of the flip
                if (this.RotationDirection == RotationDirection.Horizontal)
                    await this.RotateYTo(180, animationDuration);
                else
                    await this.RotateXTo(180, animationDuration);
            }
        }

        /// <summary>
        /// Sets the rotation on the back view
        /// </summary>
        [Obsolete]
        private void SetBackviewRotation()
        {
            if (this.RotationDirection == RotationDirection.Horizontal)
            {
                this.BackView.RotationX = 0;
                this.BackView.RotationY = 180;
            }
            else
            {
                this.BackView.RotationY = 0;
                this.BackView.RotationX = 180;
            }
        }

        /// <summary>
        /// When the <c>ViewFlipper</c> gets tapped
        /// </summary>
        [Obsolete]
        private void OnTapped()
        {
            if (!this.FlipOnTap)
                return;

            this.FlipState = this.FlipState == FlipState.Front ?
                FlipState.Back :
                FlipState.Front;
        }

        /// <summary>
        /// When the <c>FlipState</c> changed
        /// </summary>
        [Obsolete]
        private static void FlipStateChanged(BindableObject obj, FlipState oldValue, FlipState newValue)
        {
            ViewFlipper flipper = obj as ViewFlipper;
            if (flipper == null)
            {
                return;
            }

            flipper.Flip();
        }

        /// <summary>
        /// When the <c>FrontView</c> changed
        /// </summary>
        private static void FrontViewChanged(BindableObject obj, View oldValue, View newValue)
        {
            ViewFlipper flipper = obj as ViewFlipper;
            if (flipper == null)
            {
                return;
            }

            if (oldValue == null)
            {
                flipper.Content = newValue;
            }
        }

        /// <summary>
        /// When the <c>BackView</c> changed
        /// </summary>
        [Obsolete]
        private static void BackViewChanged(BindableObject obj, View oldValue, View newValue)
        {
            ViewFlipper flipper = obj as ViewFlipper;

            if (flipper == null || newValue == null)
            {
                return;
            }

            flipper.SetBackviewRotation();
        }

        [Obsolete]
        private static void RotationDirectionChanged(BindableObject obj, RotationDirection oldValue, RotationDirection newValue)
        {
            ViewFlipper flipper = obj as ViewFlipper;

            if (flipper == null || flipper.BackView == null)
            {
                return;
            }

            flipper.SetBackviewRotation();
        }

    }

    public enum FlipState
    {
        Front,
        Back
    }

    public enum RotationDirection
    {
        Horizontal,
        Vertical
    }
}
