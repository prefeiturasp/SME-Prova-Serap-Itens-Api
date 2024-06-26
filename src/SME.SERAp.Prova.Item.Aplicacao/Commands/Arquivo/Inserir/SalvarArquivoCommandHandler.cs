﻿using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Arquivo.Inserir
{
    public class SalvarArquivoCommandHandler : IRequestHandler<SalvarArquivoCommand, long>
    {
        private readonly IRepositorioArquivo repositorioArquivo;

        public SalvarArquivoCommandHandler(IRepositorioArquivo repositorioArquivo)
        {
            this.repositorioArquivo = repositorioArquivo ?? throw new ArgumentNullException(nameof(repositorioArquivo));
        }

        public async Task<long> Handle(SalvarArquivoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await repositorioArquivo.SalvarAsync(request.Arquivo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
