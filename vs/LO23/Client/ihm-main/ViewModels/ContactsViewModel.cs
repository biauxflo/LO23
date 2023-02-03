using GalaSoft.MvvmLight.Command;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
	class ContactsViewModel
	{
		/// <summary>
		/// Liste des contacts
		/// </summary>
		public ObservableCollection<LightUser> Contacts
		{
			get;set;
		}

		public ICommand ContactCommand
		{
			get;set;
		}

		public ContactsViewModel()
		{
			ContactCommand = new RelayCommand(ContactClick, true);

			Contacts = new ObservableCollection<LightUser>
			{
				new LightUser(Guid.NewGuid(), "Florestan", string.Empty),
				new LightUser(Guid.NewGuid(), "Gauthier", string.Empty),
			};
		}

		/// <summary>
		/// Action réalisée lors du clic sur un contact.
		/// </summary>
		private void ContactClick()
		{
			Window view = Application.Current.MainWindow;
			MessageBox.Show(view, "La fonction n'est pas encore implémantée", "W.I.P", MessageBoxButton.OK, MessageBoxImage.None);
		}
	}
}
