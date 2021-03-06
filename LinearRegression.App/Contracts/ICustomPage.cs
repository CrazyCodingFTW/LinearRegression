﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    public interface ICustomPage
    {
        IServiceProvider Services { get; }

        string PageTitle { get; }

        IHelpContent HelpContent { get; }
    }
}
