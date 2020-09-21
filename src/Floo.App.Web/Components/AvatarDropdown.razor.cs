using AntDesign;
using Microsoft.AspNetCore.Components;

namespace Floo.App.Web.Components
{
    public partial class AvatarDropdown
    {
        [Parameter] public string Avatar { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public EventCallback<MenuItem> OnItemSelected { get; set; }
    }
}