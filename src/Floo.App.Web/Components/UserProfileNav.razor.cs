using AntDesign;
using Floo.Core.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.App.Web.Components
{
    public partial class UserProfileNav : ComponentBase
    {
        [Inject] IIdentityContext identityContext { get; set; }

        public void OnUserItemSelected(MenuItem menuItem) { }

        protected override async Task OnInitializedAsync()
        {
            await identityContext.GetState();

            await base.OnInitializedAsync();
        }
    }
}
