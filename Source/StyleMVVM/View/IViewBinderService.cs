using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.View
{
	/// <summary>
	/// Service provides binding services for a View
	/// </summary>
	public interface IViewBinderService
	{
		/// <summary>
		/// Bind all functionality to a View
		/// </summary>
		/// <param name="view">view object to bind</param>
		/// <param name="navigationParameter">navigation parameter for this view</param>
		void Bind(object view, object navigationParameter);
	}
}
