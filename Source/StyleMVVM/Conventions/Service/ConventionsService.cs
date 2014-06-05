using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Grace.Logging;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
#endif

namespace StyleMVVM.Conventions.Service
{
	/// <summary>
	/// Conventions Service
	/// </summary>
	public class ConventionsService : IConventionsService
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ConventionsService()
		{
			ViewModelSuffix = "ViewModel";
		}

		/// <summary>
		/// ViewModel suffix to use for conventions, Default is ViewModel
		/// </summary>
		public string ViewModelSuffix { get; set; }

		/// <summary>
		/// Returns a list of Possible ViewModel names
		/// </summary>
		/// <param name="viewType">view type</param>
		/// <returns>list of view model names</returns>
		public IEnumerable<string> ViewModelNameConventions(Type viewType)
		{
			string viewName = viewType.Name;
			string viewNameLower = viewType.Name.ToLower();
			List<string> returnValue = new List<string>();

			if (viewNameLower.EndsWith("view"))
			{
				returnValue.Add(viewName.Substring(0, viewName.Length - 4) + ViewModelSuffix);
			}
			else if (viewNameLower.EndsWith("page"))
			{
				returnValue.Add(viewName.Substring(0, viewName.Length - 4) + ViewModelSuffix);
			}

			returnValue.Add(viewName + ViewModelSuffix);

			return returnValue;
		}

		public BindingInformation GetPropertyConvention(Type controlType, PropertyInfo propertyInfoType)
		{
			BindingInformation returnValue = GetPropertyConventionInformation(controlType, propertyInfoType);

			if (returnValue != null)
			{
				if (!propertyInfoType.CanWrite && returnValue.BindingMode == BindingMode.TwoWay)
				{
					returnValue.BindingMode = BindingMode.OneWay;
				}

				returnValue.ValueConverter = GetValueConverter(controlType, propertyInfoType, returnValue);
			}

			return returnValue;
		}

		protected virtual IValueConverter GetValueConverter(Type controlType,
			PropertyInfo propertyInfo,
			BindingInformation bindingInformation)
		{
			return null;
		}

		protected virtual BindingInformation GetPropertyConventionInformation(Type controlType, PropertyInfo propertyInfoType)
		{
			PropertyInfo propertyInfo = (PropertyInfo)propertyInfoType;
			TypeInfo controlTypeInfo = controlType.GetTypeInfo();

			if (typeof(TextBox).GetTypeInfo().IsAssignableFrom(controlTypeInfo))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = TextBox.TextProperty,
							 BindingMode = BindingMode.TwoWay
						 };
			}

			if (typeof(TextBlock).GetTypeInfo().IsAssignableFrom(controlTypeInfo))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = TextBlock.TextProperty,
							 BindingMode = BindingMode.OneWay
						 };
			}

#if NETFX_CORE
			if (typeof(PasswordBox).GetTypeInfo().IsAssignableFrom(controlTypeInfo))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = PasswordBox.PasswordProperty,
							 BindingMode = BindingMode.TwoWay
						 };
			}
#endif
			if (typeof(Image).GetTypeInfo().IsAssignableFrom(controlTypeInfo))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = Image.SourceProperty,
							 BindingMode = BindingMode.OneWay
						 };
			}

#if NETFX_CORE

			if (typeof(ToggleSwitch).GetTypeInfo().IsAssignableFrom(controlTypeInfo) &&
				 (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?)))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = ToggleSwitch.IsOnProperty,
							 BindingMode = BindingMode.TwoWay
						 };
			}

#endif

			if (typeof(ToggleButton).GetTypeInfo().IsAssignableFrom(controlTypeInfo) &&
				 (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?)))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = ToggleButton.IsCheckedProperty,
							 BindingMode = BindingMode.TwoWay
						 };
			}

			if (typeof(ButtonBase).GetTypeInfo().IsAssignableFrom(controlTypeInfo) &&
				 typeof(ICommand).GetTypeInfo().IsAssignableFrom(propertyInfo.PropertyType.GetTypeInfo()))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = ButtonBase.CommandProperty,
							 BindingMode = BindingMode.OneWay
						 };
			}

			if (typeof(ContentControl).GetTypeInfo().IsAssignableFrom(controlType.GetTypeInfo()))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = ContentControl.ContentProperty,
							 BindingMode = BindingMode.TwoWay
						 };
			}

			if (typeof(ItemsControl).GetTypeInfo().IsAssignableFrom(controlTypeInfo) &&
				 propertyInfo.PropertyType != typeof(string) &&
				 typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(propertyInfo.PropertyType.GetTypeInfo()))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = ItemsControl.ItemsSourceProperty,
							 BindingMode = BindingMode.OneWay
						 };
			}

			if (typeof(Selector).GetTypeInfo().IsAssignableFrom(controlTypeInfo))
			{
				return new BindingInformation
						 {
							 PropertyToBindTo = Selector.SelectedItemProperty,
							 BindingMode = BindingMode.TwoWay
						 };
			}

			return null;
		}
	}
}
