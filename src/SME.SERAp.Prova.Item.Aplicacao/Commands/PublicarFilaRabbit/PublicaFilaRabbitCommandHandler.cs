using MediatR;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using SME.SERAp.Prova.Item.Infra.Extensions;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SME.SERAp.Prova.Item.Infra.Services.ServicoLog;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.PublicarFilaRabbit
{
    public class PublicaFilaRabbitCommandHandler : IRequestHandler<PublicaFilaRabbitCommand, bool>
    {
        private readonly IChannel channel;
        private readonly IServicoLog servicoLog;

        public PublicaFilaRabbitCommandHandler(IChannel channel, IServicoLog servicoLog)
        {
            this.channel = channel ?? throw new ArgumentNullException(nameof(channel));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public async Task<bool> Handle(PublicaFilaRabbitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mensagem = new MensagemRabbit(request.Mensagem, Guid.NewGuid());
                var body = Encoding.UTF8.GetBytes(mensagem.ConverterObjectParaJson());
                var props = new BasicProperties { Persistent = true };

                var pipeline = new ResiliencePipelineBuilder()
                    .AddRetry(new RetryStrategyOptions
                    {
                        MaxRetryAttempts = 3,
                        Delay = TimeSpan.FromSeconds(2),
                        BackoffType = DelayBackoffType.Exponential,
                        OnRetry = args =>
                        {
                            servicoLog.Registrar(LogNivel.Critico,
                                $"Erro ao publicar mensagem, tentativa {args.AttemptNumber + 1}: {args.Outcome.Exception?.Message}",
                                string.Empty,
                                args.Outcome.Exception?.StackTrace);
                            return ValueTask.CompletedTask;
                        }
                    })
                    .Build();

                await pipeline.ExecuteAsync(async ct =>
                {
                    var address = new PublicationAddress(ExchangeType.Direct, ExchangeRabbit.SerapEstudanteItem, request.NomeRota);
                    await channel.BasicPublishAsync(address, props, body, ct);
                }, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(LogNivel.Critico,
                    $"Erro: PublicaFilaRabbitCommand -- {ex.Message}",
                    $"API Serap: Rota -> {request.NomeRota} Fila -> {request.NomeFila}",
                    ex.StackTrace);
                return false;
            }
        }
    }
}