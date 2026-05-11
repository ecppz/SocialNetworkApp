using Application.Dtos.Post;
using Application.Interfaces;
using Application.ViewModels.Comment;
using Application.ViewModels.Post;
using AutoMapper;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ItlaSocialMedia.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        private readonly UserManager<AccountUser> userManager;
        private readonly IMapper mapper;

        public HomeController(IPostService postService, ICommentService commentService, UserManager<AccountUser> userManager, IMapper mapper)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = GetUserId();
            var userSession = await userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var postDtos = await postService.GetPostsByUserIdAsync(currentUserId);
            var postDisplayDtos = mapper.Map<List<PostDisplayDto>>(postDtos);

            var userIds = postDisplayDtos.Select(p => p.UserId).Distinct().ToList();
            var stringUserIds = userIds.Select(id => id.ToString()).ToList();

            var users = await userManager.Users
                .Where(u => stringUserIds.Contains(u.Id))
                .ToDictionaryAsync(u => Guid.Parse(u.Id));

            foreach (var dto in postDisplayDtos)
            {
                if (users.TryGetValue(dto.UserId, out var user))
                {
                    dto.UserName = user.UserName;
                    dto.ProfileImage = user.ProfileImage;
                }
            }

            var postVms = mapper.Map<List<PostDisplayViewModel>>(postDisplayDtos);

            foreach (var postVm in postVms)
            {
                var reaction = postVm.Reactions.FirstOrDefault(r => r.UserId == currentUserId);
                postVm.CurrentUserReaction = reaction?.Type;

                var commentDtos = await commentService.GetByPostIdAsync(postVm.Id);
                var allComments = mapper.Map<List<CommentDisplayViewModel>>(commentDtos);

                var commentUserIds = allComments.Select(c => c.UserId).Distinct().ToList();
                var allUsers = await userManager.Users.ToListAsync();

                var commentUsers = allUsers
                    .Where(u => commentUserIds.Contains(Guid.Parse(u.Id)))
                    .ToDictionary(u => Guid.Parse(u.Id));


                foreach (var comment in allComments)
                {
                    if (commentUsers.TryGetValue(comment.UserId, out var commentUser))
                    {
                        comment.UserName = commentUser.UserName;
                        comment.ProfileImage = commentUser.ProfileImage;
                    }
                }

                var rootComments = allComments
                    .Where(c => c.ReplyToCommentId == null)
                    .ToList();

                foreach (var root in rootComments)
                {
                    root.Replies = allComments
                        .Where(r => r.ReplyToCommentId == root.Id)
                        .OrderBy(r => r.CreatedAt)
                        .ToList();
                }

                postVm.Comments = rootComments;
            }

            ViewBag.CurrentUserId = currentUserId;
            return View(postVms);
        }


    }
}
