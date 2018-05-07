using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts.Services
{
    public interface IService
    {
        IServiceProvider ServiceProvider { get; }
    }
}
