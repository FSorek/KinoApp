using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Behaviors
{
    class StarBehavior : BehaviorBase<View>
    {
        TapGestureRecognizer tapRecognizer;

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

        public string StarName
        {
            get { return (string)GetValue(StarNameProperty); }
            set { SetValue(StarNameProperty, value); }
        }

        public StarBehavior()
        {
        }

        public static void AddOrUpdateGroup(string newGroupName, StarBehavior behavior)
        {
            List<StarBehavior> group;
            if (!starGroups.ContainsKey(newGroupName))
            {
                group = new List<StarBehavior>
                {
                    behavior
                };
                starGroups.Add(newGroupName, group);
            }
            else
            {
                group = starGroups[newGroupName];
                var oldBehavior = group.FirstOrDefault(b => b.StarName.Equals(behavior.StarName));
                if(oldBehavior != null)
                {
                    var index = group.IndexOf(oldBehavior);
                    group[index] = behavior;
                }
                else
                {
                    group.Add(behavior);
                }
            }
        }

        public static void RemoveFrom(string groupName, StarBehavior behavior)
        {
            List<StarBehavior> group = starGroups[groupName];
            group.Remove(behavior);
            if(group.Count == 0)
            {
                starGroups.Remove(groupName);
            }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += OnTapped;
            bindable.GestureRecognizers.Add(tapRecognizer);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            bindable.GestureRecognizers.Remove(tapRecognizer);
            tapRecognizer.Tapped -= OnTapped;
            tapRecognizer = null;
            base.OnDetachingFrom(bindable);
        }

        private static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (StarBehavior)bindable;
            string oldGroupName = (string)oldValue;
            string newGroupName = (string)newValue;

            if (String.IsNullOrEmpty(newGroupName))
            {
                throw new ArgumentNullException("GroupName", "Group name cannot be null or Empty");
            }

            if (!String.IsNullOrEmpty(oldGroupName))
            {
                RemoveFrom(oldGroupName, behavior);
            }

            AddOrUpdateGroup(newGroupName, behavior);
        }

        private void OnTapped(object sender, EventArgs eventArgs)
        {
            // To fire OnIsMarkedChanged, maybe i should resign from this property changed method?
            if (IsMarked)
            {
                IsMarked = false;
            }

            IsMarked = true;
        }

        private static void OnIsMarkedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StarBehavior behavior = (StarBehavior)bindable;

            if ((bool)newValue)
            {
                string groupName = behavior.GroupName;
                List<StarBehavior> group = starGroups[groupName];
                bool reached = false;
                foreach(var b in group)
                {
                    if (!reached)
                    {
                        b.IsMarked = true;
                        if (b == behavior)
                        {
                            reached = true;
                        }
                    }
                    else
                    {
                        b.IsMarked = false;
                    }
                }
            }
        }

        //void OnTapRecognized(object sender, object eventArgs)
        //{
        //    if(IsMarked)
        //    {
        //        IsMarked = false;
        //    }
        //    IsMarked = true;

        //    // Workaround, to make sure THIS star is marked.
        //    if (!IsMarked)
        //    {
        //        IsMarked = true;
        //    }
        //}

        //private static void OnIsMarkedChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    StarBehavior behavior = (StarBehavior)bindable;

        //    if ((bool)newValue)
        //    {
        //        string groupName = behavior.GroupName;
        //        List<StarBehavior> behaviors = null;
        //        if (String.IsNullOrEmpty(groupName))
        //        {
        //            behaviors = defaultBehaviors;
        //        }
        //        else
        //        {
        //            behaviors = starGroups[groupName];
        //        }

        //        bool itemReached = false;
        //        int count = 0;
        //        int rating = 0;

        //        foreach (var item in behaviors)
        //        {
        //            if (!itemReached)
        //            {
        //                count++;
        //                item.IsMarked = true;
        //                if (item == behavior)
        //                {
        //                    itemReached = true;
        //                    rating = count;
        //                }
        //            }
        //            else
        //            {
        //                item.IsMarked = false;
        //            }

        //            item.Rating = rating;
        //        }
        //    }
        //}
    }
}
