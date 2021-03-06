﻿using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace demo1.Middleware
{
    public class Middleware2 : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            await turnContext.SendActivityAsync($"[Middleware2]{turnContext.Activity.Type}/OnTurn/Before");
            if (turnContext.Activity.Type is ActivityTypes.Message && turnContext.Activity.Text == "secret password")
            {
                await next(cancellationToken);
            }

            await turnContext.SendActivityAsync($"[Middleware2]{turnContext.Activity.Type}/OnTurn/After");
        }
    }
}
