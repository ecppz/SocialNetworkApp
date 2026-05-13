using Application.Dtos.Post;
using Application.Interfaces;
using Application.ViewModels.Comment;
using Application.ViewModels.Friend;
using Application.ViewModels.Post;
using Application.ViewModels.Reaction;
using AutoMapper;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ItlaSocialMedia.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendRequestService friendRequestService;
        private readonly IFriendService friendService;
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly UserManager<AccountUser> userManager;
        private readonly ICommentService commentService;

        public FriendController(IFriendRequestService friendRequestService, IMapper mapper, IFriendService friendService, IPostService postService,
            ICommentService commentService,
            UserManager<AccountUser> userManager)
        {
            this.friendRequestService = friendRequestService;
            this.friendService = friendService;
            this.postService = postService;
            this.commentService = commentService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Route("Friend/{userName}")]
        public async Task<IActionResult> FriendProfile(string userName)
        {
            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userSession = await userManager.GetUserAsync(User);

            var friend = await userManager.FindByNameAsync(userName);
            if (friend == null)
            {
                TempData["Message"] = "Usuario no encontrado.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            var friendId = Guid.Parse(friend.Id);
            var friendIds = await friendRequestService.GetAcceptedFriendIdsAsync(currentUserId);

            if (!friendIds.Contains(friendId))
            {
                TempData["Message"] = "No tienes acceso a este perfil.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

       
            var postDtos = await postService.GetPostsByUserIdAsync(friendId);
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

            var allUsers = await userManager.Users.ToListAsync();

            foreach (var postVm in postVms)
            {
                var reaction = postVm.Reactions.FirstOrDefault(r => r.UserId == currentUserId);
                postVm.CurrentUserReaction = reaction?.Type;

                var commentDtos = await commentService.GetByPostIdAsync(postVm.Id);
                var allComments = mapper.Map<List<CommentDisplayViewModel>>(commentDtos);

                var commentUserIds = allComments.Select(c => c.UserId).Distinct().ToList();

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
            ViewBag.CurrentUserProfile = userSession.ProfileImage;

            ViewBag.FriendName = $"{friend.Name} {friend.LastName}";
            return View(postVms);
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userSession = await userManager.GetUserAsync(User);

            var friendIds = await friendRequestService.GetAcceptedFriendIdsAsync(currentUserId);
            var friendIdStrings = friendIds.Select(id => id.ToString()).ToList();

            var users = await userManager.Users
                .Where(u => friendIdStrings.Contains(u.Id))
                .ToListAsync();

            var friendViewModels = users.Select(u => new FriendViewModel
            {
                Id = Guid.Parse(u.Id),
                UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName,
                ProfileImage = u.ProfileImage ?? ""
            }).ToList();

            var posts = await postService.GetFriendsPostsAsync(currentUserId);
            var allUsers = await userManager.Users.ToListAsync();

            var postViewModels = posts.Select(p =>
            {
                var reactions = p.Reactions;
                var currentReaction = reactions.FirstOrDefault(r => r.UserId == currentUserId);

                return new PostDisplayViewModel
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    ImageFile = p.ImageFile,
                    YouTubeUrl = p.YouTubeUrl,
                    UserId = p.UserId,
                    UserName = allUsers.First(u => Guid.Parse(u.Id) == p.UserId).UserName,
                    ProfileImage = allUsers.First(u => Guid.Parse(u.Id) == p.UserId).ProfileImage ?? "",
                    Reactions = mapper.Map<ICollection<ReactionViewModel>>(reactions),
                    CurrentUserReaction = currentReaction?.Type
                };
            }).ToList();


            foreach (var postVm in postViewModels)
            {
                var commentDtos = await commentService.GetByPostIdAsync(postVm.Id);
                var allComments = mapper.Map<List<CommentDisplayViewModel>>(commentDtos);

                var commentUserIds = allComments.Select(c => c.UserId).Distinct().ToList();
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

            var vm = new FriendFeedViewModel
            {
                Friends = friendViewModels,
                Posts = postViewModels
            };
            ViewBag.CurrentUserId = currentUserId;
            ViewBag.CurrentUserProfile = userSession.ProfileImage;
            return View(vm);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var friendId = id.ToString();
            var friend = await userManager.Users.FirstOrDefaultAsync(u => u.Id == friendId);
            if (friend == null)
            {
                TempData["Message"] = "Amigo no encontrado.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            var friendIds = await friendRequestService.GetAcceptedFriendIdsAsync(currentUserId);
            if (!friendIds.Contains(id))
            {
                TempData["Message"] = "No tienes acceso a eliminar este amigo.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            var mutualFriends = await friendService.GetMutualFriendAsync(currentUserId, id); 

            var vm = new DeleteFriendViewModel
            {
                Id = id,
                UserName = friend.UserName,
                ProfileImage = friend.ProfileImage,
                MutualFriends = mutualFriends
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(DeleteFriendViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var success = await friendService.RemoveFriendAsync(currentUserId, vm.Id);

            if (success)
            {
                TempData["Message"] = "Amigo eliminado correctamente";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "No se pudo eliminar al amigo";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }
    }
}