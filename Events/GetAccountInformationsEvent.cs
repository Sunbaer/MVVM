﻿using Data;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundBasedGameMVVM.Events
{
    /// <summary>
    /// Event signalizes to get AccountInformations like which save slot is used
    /// </summary>
    internal class GetAccountInformationsEvent : CompositePresentationEvent<AccountInfo>
    {
    }
}
