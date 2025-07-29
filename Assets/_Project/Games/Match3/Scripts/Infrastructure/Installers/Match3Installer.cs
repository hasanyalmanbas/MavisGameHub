using _Project.Games.Match3.Scripts.Application.UseCases;
using _Project.Games.Match3.Scripts.Domain.Ports;
using _Project.Games.Match3.Scripts.Domain.Services;
using Reflex.Core;
using UnityEngine;

namespace _Project.Games.Match3.Scripts.Infrastructure.Installers
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