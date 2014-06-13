using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// Simple delegating command, based largely on DelegateCommand from PRISM/CAL
	/// </summary>
	/// <typeparam name="T">The type for the </typeparam>
	public class DelegateCommand : ICommand
	{
		private readonly Func<object, bool> canExecuteMethod;
		private readonly Action<object> executeMethod;

		/// <summary>
		/// DelegateCommand takes an execute method and a can execute method
		/// </summary>
		/// <param name="executeMethod">execute method</param>
		/// <param name="canExecuteMethod">can execute method</param>
		public DelegateCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
		{
			this.executeMethod = executeMethod;
			this.canExecuteMethod = canExecuteMethod;
		}

		/// <summary>
		/// Delegate command takes an execute method
		/// </summary>
		/// <param name="executeMethod">execute method</param>
		public DelegateCommand(Action<object> executeMethod)
		{
			this.executeMethod = executeMethod;
			this.canExecuteMethod = (x) => true;
		}

		/// <summary>
		/// can the delegate execute
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object parameter)
		{
			return canExecuteMethod(parameter);
		}

		/// <summary>
		/// execute the delegate command
		/// </summary>
		/// <param name="parameter"></param>
		public virtual void Execute(object parameter)
		{
			executeMethod(parameter);
		}

		/// <summary>
		/// Can the delegate execute has changed
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Raise the can execute changed event
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}
}
