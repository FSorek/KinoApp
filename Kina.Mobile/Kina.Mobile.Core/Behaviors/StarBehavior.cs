using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Behaviors
{
    class StarBehavior : BehaviorBase<View>
    {
        TapGestureRecognizer tapRecognizer;

        static List<StarBehavior> defaultBehaviors = new List<StarBehavior>();
        static Dictionary<string, List<StarBehavior>> starGroups = new Dictionary<string, List<StarBehavior>>();

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create("GroupName",
                typeof(string),
                typeof(StarBehavior),
                null,
                propertyChanged: OnGroupNameChanged);

        public static readonly BindableProperty IsMarkedProperty =
            BindableProperty.Create("IsMarked",
                typeof(bool),
                typeof(StarBehavior),
                false,
                propertyChanged: OnIsMarkedChanged);

        public static readonly BindableProperty RatingProperty =
            BindableProperty.Create("Rating",
                typeof(int),
                typeof(StarBehavior),
                default(int));

        public static readonly BindableProperty StarNameProperty =
            BindableProperty.Create("StarName",
                typeof(string),
                typeof(StarBehavior),
                null);

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        public bool IsMarked
        {
            get { return (bool)GetValue(IsMarkedProperty); }
            set { SetValue(IsMarkedProperty, value); }
        }

        public int Rating
        {
            get { return (int)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public string StarName
        {
            get { return (string)GetValue(StarNameProperty); }
            set { SetValue(StarNameProperty, value); }
        }

        public StarBehavior()
        {
            defaultBehaviors.Add(this);
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += OnTapRecognized;
            bindable.GestureRecognizers.Add(tapRecognizer);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            bindable.GestureRecognizers.Remove(tapRecognizer);
            tapRecognizer.Tapped -= OnTapRecognized;
            base.OnDetachingFrom(bindable);
        }

        void OnTapRecognized(object sender, object eventArgs)
        {
            if(IsMarked)
            {
                IsMarked = false;
            }
            IsMarked = true;

            // Workaround, to make sure THIS star is marked.
            if (!IsMarked)
            {
                IsMarked = true;
            }
        }

        private static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (StarBehavior)bindable;
            string oldGroupName = (string)oldValue;
            string newGroupName = (string)newValue;

            if (String.IsNullOrEmpty(oldGroupName))
            {
                defaultBehaviors.Remove(behavior);
            }
            else
            {
                List<StarBehavior> behaviors = starGroups[oldGroupName];
                behaviors.Remove(behavior);
                if (behaviors.Count == 0)
                {
                    starGroups.Remove(oldGroupName);
                }
            }

            if (String.IsNullOrEmpty(newGroupName))
            {
                AddOrUpdateBehavior(defaultBehaviors, behavior);
            }
            else
            {
                List<StarBehavior> behaviors = null;
                if (starGroups.ContainsKey(newGroupName))
                {
                    behaviors = starGroups[newGroupName];
                }
                else
                {
                    behaviors = new List<StarBehavior>();
                    starGroups.Add(newGroupName, behaviors);
                }

                AddOrUpdateBehavior(behaviors, behavior);
            }
        }

        private static void AddOrUpdateBehavior(List<StarBehavior> behaviors, StarBehavior behavior)
        {
            var behaviorExists = behaviors.FirstOrDefault(b => b.StarName.Equals(behavior.StarName));
            if (behaviorExists != null)
            {
                var index = behaviors.IndexOf(behaviorExists);
                if (index != -1)
                {
                    behaviors[index] = behavior;
                }
            }
            else
            {
                behaviors.Add(behavior);
            }
        }

        private static void OnIsMarkedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StarBehavior behavior = (StarBehavior)bindable;

            if ((bool)newValue)
            {
                string groupName = behavior.GroupName;
                List<StarBehavior> behaviors = null;
                if (String.IsNullOrEmpty(groupName))
                {
                    behaviors = defaultBehaviors;
                }
                else
                {
                    behaviors = starGroups[groupName];
                }

                bool itemReached = false;
                int count = 0;
                int rating = 0;

                foreach (var item in behaviors)
                {
                    if (!itemReached)
                    {
                        count++;
                        item.IsMarked = true;
                        if (item == behavior)
                        {
                            itemReached = true;
                            rating = count;
                        }
                    }
                    else
                    {
                        item.IsMarked = false;
                    }

                    item.Rating = rating;
                }
            }
        }
    }
}
