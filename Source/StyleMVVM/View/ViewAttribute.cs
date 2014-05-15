using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection.Attributes.Interfaces;

namespace StyleMVVM.View
{
	/// <summary>
	/// Exports a view by class name
	/// </summary>
	public class ViewAttribute : Attribute, IExportAttribute
	{
		/// <summary>
		/// If you want to provide a different name other than the default
		/// </summary>
		public string Name { get; set; }

		public IEnumerable<string> ProvideExportNames(Type attributedType)
		{
			yield return Name ?? attributedType.Name;
		}

		public IEnumerable<Type> ProvideExportTypes(Type attributedType)
		{
			return new Type[0];
		}
	}
}
