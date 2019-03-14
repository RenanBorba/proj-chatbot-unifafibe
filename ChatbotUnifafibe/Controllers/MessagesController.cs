using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChatbotUnifafibe.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Connector;

namespace ChatbotUnifafibe
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            var attributes = new LuisModelAttribute(
                ConfigurationManager.AppSettings["LuisId"],
                ConfigurationManager.AppSettings["LuisSubscriptionKey"]);

            var service = new LuisService(attributes);

            switch (activity.Type)
            {
                case ActivityTypes.Message:
                    await Conversation.SendAsync(activity, () => new LuisDialog(service));
                    break;

                case ActivityTypes.ConversationUpdate:

                    if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                    {
                        var reply = activity.CreateReply();

                        var cardApresentacao = new HeroCard();
                        cardApresentacao.Title = "UNIFAFIBE";
                        cardApresentacao.Subtitle = "Veja os todos os cursos que a UNIFAFIBE oferece, " +
                            "valores das mensalidades, " +
                            "portal do aluno e informações gerais! " +
                            "Diga no que posso te ajudar!";

                        cardApresentacao.Images = new List<CardImage>
                        {
                         new CardImage("https://scontent.fbat1-1.fna.fbcdn.net/v/t1.0-9/38757640_1817564504965401_3697432681068363776_n.png?_nc_cat=0&oh=264ab237e16722e8f700a1f202adce62&oe=5BFAE12F",
                            "Unifafibe",
                         new CardAction(ActionTypes.OpenUrl,
                            "unifafibe", "http://unifafibe.com.br/" ))
                        };

                        cardApresentacao.Buttons = new List<CardAction> {

                        new CardAction

                        {
                            Title = "Site",
                            DisplayText = "Test",
                            Value = $"http://www.unifafibe.com.br/",
                            Type = "openUrl",
                        },

                        new CardAction

                        {
                            Title = "Teste Vocacional",
                            DisplayText = "Test",
                            Value = $"http://www.unifafibe.com.br/testevocacional/#",
                            Type = "openUrl", 
                        }

                    };                                         

                        reply.Attachments = new List<Attachment> {
                        cardApresentacao.ToAttachment()

                    };                 
                       await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                    
                   break;
            }
                     

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
