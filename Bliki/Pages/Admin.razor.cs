using Bliki.Data;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;


namespace Bliki.Pages
{
    public partial class Admin
    {
        [Inject]
        public BlikiUserManager UserManager { get; set; } = default!;


        public async void AddUser()
        {
            await UserManager.CreateAsync(new BlikiUser(NewUser.Email), NewUser.Password);
            NewUser = new UserModel();
        }


        protected override void OnParametersSet()
        {
            _allUsers = UserManager.Users.ToList();
            base.OnParametersSet();
        }


        private async void ChangeAdminStatus(string userId)
        {
            var user = _allUsers.First(u => u.Id == userId);

            if (await UserManager.IsInRoleAsync(user, "admin"))
            {
                await UserManager.RemoveFromRoleAsync(user, "admin");
            }
            else
            {
                await UserManager.AddToRoleAsync(user, "admin");
            }

            await UserManager.UpdateAsync(user);
            StateHasChanged();
        }


        private async void DeleteUser(string userId)
        {
            var user = _allUsers.First(u => u.Id == userId);

            await UserManager.DeleteAsync(user);
            _allUsers.Remove(user);
            StateHasChanged();
        }


        private List<BlikiUser> _allUsers = new List<BlikiUser>();
        private UserModel NewUser = new UserModel();
    }
}