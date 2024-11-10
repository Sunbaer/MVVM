using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.ViewModels;
using RoundBasedGameMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundBasedGameMVVM.Events
{
    /// <summary>
    /// Event signalizes to swap the current view to the start screen
    /// </summary>
    public class SwapToStartScreenEvent :CompositePresentationEvent<GameStartView>
    {
    }
}
