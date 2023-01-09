using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data {

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


		public static LightUser ToLightUser(User user)
		{
			return new LightUser(user.id, user.username, user.image);
		}
	}
}
