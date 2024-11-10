using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundBasedGameMVVM.ViewModels
{
    /// <summary>
    /// Event signalizes to swap the current view to the levelupscreen
    /// </summary>
    public class SwapToLevelUpScreenEvent: CompositePresentationEvent<LevelUpView>
    {
    }
}
