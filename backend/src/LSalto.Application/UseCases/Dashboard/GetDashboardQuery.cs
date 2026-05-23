using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Dashboard;

public record GetDashboardQuery(int IdPublicador) : IRequest<DashboardDto>;
