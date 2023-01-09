using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Shared.data
{
	/// <summary>
	///  Classe <c>LightUser</c> Classe modélisant un LightUser
	/// </summary>
	public class LightUser

    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string image { get; set; }

		public LightUser()
		{
		}
		/// <summary>
		/// Constructeur permettant d'instancier un LightUser en prenant en paramètre un LightUer
		/// </summary>
		/// <param name="lu"></param>
		public LightUser(LightUser lu)
		{
			this.id = lu.id;
			this.username = lu.username;
			this.image = lu.image;
		}
		/// <summary>
		/// Constructeur permettant d'instancier un LightUser de manière classique
		/// </summary>
		/// <param name="id"></param>
		/// <param name="username"></param>
		/// <param name="image"></param>
		public LightUser(
            Guid id,
            string username,
            string image
        ) {
            this.id = id;
            this.username = username;
            this.image = image;
        }
    }
}
