using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
    public interface Interface_Comm_calls_Data_Server
    {
        LightUser getUser();
        List<LightUser> registerUser(LightUser lightUser);
        void removeUser(int idJoueur);

    }
}

