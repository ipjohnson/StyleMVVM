using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StyleMVVM.Ultilities;
#if NETFX_CORE
using Windows.UI.Xaml;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// Allows for connecting events to methods in ViewModels
	/// </summary>
	public class EventHandlers
	{
		/// <summary>
		/// Get the list of event handlers
		/// </summary>
		/// <param name="obj">control the event handlers are attached to</param>
		/// <returns>list of event handlers</returns>
		public static EventHandlerList GetList(DependencyObject obj)
		{
			return (EventHandlerList)obj.GetValue(ListProperty);
		}

		/// <summary>
		/// Sets the list of event handlers
		/// </summary>
		/// <param name="obj">control to bind events to</param>
		/// <param name="value">list of event handlers</param>
		public static void SetList(DependencyObject obj, EventHandlerList value)
		{
			obj.SetValue(ListProperty, value);
		}

		/// <summary>
		/// List of event handlers for a particular control
		/// </summary>
		public static readonly DependencyProperty ListProperty =
			 DependencyProperty.RegisterAttached("List", typeof(EventHandlerList), typeof(EventHandlers), new PropertyMetadata(null, ListChangedCallback));

		private static void ListChangedCallback(DependencyObject dependencyObject,
															 DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			if (DesignModeUtility.DesignModeIsEnabled)
			{
				return;
			}

			EventHandlerList oldHandlerList = dependencyPropertyChangedEventArgs.OldValue as EventHandlerList;

			if (oldHandlerList != null)
			{
				foreach (EventHandlerInstance eventHandlerInstance in oldHandlerList)
				{
					eventHandlerInstance.Disconnect();
				}
			}

			EventHandlerList newList = dependencyPropertyChangedEventArgs.NewValue as EventHandlerList;

			if (newList != null)
			{
				foreach (EventHandlerInstance eventHandlerInstance in newList)
				{
					eventHandlerInstance.Connect(dependencyObject);
				}
			}
		}

		/// <summary>
		/// Get the list of methods to attach to events
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetAttach(DependencyObject obj)
		{
			return (string)obj.GetValue(AttachProperty);
		}

		/// <summary>
		/// Sets the ViewModel event handlers for the view
		/// </summary>
		/// <param name="obj">control to set the handlers on</param>
		/// <param name="value">handlers to attach</param>
		public static void SetAttach(DependencyObject obj, string value)
		{
			obj.SetValue(AttachProperty, value);
		}

		/// <summary>
		/// Attach events to view model methods using this property
		/// </summary>
		public static readonly DependencyProperty AttachProperty =
			 DependencyProperty.RegisterAttached("Attach", typeof(string), typeof(EventHandlers), new PropertyMetadata(null,AttachChangedCallback));

		private static void AttachChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			if (DesignModeUtility.DesignModeIsEnabled)
			{
				return;
			}

			string attachString = dependencyPropertyChangedEventArgs.NewValue as string;

			if (!string.IsNullOrWhiteSpace(attachString))
			{
				EventHandlerList handlerHelpers = new EventHandlerList();
				string[] attachStrings = attachString.Split(';');

				foreach (string s in attachStrings)
				{
					var trimString = s.Trim();

					if (!string.IsNullOrEmpty(trimString))
					{
						var newHandler = new EventHandlerInstance { Attach = trimString };

						handlerHelpers.Add(newHandler);
					}
				}

				SetList(dependencyObject, handlerHelpers);
			}
		}
	}
}
