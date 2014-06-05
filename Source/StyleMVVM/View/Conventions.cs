using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// Conventions binding at design time
	/// </summary>
	public class Conventions
	{
		/// <summary>
		/// Bind at design time
		/// </summary>
		/// <param name="obj">dependency object</param>
		/// <returns></returns>
		public static bool GetBindAtDesignTime(DependencyObject obj)
		{
			return (bool)obj.GetValue(BindAtDesignTimeProperty);
		}

		/// <summary>
		/// Sets the bind at design time property
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="value"></param>
		public static void SetBindAtDesignTime(DependencyObject obj, bool value)
		{
			obj.SetValue(BindAtDesignTimeProperty, value);
		}

		/// <summary>
		/// Bind the viewmodel to the view at design time
		/// </summary>
		public static readonly DependencyProperty BindAtDesignTimeProperty =
			 DependencyProperty.RegisterAttached("BindAtDesignTime", typeof(bool), typeof(Conventions), new PropertyMetadata(false, BindAtDesignTimeChanged));

		/// <summary>
		/// Bind the view model to the view at design time
		/// </summary>
		/// <param name="d">view to bind</param>
		/// <param name="e">property change event</param>
		private static void BindAtDesignTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool designTime = (bool)e.NewValue;

			if(designTime)
			{

			}
			else
			{

			}
		}
	}
}
