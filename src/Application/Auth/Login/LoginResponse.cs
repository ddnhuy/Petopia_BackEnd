﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Login;

public sealed record LoginResponse(string AccessToken, string RefreshToken);
