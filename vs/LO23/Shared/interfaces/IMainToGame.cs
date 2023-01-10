using Shared.data;


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
