using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
using System.Windows.Data;
#endif

namespace StyleMVVM.Conventions.Service
{
	/// <summary>
	/// Binding information
	/// </summary>
	public class BindingInformation
	{
		/// <summary>
		/// Binding Mode to use
		/// </summary>
		public BindingMode BindingMode { get; set; }

		/// <summary>
		/// Property to bind to
		/// </summary>
		public DependencyProperty PropertyToBindTo { get; set; }

		/// <summary>
		/// Value converter to use when setting up binding
		/// </summary>
		public IValueConverter ValueConverter { get; set; }
	}

	/// <summary>
	/// Convention service, used to provide conventions
	/// </summary>
	public interface IConventionsService
	{
		/// <summary>
		/// View Model Suffix
		/// </summary>
		string ViewModelSuffix { get; set; }

		/// <summary>
		/// Returns a list of Possible ViewModel names
		/// </summary>
		/// <param name="viewType">view type</param>
		/// <returns>list of view model names</returns>
		IEnumerable<string> ViewModelNameConventions(Type viewType);

		/// <summary>
		/// Given a particular control type and a ViewModel Property info return a dependency property to bind the property to
		/// </summary>
		/// <param name="controlType">Control type</param>
		/// <param name="propertyInfoType">property info type</param>
		/// <returns>Depednecy Property</returns>
		BindingInformation GetPropertyConvention(Type controlType, PropertyInfo propertyInfoType);
	}
}
