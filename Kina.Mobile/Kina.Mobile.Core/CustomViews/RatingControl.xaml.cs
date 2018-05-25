using Kina.Mobile.Core.Behaviors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RatingControl : ContentView
	{
        private RatingCellBehavior _cellBehavior;

        public readonly BindableProperty IsReadOnlyProperty =
            BindableProperty.Create("IsReadOnly", typeof(bool), typeof(RatingControl), false, BindingMode.TwoWay);
        public static readonly BindableProperty CellSizeProperty = 
            BindableProperty.Create("CellSize", typeof(GridLength), typeof(RatingControl), GridLength.Auto, BindingMode.TwoWay);
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(double), typeof(RatingControl), default(double), BindingMode.TwoWay, propertyChanged: OnValuePropertyChanged);

        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ratingControl = (RatingControl)bindable;
            ratingControl._cellBehavior.Value = (double)newValue;
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public GridLength CellSize
        {
            get => (GridLength)GetValue(CellSizeProperty);
            set => SetValue(CellSizeProperty, value);
        }

        public double Value
        {
            get => (float)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public RatingControl()
        {
            _cellBehavior = new RatingCellBehavior(this);

            InitializeComponent();

            Binding ValueBinding = new Binding(nameof(RatingCellBehavior.Value))
            {
                Source = _cellBehavior
            };

            SetBinding(ValueProperty, ValueBinding);

            Binding IsReadOnlyBinding = new Binding(nameof(RatingCellBehavior.IsReadOnly))
            {
                Source = _cellBehavior
            };

            SetBinding(IsReadOnlyProperty, IsReadOnlyBinding);

            FirstCell.Behaviors.Add(_cellBehavior);
            SecondCell.Behaviors.Add(_cellBehavior);
            ThirdCell.Behaviors.Add(_cellBehavior);
            FourthCell.Behaviors.Add(_cellBehavior);
            FifthCell.Behaviors.Add(_cellBehavior);
		}
    }
}
