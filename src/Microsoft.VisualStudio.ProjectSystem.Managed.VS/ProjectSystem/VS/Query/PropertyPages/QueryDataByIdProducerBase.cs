﻿// Licensed to the .NET Foundation under one or more agreements. The .NET Foundation licenses this file to you under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ProjectSystem.Query;
using Microsoft.VisualStudio.ProjectSystem.Query.ProjectModel.Implementation;
using Microsoft.VisualStudio.ProjectSystem.Query.QueryExecution;

namespace Microsoft.VisualStudio.ProjectSystem.VS.Query
{
    /// <summary>
    /// Handles the boilerplate of retrieving an <see cref="IEntityValue"/> based on an ID.
    /// </summary>
    internal abstract class QueryDataByIdProducerBase<T> : QueryDataProducerBase<IEntityValue>, IQueryDataProducer<IReadOnlyCollection<EntityIdentity>, IEntityValue>
    {
        public async Task SendRequestAsync(QueryProcessRequest<IReadOnlyCollection<EntityIdentity>> request)
        {
            foreach (EntityIdentity requestId in request.RequestData)
            {
                if (TryExtactKeyDataOrNull(requestId) is T keyData)
                {
                    try
                    {
                        IEntityValue? entityValue = await TryCreateEntityOrNullAsync(request.QueryExecutionContext.EntityRuntime, requestId, keyData);
                        if (entityValue is not null)
                        {
                            await ResultReceiver.ReceiveResultAsync(new QueryProcessResult<IEntityValue>(entityValue, request, ProjectModelZones.Cps));
                        }
                    }
                    catch (Exception ex)
                    {
                        request.QueryExecutionContext.ReportError(ex);
                    }
                }
            }

            await ResultReceiver.OnRequestProcessFinishedAsync(request);
        }

        protected abstract T? TryExtactKeyDataOrNull(EntityIdentity requestId);
        protected abstract Task<IEntityValue?> TryCreateEntityOrNullAsync(IEntityRuntimeModel runtimeModel, EntityIdentity id, T keyData);
    }
}
