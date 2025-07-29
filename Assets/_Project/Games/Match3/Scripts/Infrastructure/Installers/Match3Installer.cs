using _Project.Games.Match3.Application.Services;
using _Project.Games.Match3.Application.UseCases;
using _Project.Games.Match3.Domain.Ports;
using Reflex.Core;
using UnityEngine;

namespace _Project.Games.Match3.Infrastructure.Installers
{
    public class Match3Installer : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddScoped(typeof(GridService), typeof(IGridService));
            builder.AddScoped(typeof(ScoreService), typeof(IScoreService));
            builder.AddScoped(typeof(AddScoreUseCase));
            builder.AddScoped(typeof(ResetScoreUseCase));
        }
    }
}