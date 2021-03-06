﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Zodiacon.WPF.Utilities;

namespace Zodiacon.WPF.Behaviors {
    /// <summary>
    /// Provides a setter for selected item in TreeView
    /// </summary>
    public sealed class TreeViewSelectedItemBehavior : Behavior<TreeView> {
        protected override void OnAttached() {
            base.OnAttached();

            AssociatedObject.SelectedItemChanged += AssociatedObject_SelectedItemChanged;
        }

        protected override void OnDetaching() {
            AssociatedObject.SelectedItemChanged -= AssociatedObject_SelectedItemChanged;

            base.OnDetaching();
        }

        private void AssociatedObject_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            SelectedItem = AssociatedObject.SelectedItem;
        }

        public object SelectedItem {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(TreeViewSelectedItemBehavior), 
				new PropertyMetadata(null, OnSelectedItemChanged));

		private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var tree = (d as TreeViewSelectedItemBehavior).AssociatedObject;
			Debug.Assert(tree != null);

			if (e.NewValue != null) {
				if (tree.ItemContainerGenerator.ContainerFromItem(e.NewValue) is TreeViewItem item)
					item.IsSelected = true;
			}
		}
	}
}
