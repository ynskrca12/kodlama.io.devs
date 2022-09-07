using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
        public class PLBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public PLBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(r => r.Name == name);
            if (result.Items.Any()) throw new BusinessException("ProgrammingLanguage name exists.");
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage programmingLanguage)
        {
            if (programmingLanguage == null) throw new BusinessException("Requested programming language does not exist");
        }


        public async Task ProgrammingLanguageShouldBeExist(int id)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
            if (result == null) throw new BusinessException("Programming Language does not exist.");
        }

        public async Task ProgrammingLanguageMustBeExist(ProgrammingLanguage programmingLanguage)
        {

            if (programmingLanguage is null) throw new BusinessException("Programming language not exist.");
        }

        public async Task ProgrammingLanguageCanNotBeDuplicatedWhenUpdated(string name)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(p => p.Name == name);

            if (result != null) throw new BusinessException("Programming Language exists.");
        }


    }
}
