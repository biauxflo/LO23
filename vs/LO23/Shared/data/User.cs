using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data {
	/// <summary>
	/// Classe <c>User</c> Modélise un utilisateur ('user' en anglais), hérite de la classe LightUser
	/// </summary>
	public class User : LightUser
    {
        public string password { get; set; }
        public bool status { get; set; }
        public string firstname { get; set; }   // Once created the user should not be able to change his firstname
        public string lastname { get; set; }    // Once created the user should not be able to change his lastname
        public int age { get; set; }    // Once created the user should not be able to change his age


        public User()
        {
        }
		/// <summary>
		/// Permet de construire un User
		/// </summary>
		/// <param name="id"></param>
		/// <param name="username"></param>
		/// <param name="image"></param>
		/// <param name="password"></param>
		/// <param name="status"></param>
		/// <param name="firstname"></param>
		/// <param name="lastname"></param>
		/// <param name="age"></param>
        public User(
            Guid id,
            string username,
            string image,
            string password,
            bool status,
            string firstname,
            string lastname,
            int age
        ) : base(id, username, image) 
        {
            this.password = password;
            this.status = status;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
        }

		/// <summary>
		/// Permet de convertir un User en LightUser
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static LightUser ToLightUser(User user)
		{
			return new LightUser(user.id, user.username, user.image);
		}
	}
}
