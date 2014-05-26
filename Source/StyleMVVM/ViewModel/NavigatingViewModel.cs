using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.View;

namespace StyleMVVM.ViewModel
{
	public class NavigatingViewModel : BaseViewModel, INavigationViewModel
	{
		private string syncKey = null;
		protected bool attemptToSyncOnLeave = true;
		protected bool attemptToResumeOnNavigate = true;

		public virtual object NavigationParameter { get; set; }


		public void NavigatedTo(object sender, StyleNavigationEventArgs e)
		{
			NavigationParameter = e.Parameter;

			OnNavigatedTo(sender, e);
		}

		public void NavigatedFrom(object sender, StyleNavigationEventArgs e)
		{
			OnNavigatedFrom(sender, e);
		}


		public Task NavigatingFrom(object sender, StyleNavigatingCancelEventArgs e)
		{
			return InternalNavigatingFrom(sender, e);
		}

		protected virtual async Task InternalNavigatingFrom(object sender, StyleNavigatingCancelEventArgs e)
		{
			await OnNavigatingFrom(sender, e);
		}

		protected virtual void OnNavigatedTo(object sender, StyleNavigationEventArgs e)
		{
		}

		protected virtual void OnNavigatedFrom(object sender, StyleNavigationEventArgs e)
		{
		}

		protected virtual async Task OnNavigatingFrom(object sender, StyleNavigatingCancelEventArgs e)
		{

		}
	}
}
