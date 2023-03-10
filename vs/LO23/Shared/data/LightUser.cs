using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Shared.data
{

    public class LightUser

    {
        public Guid id { get; set; }
        public string username { get; set; }    // username et login c'est la même chose 
        public string image { get; set; }

		public LightUser()
		{
		}


		public LightUser(LightUser lu)
		{
			this.id = lu.id;
			this.username = lu.username;
			this.image = lu.image;
		}

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
