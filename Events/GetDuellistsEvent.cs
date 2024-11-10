using Data;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RoundBasedGameMVVM.Events
{
    /// <summary>
    /// Event signalizes to transport the duellistinformation to another view
    /// </summary>
    public class GetDuellistsEvent: CompositePresentationEvent<Duellists>
    {

    }
}
