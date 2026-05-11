using Application.Interfaces;
using Application.ViewModels.Comment;
using AutoMapper;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ItlaSocialMedia.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly UserManager<AccountUser> userManager;

        public CommentController(ICommentService commentService, IMapper mapper, UserManager<AccountUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid postId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["Message"] = "El comentario no puede estar vacío.";
                TempData["MessageType"] = "warning";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var userId = GetUserId();
            var commentDto = await commentService.AddCommentAsync(postId, userId, text);

            if (commentDto == null)
            {
                TempData["Message"] = "No se pudo agregar el comentario.";
                TempData["MessageType"] = "danger";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            var user = await userManager.FindByIdAsync(userId.ToString());
            var commentDisplay = mapper.Map<CommentDisplayViewModel>(commentDto);
            if (user != null)
            {
                commentDisplay.UserName = user.UserName;
                commentDisplay.ProfileImage = user.ProfileImage;
            }

            var commentVm = mapper.Map<CommentDisplayViewModel>(commentDto);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Reply(Guid postId, Guid replyToCommentId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["Message"] = "La respuesta no puede estar vacía.";
                TempData["MessageType"] = "warning";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var userId = GetUserId();
            var replyDto = await commentService.AddReplyAsync(postId, replyToCommentId, userId, text);

            if (replyDto == null)
            {
                TempData["Message"] = "No se pudo agregar la respuesta.";
                TempData["MessageType"] = "danger";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var user = await userManager.FindByIdAsync(userId.ToString());
            var replyDisplay = mapper.Map<CommentDisplayViewModel>(replyDto);
            if (user != null)
            {
                replyDisplay.UserName = user.UserName;
                replyDisplay.ProfileImage = user.ProfileImage;
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Edit(Guid id, bool isReply)
        {
            var userId = GetUserId();
            var result = await commentService.GetByIdAsync(id);

            if (result.IsFailure || result.Value == null || result.Value.UserId != userId)
            {
                TempData["Message"] = isReply
                    ? "No tienes permiso para editar esta respuesta."
                    : "No tienes permiso para editar este comentario.";
                TempData["MessageType"] = "danger";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var comment = result.Value;

            var vm = new EditCommentViewModel
            {
                Id = comment.Id,
                PostId = comment.PostId,
                Text = comment.Text
            };

            ViewBag.IsReply = isReply;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentViewModel model, string? returnUrl)
        {
            var userId = GetUserId();
            var (success, isReply) = await commentService.UpdateAsync(model.Id, userId, model.Text);

            if (string.IsNullOrWhiteSpace(model.Text))
            {
                TempData["Message"] = isReply
                    ? "La respuesta no puede estar vacía."
                    : "El comentario no puede estar vacío.";
                TempData["MessageType"] = "warning";
                ViewBag.IsReply = isReply;
                return View(model);
            }

            if (!success)
            {
                TempData["Message"] = isReply
                    ? "No se pudo editar la respuesta."
                    : "No se pudo editar el comentario.";
                TempData["MessageType"] = "danger";
                ViewBag.IsReply = isReply;
                return View(model);
            }

            TempData["Message"] = isReply
                ? "Respuesta editada correctamente."
                : "Comentario editado correctamente.";
            TempData["MessageType"] = "success";
            return Redirect(returnUrl);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, bool isReply)
        {
            var userId = GetUserId();
            var success = await commentService.DeleteAsync(id, userId);

            if (!success)
            {
                TempData["Message"] = isReply
                    ? "No se pudo eliminar la respuesta"
                    : "No se pudo eliminar el comentario";
                TempData["MessageType"] = "danger";
            }
            else
            {
                TempData["Message"] = isReply
                    ? "Respuesta eliminada correctamente"
                    : "Comentario eliminado correctamente";
                TempData["MessageType"] = "success";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }



    }

}