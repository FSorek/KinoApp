using Kina.Mobile.Core.CustomViews;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Behaviors
{
    public class RatingCellBehavior : BehaviorBase<RatingCell>
    {
        private TapGestureRecognizer _tapRecognizer;
        private List<RatingCell> _cells;

        public static readonly BindableProperty IsReadOnlyProperty =
            BindableProperty.Create("IsReadOnly", typeof(bool), typeof(RatingCellBehavior), false);
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(double), typeof(RatingCellBehavior), default(double), propertyChanged: OnValuePropertyChanged);

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public RatingCellBehavior()
        {
            _cells = new List<RatingCell>();
        }

        protected override void OnAttachedTo(RatingCell bindable)
        {
            base.OnAttachedTo(bindable);
            _tapRecognizer = new TapGestureRecognizer();
            _tapRecognizer.Tapped += OnTapped;
            _cells.Add(bindable);
            bindable.GestureRecognizers.Add(_tapRecognizer);
        }

        protected override void OnDetachingFrom(RatingCell bindable)
        {
            bindable.GestureRecognizers.Remove(_tapRecognizer);
            _cells.Remove(bindable);
            _tapRecognizer.Tapped -= OnTapped;
            _tapRecognizer = null;
            base.OnDetachingFrom(bindable);
        }

        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (RatingCellBehavior)bindable;
            foreach (var cell in behavior._cells)
            {
                if (behavior.Value > cell.CellNumber)
                {
                    cell.IsMarked = true;
                }
                else
                {
                    cell.IsMarked = false;
                }
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (!IsReadOnly)
            {
                var tapped = (RatingCell)sender;
                var newValue = 0.0;
                foreach (var cell in _cells)
                {
                    if (cell.CellNumber <= tapped.CellNumber)
                    {
                        newValue++;
                    }
                }

                Value = newValue;
            }
        }
    }
}
