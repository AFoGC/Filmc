using Filmc.Entities.Entities;
using Filmc.Entities.Repositories;
using Filmc.Recomendations.Recomendations;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class RecomendationMenuService
    {
        private RecomendationMenuViewModel _menuViewModel;
        private readonly ProfilesService _profilesService;
        private readonly FilmsRecomendationService _filmsRecomendationService;

        public RecomendationMenuService(RecomendationMenuViewModel menuViewModel, ProfilesService profilesService)
        {
            _menuViewModel = menuViewModel;
            _profilesService = profilesService;
            _filmsRecomendationService = new FilmsRecomendationService();
        }

        public void OpenRecomendations(FilmTablesViewModel tablesViewModel)
        {
            RepositoriesFacade repositories = _profilesService.SelectedProfile.TablesContext;
            FilmsRecomendationService service = new FilmsRecomendationService();
            var ratings = service.CreateRecomendations(repositories);

            EntityRating<FilmViewModel>[] viewModels = new EntityRating<FilmViewModel>[ratings.Length];

            for (int i = 0; i < ratings.Length; i++)
            {
                var ratingEntity = ratings[i];
                var viewModel = tablesViewModel.FilmVMs
                    .First(x => x.Model == ratingEntity.Entity);

                viewModels[i] = new EntityRating<FilmViewModel>(viewModel)
                {
                    TagRating = ratingEntity.TagRating,
                    GenreRating = ratingEntity.GenreRating,
                    CategoryRating = ratingEntity.CategoryRating,
                    TotalRating = ratingEntity.TotalRating
                };
            }

            _menuViewModel.OpenMenu(viewModels);
        }

        public void CloseRecomendations()
        {
            _menuViewModel.CloseMenu();
        }
    }
}
