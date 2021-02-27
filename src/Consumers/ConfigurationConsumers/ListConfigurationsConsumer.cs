﻿using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using MassTransit;
using MongoDB.Driver;

namespace Communications.Business.Consumers.ConfigurationConsumers
{
    public class ListConfigurationsConsumer : IConsumer<ListConfigurations>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;

        public ListConfigurationsConsumer(IConfigurationMapper mapper, IConfigurationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ListConfigurations> context)
        {
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;

            var filterBy = _repository.Helpers.CreateFilters(
                context.Message.AccountId,
                context.Message.ShowDeleted,
                null,
                AddFilterBy(context.Message.FilterBy));

            var orderBy = _repository.Helpers.CreateSorting(
                context.Message.OrderBy,
                context.Message.Sorting.ToString());

            var (totalPages, configurations) = await _repository.QueryByPage(
                context.Message.AccountId,
                page,
                pageSize,
                filterBy,
                orderBy);

            if (!configurations.Any())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Configurations not found"
                });
            else
                await context.RespondAsync<ListConfigurationsResponse>(new
                {
                    context.Message.Page,
                    context.Message.PageSize,
                    RecordsInPage = configurations.Count,
                    TotalPages = totalPages,
                    Items = _mapper.MapModelToResponse(configurations.ToList()),
                    context.Message.FilterBy,
                    context.Message.OrderBy,
                    context.Message.Sorting
                });
        }
        
        private FilterDefinition<Configuration> AddFilterBy(string filterBy)
        {
            if (string.IsNullOrWhiteSpace(filterBy))
                return null;

            var optionalFilters = Builders<Configuration>.Filter.Or(
                _repository.Helpers.Like(doc => doc.Name, filterBy)
            );

            return optionalFilters;
        }
    }
}