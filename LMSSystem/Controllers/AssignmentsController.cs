using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpPost("{assignmentId}/submit")]
        public IActionResult SubmitAssignment(int assignmentId) => throw new NotImplementedException();

        [Authorize(Roles = nameof(UserRole.Student))]
        [HttpGet("{assignmentId}/my-submission")]
        public IActionResult GetMySubmission(int assignmentId) => throw new NotImplementedException();

        [Authorize(Roles = $"{nameof(UserRole.Instructor)}, {nameof(UserRole.Assistant)}")]
        [HttpGet("{assignmentId}/submissions")]
        public IActionResult GetSubmissions(int assignmentId) => throw new NotImplementedException();

        [Authorize(Roles = $"{nameof(UserRole.Instructor)}, {nameof(UserRole.Assistant)}")]
        [HttpPut("submissions/{submissionId}/grade")]
        public IActionResult GradeSubmission(int submissionId) => throw new NotImplementedException();
    }
}
