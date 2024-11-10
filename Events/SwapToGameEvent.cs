using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Views;
using System.Windows.Controls;

namespace RoundBasedGameMVVM.Events
{
    /// <summary>
    /// Event signalizes to swap the current to the Game
    /// </summary>
    public class SwapToGameEvent : CompositePresentationEvent<BattleView>
    {

    }
}
