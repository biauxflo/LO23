namespace Client.ihm_main.DTO
{
    // TODO : Remplacer par la classe de data.
    class Game
    {
        private string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        private int nbPeople = 0;
        public int NbPeople
        {
            get => nbPeople;
            set
            {
                nbPeople = value;
            }
        }

        // 1 : partie en cours
        private int gameStatus = default;
        public int GameStatus
        {
            get => gameStatus;
            set
            {
                gameStatus = value;
            }
        }

        public Game()
        {

            }

        public Game(string name, int gameStatus)
            {
            Name = name;
            GameStatus = gameStatus;
        }
        public Game(string name, int joueurs, int jeutons, bool spec, bool chat_spec)
        {
            nom = name;
            nbJoueurs = joueurs;
            nbJoueurs = jeutons;
            spectateurs = spec;
            chat_spectateurs = chat_spec;
            
        public Game(string name, int gameStatus, int nbPeople)
        {
            Name = name;
            GameStatus = gameStatus;
            NbPeople = nbPeople;
        }

        public override bool Equals(object obj)
        {

            if(obj.GetType() != GetType())
            {
                throw new Exception("Le type testé n'est pas une partie");
            }
            else
            {
                Game game = (Game)obj;
                return nom == game.nom;
            }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}
