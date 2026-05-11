using Application.Dtos.User;
using Application.Interfaces;
using Application.ViewModels.User;
using AutoMapper;
using Infrastructure.Identity.Entities;
using ItlaSocialMedia.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ItlaSocialMedia.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountService accountService;
        private readonly UserManager<AccountUser> userManager;
        private readonly IMapper mapper;

        public ProfileController(IAccountService accountService, UserManager<AccountUser> userManager, IMapper mapper)
        {
            this.accountService = accountService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var dto = await accountService.GetUserById(user.Id);

            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var vm = mapper.Map<UpdateUserViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var dto = mapper.Map<SaveUserDto>(vm);
            dto.UserName = user.UserName ?? "";
            dto.Email = user.Email ?? "";
            dto.Password = vm.Password ?? "";
            dto.ProfileImage = FileManager.Upload(vm.ProfileImageFile, user.Id, "Users");

            var result = await accountService.EditUser(dto, Request.Headers["Origin"].ToString());

            if (result.HasError)
            {
                TempData["Message"] = "error al actualizar el usuario";
                TempData["MessageType"] = "danger";
            }
            else
            {
                TempData["Message"] = "usuario editado correctamente";
                TempData["MessageType"] = "success";

            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

    }

}