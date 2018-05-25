using Kina.Mobile.DataProvider.Models;
using System;

namespace Kina.Mobile.Core.Services
{
    public interface IDataConverter
    {
        object FromJson(string json, Type type);
        string SerializeScore(UserScore userScore);
    }
}
