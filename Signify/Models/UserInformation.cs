using System;
using System.Collections.Generic;

namespace Signify.Models;

public partial class UserInformation
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
