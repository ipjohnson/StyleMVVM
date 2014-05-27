using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Media;
#endif

namespace StyleMVVM.Ultilities
{
	public static class VisualTreeHelperExtensions
	{
		public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject start)
		{
			var queue = new Queue<DependencyObject>();
			var count = VisualTreeHelper.GetChildrenCount(start);

			for (int i = 0; i < count; i++)
			{
				var child = VisualTreeHelper.GetChild(start, i);
				yield return child;
				queue.Enqueue(child);
			}

			while (queue.Count > 0)
			{
				var parent = queue.Dequeue();
				var count2 = VisualTreeHelper.GetChildrenCount(parent);

				for (int i = 0; i < count2; i++)
				{
					var child = VisualTreeHelper.GetChild(parent, i);
					yield return child;
					queue.Enqueue(child);
				}
			}
		}

		public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject start)
		{
			var parent = VisualTreeHelper.GetParent(start);

			while (parent != null)
			{
				yield return parent;
				parent = VisualTreeHelper.GetParent(parent);
			}
		}

		public static bool IsInVisualTree(this DependencyObject dob)
		{
#if NETFX_CORE
			if (Window.Current == null)
			{
				// This may happen when a picker or CameraCaptureUI etc. is open.
				return false;
			}

			return dob.GetAncestors().Contains(Window.Current.Content);
#elif WINDOWS_PHONE
			if (Application.Current.RootVisual == null)
			{
				return false;
			}

			return dob.GetAncestors().Contains(Application.Current.RootVisual);
#else
			if (Application.Current != null && Application.Current.MainWindow != null)
			{
				dob.GetAncestors().Contains(Application.Current.MainWindow);
			}

			return false;
#endif
		}

		public static Rect GetBoundingRect(this FrameworkElement dob, FrameworkElement relativeTo)
		{
#if NETFX_CORE
			if (relativeTo == null)
			{
				relativeTo = Window.Current.Content as FrameworkElement;
			}

#else
			if (relativeTo == null && Application.Current != null)
			{
				relativeTo = Application.Current.MainWindow;
			}
#endif
			if (relativeTo == null)
			{
				throw new InvalidOperationException("Element not in visual tree.");
			}

			if (dob == relativeTo)
			{
				return new Rect(0, 0, relativeTo.ActualWidth, relativeTo.ActualHeight);
			}

			var ancestors = dob.GetAncestors().ToArray();

			if (!ancestors.Contains(relativeTo))
			{
				throw new InvalidOperationException("Element not in visual tree.");
			}

#if NETFX_CORE
			var pos =
				dob
					.TransformToVisual(relativeTo)
					.TransformPoint(new Point());
			var pos2 =
				dob
					.TransformToVisual(relativeTo)
					.TransformPoint(
						new Point(
							dob.ActualWidth,
							dob.ActualHeight));

			return new Rect(pos, pos2);
#else
			var pos =
				dob
					.TransformToVisual(relativeTo)
					.Transform(new Point());
			var pos2 =
				dob
					.TransformToVisual(relativeTo)
					.Transform(
						new Point(
							dob.ActualWidth,
							dob.ActualHeight));

			return new Rect(pos, pos2);
#endif
		}
	}
}
