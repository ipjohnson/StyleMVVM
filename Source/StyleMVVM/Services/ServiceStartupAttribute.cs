using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection.Attributes.Interfaces;

namespace StyleMVVM.Services
{
	/// <summary>
	/// When applied to a service it will be created upon startup
	/// </summary>
	public class ServiceStartupAttribute : Attribute, IExportMetadataAttribute
	{
		/// <summary>
		/// Startup after the app has been launched
		/// </summary>
		public bool AfterLaunch { get; set; }

		/// <summary>
		/// Returns a metadata pair that tells the container to launch it
		/// </summary>
		/// <param name="attributedType"></param>
		/// <returns></returns>
		public KeyValuePair<string, object> ProvideMetadata(Type attributedType)
		{
			return AfterLaunch ? 
						new KeyValuePair<string, object>(MetadataConstants.ServiceStartupAfterLaunch, true) : 
						new KeyValuePair<string, object>(MetadataConstants.ServiceStartupBeforeLaunch, true);
		}
	}
}
