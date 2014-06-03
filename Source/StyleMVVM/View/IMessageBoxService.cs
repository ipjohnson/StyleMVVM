using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.View
{
	/// <summary>
	/// This service gives you easy access to the MessageDialog class as well as creates IMessageDialog wrappers
	/// </summary>
	public interface IMessageBoxService
	{
		/// <summary>
		/// Opens a message box with one button with the provided
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="buttonText"></param>
		void Notify(string message, string title = null, string buttonText = null);

		/// <summary>
		/// Show a new message dialog with the specified title and commands
		/// </summary>
		/// <param name="message">message to display</param>
		/// <param name="title">title for the message dialog</param>
		/// <param name="commands">command to show in the dialog</param>
		/// <returns>returns the command that was clicked</returns>
		Task<string> Show(string message, string title = null, params string[] commands);
	}
}
