using Application.Interfaces;
using Application.ViewModels.FriendRequest;
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
    public class FriendRequestController : Controller
    {
        private readonly IFriendRequestService friendRequestService;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly UserManager<AccountUser> userManager;
        public FriendRequestController(IFriendRequestService friendRequestService, IAccountService accountService, IMapper mapper, UserManager<AccountUser> userManager)
        {
            this.friendRequestService = friendRequestService;
            this.accountService = accountService;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var received = await friendRequestService.GetReceivedRequestsAsync(userId);
            var sent = await friendRequestService.GetSentRequestsAsync(userId);

            var vm = new FriendRequestListViewModel
            {
                ReceivedRequests = mapper.Map<List<FriendRequestDisplayViewModel>>(received),
                SentRequests = mapper.Map<List<FriendRequestDisplayViewModel>>(sent)
            };

            var senderIds = received.Select(r => r.SenderUserId.ToString());
            var receiverIds = sent.Select(r => r.ReceiverUserId.ToString());
            var allUserIds = senderIds.Concat(receiverIds).Distinct().ToList();

            var users = await userManager.Users
                .Where(u => allUserIds.Contains(u.Id))
                .ToListAsync();

            var userMap = users.ToDictionary(u => Guid.Parse(u.Id), u => u);

            foreach (var request in vm.ReceivedRequests)
            {
                if (userMap.TryGetValue(request.SenderUserId, out var sender))
                {
                    request.UserName = sender.UserName;
                    request.ProfileImage = sender.ProfileImage;
                }
            }

            foreach (var request in vm.SentRequests)
            {
                if (userMap.TryGetValue(request.ReceiverUserId, out var receiver))
                {
                    request.UserName = receiver.UserName;
                    request.ProfileImage = receiver.ProfileImage;
                }
            }


            return View(vm);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public async Task<IActionResult> Send(string? searchTerm)
        {
            var userId = GetUserId();
            var allUserIds = await accountService.GetAllUserIdsAsync();

            var activeRequestUserIds = await friendRequestService.GetUsersWithActiveRequestAsync(userId);

            var candidateIds = allUserIds
                .Where(id => id != userId && !activeRequestUserIds.Contains(id))
                .ToList();

            var acceptedFriendIds = await friendRequestService.GetAcceptedFriendIdsAsync(userId);

            var candidateIdStrings = candidateIds.Select(id => id.ToString()).ToList();

            var users = await userManager.Users
                .Where(u => candidateIdStrings.Contains(u.Id) && u.EmailConfirmed)
                .ToListAsync();


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                users = users
                    .Where(u => u.UserName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var vm = new FriendRequestFormViewModel
            {
                SearchTerm = searchTerm,
                AvailableUsers = new List<FriendRequestDisplayViewModel>()
            };

            foreach (var u in users)
            {
                var candidateId = Guid.Parse(u.Id);
                var candidateFriends = await friendRequestService.GetAcceptedFriendIdsAsync(candidateId);

                var vmUser = new FriendRequestDisplayViewModel
                {
                    Id = candidateId,
                    UserName = u.UserName,
                    ProfileImage = u.ProfileImage,
                    MutualFriends = candidateFriends.Intersect(acceptedFriendIds).Count()
                };

                vm.AvailableUsers.Add(vmUser);
            }

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Send(Guid receiverUserId)
        {
            var senderId = GetUserId();
            var success = await friendRequestService.SendFriendRequestAsync(senderId, receiverUserId);

            TempData["Message"] = success ? "Solicitud enviada." : "Ya existe una solicitud activa.";
            TempData["MessageType"] = success ? "success" : "warning";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Accept(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var dto = await friendRequestService.GetByIdAsync(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }

            var sender = await accountService.GetUserById(dto.SenderUserId.ToString());
            var senderFriends = await friendRequestService.GetAcceptedFriendIdsAsync(dto.SenderUserId);
            var receiverFriends = await friendRequestService.GetAcceptedFriendIdsAsync(dto.ReceiverUserId);
            var mutualFriends = senderFriends.Intersect(receiverFriends).Count();

            var vm = mapper.Map<FriendRequestDisplayViewModel>(dto);
            vm.UserName = sender?.UserName;
            vm.ProfileImage = sender?.ProfileImage;
            vm.MutualFriends = mutualFriends;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(FriendRequestDisplayViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await friendRequestService.AcceptRequestAsync(vm.Id);

            TempData["Message"] = "Solicitud aceptada.";
            TempData["MessageType"] = "success";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Reject(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var dto = await friendRequestService.GetByIdAsync(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }

            var sender = await accountService.GetUserById(dto.SenderUserId.ToString());
            var senderFriends = await friendRequestService.GetAcceptedFriendIdsAsync(dto.SenderUserId);
            var receiverFriends = await friendRequestService.GetAcceptedFriendIdsAsync(dto.ReceiverUserId);
            var mutualFriends = senderFriends.Intersect(receiverFriends).Count();

            var vm = mapper.Map<FriendRequestDisplayViewModel>(dto);
            vm.UserName = sender?.UserName;
            vm.ProfileImage = sender?.ProfileImage;
            vm.MutualFriends = mutualFriends;

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Reject(FriendRequestDisplayViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await friendRequestService.RejectRequestAsync(vm.Id);

            TempData["Message"] = "Solicitud rechazada.";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index");
            }

            var dto = await friendRequestService.GetByIdAsync(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }

            var receiver = await accountService.GetUserById(dto.ReceiverUserId.ToString());

            var senderFriends = await friendRequestService.GetAcceptedFriendIdsAsync(dto.SenderUserId);
            var receiverFriends = await friendRequestService.GetAcceptedFriendIdsAsync(dto.ReceiverUserId);

            var mutualFriends = senderFriends.Intersect(receiverFriends).Count();

            var vm = mapper.Map<FriendRequestDisplayViewModel>(dto);
            vm.MutualFriends = mutualFriends;

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(FriendRequestDisplayViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await friendRequestService.DeleteAsync(vm.Id);

            TempData["Message"] = "Solicitud eliminada.";
            TempData["MessageType"] = "warning";

            return RedirectToAction("Index");
        }

    }

}