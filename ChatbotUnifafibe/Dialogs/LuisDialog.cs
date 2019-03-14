using System;
using System.Linq;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;

using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace ChatbotUnifafibe.Dialogs
{
    [Serializable]
    /*
    [LuisModel("2e75afb4-8fe2-4cd9-8cc3-2e3d734c9e16", "7147e225d42b4c518c6e233d4d0ece82")]   // modal, sub-key, 
    */

    public class LuisDialog : LuisDialog<object>
    {
        public LuisDialog (ILuisService service) : base(service) { }

        //Intents 

        //Frase usada no tratamento de intent "None", ao não entender o contexto
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //Anexa a pergunta(query) a frase(result)
            await context.PostAsync($"Desculpe, não consigo entender a intenção da sua pergunta.  {result.Query}");
        }

        [LuisIntent("baixo-calao")]
        public async Task BaixoCalao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Idêntificamos palavras de baixo calão e ofensa! Por favor, reformule sua pergunta.");
        }

        [LuisIntent("consciencia")]
        public async Task Consciencia(IDialogContext context, LuisResult result)
        {
            //Frase usada no tratamento de intent "Consciencia"
            await context.PostAsync("Sou o ChatBot UNIFAFIBE que " +
                "foi desenvolvido para sanar dúvidas sobre o âmbito educacional do Centro Universitário!");
        }


        [LuisIntent("contato")]
        public async Task Contato(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Rua Prof. Orlando França de Carvalho, 325 - Centro " +
                "Bebedouro/SP \n" +
                "CEP 14.701-070 \n" +
                "E-mail | contato@unifafibe.com.br \n"+
                "FAX | (17) 3344-7101 \n" +
                "PABX | (17) 3344-7100 \n" +
                "Demais informações ↓ \n" +
                "http://unifafibe.com.br/contato/"

                );
        }

        [LuisIntent("desconto-ex-aluno")]
        public async Task DescontoExAluno(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("O UNIFAFIBE possui o programa Segunda Graduação, que oferece descontos de até 40%, " +
                "nas mensalidades dos cursos de Graduação, para alunos que já possuam qualquer graduação de nível superior " +
                "que seja comprovada pelo MEC, através de Diploma de Conclusão de Curso. O programa é válido tanto para " +
                "graduados do UNIFAFIBE quanto para graduados de outras Instituições.");
        }

        [LuisIntent("documentacao")]
        public async Task Documentacao(IDialogContext context, LuisResult result)
        {
                await context.PostAsync("O aluno poderá acessar o campo ‘’Solicitação de Documentos’’ através do portal do aluno, " +
                "ou então através do nosso site www.unifafibe.com.br/secretaria.");
        }

        [LuisIntent("horario")]
        public async Task Horario(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Nossa Central de Atendimento ao Aluno funciona: \n" +
                "De segunda a sexta: das 12h às 22h \n" +
                "Aos sábados: das 9h às 12h \n" +
                "Biblioteca:\n" +
                "De segunda a sexta: das 9h10min às 22h50min \n" +
                "Aos sábados: das 8h às 16h");
        }

        [LuisIntent("informativo")]
        public async Task Informativo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Para demais informações, entrar em contato com a secretaria: (17) 3344-7100");
        }

        [LuisIntent("vocabulario-ingles")]
        public async Task Ingles(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("Sorry, I do not have support for English, contact the service center by email " +
                "contato@unifafibe.com.br or if you prefer" +
                " can consult other contacts available on our site http://unifafibe.com.br/contato/");
        }


        [LuisIntent("saudar")]
        public async Task Saudar(IDialogContext context, LuisResult result)
        {
            var agora = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).TimeOfDay;
            string saudar;

            //Reconhece período do dia
            if (agora < TimeSpan.FromHours(12)) saudar = "Bom dia";
            else if (agora < TimeSpan.FromHours(18)) saudar = "Boa tarde";
            else saudar = "Boa noite";

            await context.PostAsync($"{saudar}, tudo bem? Sou o ChatBot Inteligente UNIFAFIBE. Estou aqui para te ajudar, qual a sua dúvida?");

            context.Done<string>(null);
        }



        //Entities

        [LuisIntent("vestibular")]
        public async Task Vestibular(IDialogContext context, LuisResult result)
        {
            //declaração variavel das entidades
            var atoProva = result.Entities.FirstOrDefault(c => c.Type == "ato-prova")?.Entity;
            var bolsa = result.Entities.FirstOrDefault(c => c.Type == "bolsa")?.Entity;
            var inscricao = result.Entities.FirstOrDefault(c => c.Type == "inscricao")?.Entity;
            var modalidade = result.Entities.FirstOrDefault(c => c.Type == "modalidade")?.Entity;
            var inscricaoMeioAno = result.Entities.FirstOrDefault(c => c.Type == "incricao-meio-ano")?.Entity;


            if (string.IsNullOrEmpty(inscricao))
                await context.PostAsync("teste2");


            if (!string.IsNullOrEmpty(bolsa))
                await context.PostAsync("teste5");


            /*


            if (!string.IsNullOrEmpty(modalidade))
                await context.PostAsync("teste3");

            if (!string.IsNullOrEmpty(inscricaoMeioAno))
                await context.PostAsync("teste4");

            */

        }


        /*

        [LuisIntent("curso")]
        public async Task Cursos(IDialogContext context, LuisResult result)
        {
           
            var educacaofisica = result.Entities.FirstOrDefault(c => c.Type == "educacao-fisica")?.Entity;
            string todosCursos;

       
            switch (string.IsNullOrEmpty(todosCursos))
            {
                
                

                case :

                    if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                    {
                        
                    
                   break;
            }
        */

    }
}