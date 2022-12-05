using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.interfaces
{
	public interface IMainToGame
	{
		/// <summary>
		/// Fonction qui lance la partie depuis IMain
		/// </summary>
		/// <param name="game">Partie à lancer</param>
		void LaunchGame(Game game);
	}
}
