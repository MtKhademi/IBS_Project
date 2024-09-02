//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Interfaces;
using Common.DependencyInjectionHelpers;
using Core.UserManagement.Models;

namespace Core.UserManagement.Abstractions.Services;

[Scope]
public interface IJWTUserHandlerService : IJWTHandlerService<UserManagementModel>
{
}
