using Application.Interfaces;
using AutoMapper;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ItlaSocialMedia.Controllers
{
    [Authorize]
    public class ReactionController : Controller
    {
        private readonly IReactionService reactionService;
        private readonly IMapper mapper;


        public ReactionController(IReactionService reactionService, IMapper mapper)
        {
            this.reactionService = reactionService;
            this.mapper = mapper;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpPost]
        public async Task<IActionResult> React(Guid postId, ReactionType type)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Request.Headers["Referer"].ToString());

            }

            var userId = GetUserId();
            await reactionService.ReactAsync(userId, postId, type);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}