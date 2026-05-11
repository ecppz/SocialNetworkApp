using Application.Dtos.Post;
using Application.Interfaces;
using Application.ViewModels.Post;
using AutoMapper;
using ItlaSocialMedia.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ItlaSocialMedia.Controllers
{
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        public IActionResult Create()
        {
            return View("Save", new SavePostViewModel { Content = "", ContentType=""});
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePostViewModel vm)
        {
            var userId = GetUserId();

            if (!string.IsNullOrWhiteSpace(vm.ContentType))
            {
                if (vm.ContentType == "image" && vm.ImageFile == null)
                {
                    ModelState.AddModelError("ImageFile", "Debes subir una imagen.");
                }

                if (vm.ContentType == "video" && string.IsNullOrWhiteSpace(vm.YouTubeUrl))
                {
                    ModelState.AddModelError("YouTubeUrl", "Debes ingresar un enlace de YouTube.");
                }
            }
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            var dto = new PostDto
            {
                Id = Guid.NewGuid(),
                Content = vm.Content,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            if (vm.ContentType == "image")
            {
                dto.ImageFile = FileManager.Upload(vm.ImageFile, dto.Id.ToString(), "Posts");
                dto.YouTubeUrl = null;
            }
            else if (vm.ContentType == "video")
            {
                dto.YouTubeUrl = vm.YouTubeUrl;
                dto.ImageFile = null;
            }

            var result = await postService.AddAsync(dto);

            if (result.IsFailure || result.Value == null)
            {
                ModelState.AddModelError("", "No se pudo crear el post, intenta nuevamente");
                return View(vm);
            }

            var createdPost = result.Value;

            if (vm.ImageFile != null)
            {
                createdPost.ImageFile = FileManager.Upload(vm.ImageFile, createdPost.Id.ToString(), "Posts");
                await postService.UpdateAsync(createdPost.Id, createdPost);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await postService.GetByIdAsync(id);

            if (result.IsFailure || result.Value == null || result.Value.UserId != GetUserId())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            EditPostViewModel vm = mapper.Map<EditPostViewModel>(result.Value);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel vm)
        {
            var userId = GetUserId();

            var currentResult = await postService.GetByIdAsync(vm.Id);
            if (currentResult.IsFailure || currentResult.Value == null || currentResult.Value.UserId != userId)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var currentDto = currentResult.Value;


            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new PostDto
            {
                Id = vm.Id,
                Content = vm.Content,
                UserId = userId,
                CreatedAt = currentDto.CreatedAt,
                ImageFile = currentDto.ImageFile,
                YouTubeUrl = currentDto.YouTubeUrl
            };

            if (vm.ContentType == "image")
            {
                if (vm.ImageFile != null)
                {
                    dto.ImageFile = FileManager.Upload(vm.ImageFile, dto.Id.ToString(), "Posts", true, currentDto.ImageFile
                    );

                    dto.YouTubeUrl = null;
                }
            }

            else if (vm.ContentType == "video")
            {
                if (!string.IsNullOrWhiteSpace(vm.YouTubeUrl))
                {
                    dto.YouTubeUrl = vm.YouTubeUrl;
                    dto.ImageFile = null;
                }
            }

            await postService.UpdateAsync(dto.Id, dto);

            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var result = await postService.GetByIdAsync(id);

            if (result.IsFailure || result.Value == null || result.Value.UserId != GetUserId())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var dto = result.Value;
            DeletePostViewModel vm = mapper.Map<DeletePostViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await postService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
