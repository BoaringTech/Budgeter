using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;

namespace Budgeter.Server.Services
{
    public class SettingService : ISettingService
    {
        public BooleanSettingDTO TranslateSetting(BooleanSetting s)
        {
            return new BooleanSettingDTO { 
                Name = s.Name, 
                Enabled = s.Enabled 
            };
        }
    }
}
