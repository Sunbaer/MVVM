using Data;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundBasedGameMVVM.Events
{
    /// <summary>
    /// Event signalizes to transport the characterinformation to another view
    /// </summary>
    public class GetCharakterInformationEvent:CompositePresentationEvent<Charakter>
    {
    }
}
