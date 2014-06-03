using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Popups;
using Windows.ApplicationModel.Resources;
#else
#endif

namespace StyleMVVM.View.Impl
{
	/// <summary>
	/// Message Box Service
	/// </summary>
	public class MessageBoxService : IMessageBoxService
	{
		/// <summary>
		/// Opens a message box with one button with the provided
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="buttonText"></param>
		public async void Notify(string message, string title = null, string buttonText = null)
		{
			string[] commands = null;

			if (buttonText != null)
			{
				commands = new[] { buttonText };
			}

			await InternalShow(message, title, commands);
		}

		/// <summary>
		/// Show a new message dialog with the specified title and commands
		/// </summary>
		/// <param name="message">message to display</param>
		/// <param name="title">title for the message dialog</param>
		/// <param name="commands">command to show in the dialog</param>
		/// <returns>returns the command that was clicked</returns>
		public Task<string> Show(string message, string title = null, params string[] commands)
		{
			return InternalShow(message, title, commands);
		}

#if NETFX_CORE

		private async Task<string> InternalShow(string message, string title = null, params string[] commands)
		{
			MessageDialog newDialog = null;

			if (!string.IsNullOrEmpty(title))
			{
				newDialog = new MessageDialog(message, title);
			}
			else
			{
				newDialog = new MessageDialog(message);
			}

			if (commands != null)
			{
				foreach (string commandName in commands)
				{
					newDialog.Commands.Add(new UICommand(commandName));
				}
			}
			else
			{
				newDialog.Commands.Add(new UICommand("Ok"));
			}

			IUICommand command = await newDialog.ShowAsync();

			return command.Label;
		}
#else
		private async Task<string> InternalShow(string message, string title = null, params string[] commands)
		{
			throw new NotImplementedException("Not implemented yet");

			return "Ok";
		}

#endif
		}
}
