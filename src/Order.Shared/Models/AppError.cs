using System.Net;

namespace Order.Models;

public record AppError(HttpStatusCode StatusCode, IEnumerable<string> Messages);