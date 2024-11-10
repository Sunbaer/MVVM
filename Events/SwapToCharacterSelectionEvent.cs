using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundBasedGameMVVM.Events
{
    /// <summary>
    /// Event signalizes to swap the current to character selection
    /// </summary>
    public class SwapToCharacterSelectionEvent:CompositePresentationEvent<CharacterSelectionView>
    {
    }
}
