using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.View.Impl
{
	/// <summary>
	/// ViewBinderService calls all view binders for a View upon creation
	/// </summary>
	public class ViewBinderService : IViewBinderService
	{
		private readonly IReadOnlyCollection<IViewBinder> viewBinders;

		/// <summary>
		/// Default constructor takes a read only collection of view binders
		/// </summary>
		/// <param name="viewBinders"></param>
		public ViewBinderService(IReadOnlyCollection<IViewBinder> viewBinders)
		{
			this.viewBinders = viewBinders;
		}

		/// <summary>
		/// Bind all functionality to a View
		/// </summary>
		/// <param name="view">view object to bind</param>
		/// <param name="navigationParameter">navigation parameter for this view</param>
		public void Bind(object view, object navigationParameter)
		{
			foreach (IViewBinder viewBinder in viewBinders)
			{
				viewBinder.Bind(view, navigationParameter);
			}
		}
	}
}
