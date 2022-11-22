using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ihm_main.DTO
{
    internal class Game
    {
        private string nom = string.Empty;
        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
            }
        }
        private int nbJoueurs = 0;
        public int NbJoueurs
        {
            get => nbJoueurs;
            set
            {
                nbJoueurs = value;
            }
        }
        private int nbJeutons = 0;
        public int NbJeutons
        {
            get => nbJeutons;
            set
            {
                nbJeutons = value;
            }
        }
        private bool spectateurs = false;
        public bool Spectateurs
        {
            get => spectateurs;
            set
            {
                spectateurs = value;
            }
        }
        private bool chat_spectateurs = false;
        public bool Chat_spectateurs
        {
            get => chat_spectateurs;
            set
            {
                chat_spectateurs = value;
            }
        }
        public Game(string name, int joueurs,int jeutons, bool spec,bool chat_spec)
        {
            nom = name;
            nbJoueurs = joueurs;
            nbJoueurs = jeutons;
            spectateurs = spec;
            chat_spectateurs = chat_spec;
            
        }

        public Game()
        {

        }

        {
        }

        {
        }
    }
}
